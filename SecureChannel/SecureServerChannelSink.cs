// Stephen Toub
// stoub@microsoft.com
// SecureServerChannelSink.cs

using System;
using System.IO;
using System.Net;
using System.Timers;
using System.Threading;
using System.Collections;
using System.Security.Cryptography;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace MsdnMag.Remoting
{
	/// <summary>
	/// Server channel sink that, in conjunction with SecureClientChannelSink, provides an 
	/// asymmetric key exchange and shared key encryption across a remoting channel.
	/// </summary>
	internal class SecureServerChannelSink : 
		BaseChannelSinkWithProperties, IServerChannelSink
	{
		#region Member Variables
		/// <summary>The name of the symmetric algorithm to use.</summary>
		private readonly string _algorithm = "";
		/// <summary>Whether OAEP padding should be used.</summary>
		private readonly bool _oaep = false;
		/// <summary>The minimum amount of time (s) information about a client connection should be retained.</summary>
		private readonly double _connectionAgeLimit;
		/// <summary>How often (s) the connection sweeper should run.</summary>
		private readonly double _sweepFrequency;
		/// <summary>Whether the server requires the client to use the secure client sink.</summary>
		private readonly bool _requireSecurity;
		/// <summary>List of IPAddresses that are excepted from the _requireSecurity restriction.</summary>
		private IPAddress [] _securityExemptionList;
		/// <summary>Table of all connections to this server.</summary>
		private readonly Hashtable _connections = null;
		/// <summary>The next sink in the sink chain.</summary>
		private readonly IServerChannelSink _next = null;
		/// <summary>Timer used to signal the cleanup of connections.</summary>
		private System.Timers.Timer _sweepTimer = null;
		#endregion

		#region Construction
		/// <summary>Initialize the secure channel sink.</summary>
		/// <param name="nextSink">The next sink in the chain.</param>
		/// <param name="algorithm">The name of the symmetric algorithm to use for encryption.</param>
		/// <param name="oaep">Whether OAEP padding should be used for asymmetric encryption.</param>
		/// <param name="connectionAgeLimit">The minimum amount of time (s) information about a client connection should be retained.</param>
		/// <param name="sweeperFrequency">How often (s) the connection sweeper should run.</param>
		/// <param name="requireSecurity">Whether the server requires the client to use the secure client sink.</param>
		/// <param name="securityExemptionList">The list of IPAddresses that are exempt from the requireSecurity restriction.</param>
		public SecureServerChannelSink(
			IServerChannelSink nextSink, 
			string algorithm, bool oaep, 
			double connectionAgeLimit, double sweeperFrequency, bool requireSecurity,
			IPAddress [] securityExemptionList)
		{
			// Set preferences
			_algorithm = algorithm;
			_oaep = oaep;
			_connectionAgeLimit = connectionAgeLimit;
			_sweepFrequency = sweeperFrequency;
			_requireSecurity = requireSecurity;
			_securityExemptionList = securityExemptionList;

			// Set the next sink
			_next = nextSink;

			// Setup the connection table and start sweeping it
			_connections = new Hashtable(103, 0.5F);
			StartConnectionSweeper();
		}
		#endregion

		#region Synchronous Processing
		/// <summary>Generates the output parameters necessary to send a new shared key to the client.</summary>
		/// <param name="transactID">The transaction ID for the client to whom we're communicating.</param>
		/// <param name="requestHeaders">Headers retrieved from the client.</param>
		/// <param name="responseMsg">Upon return, contains an empty message to be sent to the client.</param>
		/// <param name="responseHeaders">Upon return, contains the transport headers to be sent to the client.</param>
		/// <param name="responseStream">Upon return, contains an empty stream to be sent to the client.</param>
		/// <returns>Status of the server message processing (always returns Complete).</returns>
		/// <remarks>Caches the generated client information for later use.</remarks>
		private ServerProcessing MakeSharedKey(
			Guid transactID, ITransportHeaders requestHeaders,
			out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			// Generate a new shared key and iv (done as part of the constructor of the algorithm)
			SymmetricAlgorithm symmetricProvider = CryptoHelper.GetNewSymmetricProvider(_algorithm);

			// Add the transaction id and related information to the connections table.
			// We cache the entire provider object for use later on.
			ClientConnectionInfo cci = new ClientConnectionInfo(transactID, symmetricProvider);
			lock(_connections.SyncRoot) _connections[transactID.ToString()] = cci;

			// Encrypt the shared key with the sent RSA public key.  We need to make sure
			// the client actually sent us one.
			RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
			string publicKey = (string)requestHeaders[CommonHeaders.PublicKey];
			if (publicKey == null || publicKey == string.Empty) throw new SecureRemotingException("No public key found with which to encrypt the shared key.");
			
			rsaProvider.FromXmlString(publicKey); // load the public key
			byte [] encryptedKey = rsaProvider.Encrypt(symmetricProvider.Key, _oaep);
			byte [] encryptedIV = rsaProvider.Encrypt(symmetricProvider.IV, _oaep);

			// Setup the output headers and messages
			responseHeaders = new TransportHeaders();
			responseHeaders[CommonHeaders.Transaction] = ((int)SecureTransaction.SendingSharedKey).ToString();
			responseHeaders[CommonHeaders.SharedKey] = Convert.ToBase64String(encryptedKey);
			responseHeaders[CommonHeaders.SharedIV] = Convert.ToBase64String(encryptedIV);

			// There is no message to send back, but need to initialize them none-the-less.
			responseMsg = null;
			responseStream = new MemoryStream();
			
			// We're done; don't forward this on to the next sink!  Just return.
			return ServerProcessing.Complete;
		}

		/// <summary>Decrypts the incoming message from the client and sends it to the next sink.</summary>
		/// <param name="transactID">The transaction ID for the client to whom we're communicating.</param>
		/// <param name="sinkStack">A stack of channel sinks.</param>
		/// <param name="requestMsg">The message that contains the request.</param>
		/// <param name="requestHeaders">Headers sent by the client.</param>
		/// <param name="requestStream">The stream that needs to be to processed.</param>
		/// <param name="responseMsg">Response message.</param>
		/// <param name="responseHeaders">Response headers</param>
		/// <param name="responseStream">Response stream.</param>
		/// <returns>Status of the server message processing.</returns>
		public ServerProcessing ProcessEncryptedMessage(
			Guid transactID, IServerChannelSinkStack sinkStack, IMessage requestMsg, 
			ITransportHeaders requestHeaders, Stream requestStream, 
			out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			// Get the connection information about the client and update timestamp
			ClientConnectionInfo cci;
			lock(_connections.SyncRoot)
			{
				cci = (ClientConnectionInfo)_connections[transactID.ToString()];
			}
			if (cci == null) throw new SecureRemotingException("No connection information about client.");
			cci.UpdateLastUsed();

			// Decrypt the stream using the cached provider
			Stream decryptedStream = CryptoHelper.GetDecryptedStream(requestStream, cci.Provider);
			requestStream.Close(); // we won't be using it anymore now that we've decrypted it

			// Send the decrypted message on to the object (through the rest of the sink chain)
			ServerProcessing processingResult = _next.ProcessMessage(
				sinkStack, requestMsg, requestHeaders, decryptedStream,
				out responseMsg, out responseHeaders, out responseStream);

			// Take the result from the object and encrypt it
			responseHeaders[CommonHeaders.Transaction] = ((int)SecureTransaction.SendingEncryptedResult).ToString();
			Stream encryptedStream = CryptoHelper.GetEncryptedStream(responseStream, cci.Provider);
			responseStream.Close(); // close the plaintext stream now that we're done with it
			responseStream = encryptedStream;

			return processingResult;
		}

		/// <summary>Checks the connection table for previous communications with this client.</summary>
		/// <param name="transactID">Transaction ID of the client to check.</param>
		/// <returns>true if previous connection; otherwise, false.</returns>
		private bool PreviousTransactionWithClient(Guid transactID)
		{
			lock(_connections.SyncRoot)
			{
				return (!transactID.Equals(Guid.Empty) && 
					_connections[transactID.ToString()] != null);
			}
		}

		/// <summary>
		/// Creates all necessary objects to send an empty message back to the client.  
		/// Can be used to send back to the client an "Unknown Identifier" transaction type message.
		/// Note that this is a recoverable error and as such does not throw an exception.
		/// </summary>
		/// <param name="transactType">The transaction type to send to the client.</param>
		/// <param name="responseMsg">The output response message.</param>
		/// <param name="responseHeaders">The output response headers.</param>
		/// <param name="responseStream">The output response stream.</param>
		/// <returns>Status of the server message processing (Complete).</returns>
		private ServerProcessing SendEmptyToClient(
			SecureTransaction transactType, 
			out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			// Initialize all output objects and set the transaction type.
			responseMsg = null;
			responseStream = new MemoryStream();
			responseHeaders = new TransportHeaders();
			responseHeaders[CommonHeaders.Transaction] = ((int)transactType).ToString();
			return ServerProcessing.Complete;
		}

		/// <summary>Requests message processing from the current sink.</summary>
		/// <param name="sinkStack">A stack of channel sinks</param>
		/// <param name="requestMsg">Request message.</param>
		/// <param name="requestHeaders">Headers sent by client.</param>
		/// <param name="requestStream">Stream to be processed..</param>
		/// <param name="responseMsg">Response message.</param>
		/// <param name="responseHeaders">Response headers.</param>
		/// <param name="responseStream">Response stream.</param>
		/// <returns>Status of the server message processing.</returns>
		public ServerProcessing ProcessMessage(
			IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, 
			out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			// Get header information about transaction
			string strTransactID = (string)requestHeaders[CommonHeaders.ID];
			Guid transactID = (strTransactID == null ? Guid.Empty : new Guid(strTransactID));
			SecureTransaction transactType = (SecureTransaction)Convert.ToInt32((string)requestHeaders[CommonHeaders.Transaction]);

			// For reference, find out who is connecting to us.  We can use this to filter
			// and to enforce security based on client identity.
			IPAddress clientAddress = requestHeaders[CommonTransportKeys.IPAddress] as IPAddress;

			// Push this onto the sink stack
			sinkStack.Push(this, null);

			// Process the transaction based on its type (as stored in the CommonHeaders.Transaction header field)
			ServerProcessing processingResult;
			switch(transactType)
			{
				case SecureTransaction.SendingPublicKey:
					// We've received a request from a new client asking for a shared key (by sending us
					// his RSA public key).  Create a shared key, encrypt it, and send it back to him.
					processingResult = MakeSharedKey(transactID, requestHeaders, out responseMsg, out responseHeaders, out responseStream);
					System.Diagnostics.Debug.WriteLine("Connection added: " + transactID);
					break;

				case SecureTransaction.SendingEncryptedMessage:
					// We've received an encrypted message.  Decrypt it and send it along to the next sink.
					// But first make sure we have a record of the transaction.
					if (PreviousTransactionWithClient(transactID)) 
					{
						processingResult = ProcessEncryptedMessage(
							transactID, sinkStack, 
							requestMsg, requestHeaders, requestStream, 
							out responseMsg, out responseHeaders, out responseStream);
					} 
						// Otherwise, let the client know that we don't recognize him.
					else 
					{
						processingResult = SendEmptyToClient(
							SecureTransaction.UnknownIdentifier, 
							out responseMsg, out responseHeaders, out responseStream);
						System.Diagnostics.Debug.WriteLine("Unknown connection: " + transactID);
					}
					break;

				case SecureTransaction.Uninitialized:
					// The transaction type did not match any known type, or wasn't specified.  
					// So just pass on the message to the next sink.  This shouldn't happen 
					// unless the client isn't using the SecureClientSink provider, in which 
					// case this is the correct behavior.
					if (!RequireSecurity(clientAddress))
					{
						processingResult = _next.ProcessMessage(
							sinkStack, requestMsg, requestHeaders, requestStream,
							out responseMsg, out responseHeaders, out responseStream);
					} 
						// If the server has elected not to allow plaintext traffic, let the
						// client know that we're not happy.
					else throw new SecureRemotingException("Server requires a secure connection for this client");
					break;

				default:
					// Houston, we have a problem!
					throw new SecureRemotingException("Invalid request from client: " + transactType + ".");
			}

			// Take us off the stack and return the result.
			sinkStack.Pop(this);
			return processingResult;
		}

		/// <summary>Determine whether we'll require encryption when communicating with the given endpoint.</summary>
		/// <param name="clientAddress">The address to check for security restrictions.</param>
		/// <returns>Whether communications with this client must be encrypted.</returns>
		private bool RequireSecurity(IPAddress clientAddress)
		{
			// If there are no exemptions, return the general case
			if (clientAddress == null ||
				_securityExemptionList == null || _securityExemptionList.Length == 0) 
			{
				return _requireSecurity;
			}

			// Otherwise, we need to check each of the address lists
			bool found = false;
			foreach(IPAddress address in _securityExemptionList)
			{
				if (clientAddress.Equals(address))
				{
					found = true;
					break;
				}
			}

			// If the item was found, then we need to do the opposite of the general case.
			// Otherwise, we do what's done in the general case.  The result that follows
			// is equivalent to "found XOR _requireSecurity"
			return found ? !_requireSecurity : _requireSecurity;
		}

		/// <summary>Gets the next server channel sink in the server sink chain.</summary>
		public IServerChannelSink NextChannelSink
		{
			get { return _next; }
		}

		/// <summary>Returns the Stream onto which the provided response message is to be serialized.</summary>
		public Stream GetResponseStream(
			IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			// Always return null
			return null;
		}
		#endregion

		#region Asynchronous Processing
		/// <summary>Requests processing from the current sink of the response from a method call sent asynchronously.</summary>
		public void AsyncProcessResponse(
			IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			// In v1.0 of the framework, asynchronous processing is not supported on the server in custom channel sinks.
			throw new NotSupportedException();
		}
		#endregion

		#region Connection Cleanup
		/// <summary>Starts the connection sweeper.</summary>
		private void StartConnectionSweeper()
		{
			// Create and start a timer that will execute the SweepConnections method 
			// every "_sweepFrequency" seconds.
			if (_sweepTimer == null) 
			{
				_sweepTimer = new System.Timers.Timer(_sweepFrequency*1000);
				_sweepTimer.Elapsed += new ElapsedEventHandler(SweepConnections);
				_sweepTimer.Start();
			}
		}

		/// <summary>Removes from the connection table any outdated connection information.</summary>
		/// <remarks>
		/// When the table is sweeped, the table is locked to prevent it from being modified during the
		/// sweep which could cause exceptions to be thrown.  The downside to this is that while the
		/// table is locked, incoming requests will be blocked!  As such, either this should be rewritten
		/// or the frequency of the sleep should be limited.  Idle checks could also be implemented to
		/// ensure that the process runs only when a long idle period has been observed.
		/// </remarks>
		private void SweepConnections(object sender, ElapsedEventArgs e)
		{
			// We lock it because we want all checks and deletions to be atomic.
			// If anyone tries to access the hashtable during the sweep, they'll
			// have to wait.
			lock (_connections.SyncRoot) 
			{
				ArrayList toDelete = new ArrayList(_connections.Count);

				// Find all entries that need to be deleted
				foreach(DictionaryEntry entry in _connections) 
				{
					ClientConnectionInfo cci = (ClientConnectionInfo)entry.Value;
					if (cci.LastUsed.AddSeconds(_connectionAgeLimit).CompareTo(DateTime.UtcNow) < 0) 
					{
						toDelete.Add(entry.Key);
						((IDisposable)cci).Dispose(); // Dispose of the connection
						System.Diagnostics.Debug.WriteLine("Removing connection: " + cci.TransactID);
					}
				}

				// Delete the out-of-date entries found above
				foreach(Object obj in toDelete) _connections.Remove(obj);
				toDelete = null;
			}
		}
		#endregion
	}
}
