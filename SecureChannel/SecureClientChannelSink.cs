// Stephen Toub
// stoub@microsoft.com
// SecureClientChannelSink.cs

using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Security.Cryptography;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

// PERFORMANCE NOTE: 
// For ease of implementation, SecureClientChannelSink can take out some pretty big locks.
// Not a problem for demo purposes, but if this will be used as the basis for any kind of
// production level code, synchronization needs to reviewed and possibly greatly revamped.

namespace MsdnMag.Remoting
{
	/// <summary>
	/// Client channel sink that, in conjunction with SecureServerChannelSink, provides an
	/// asymmetric key exchange and shared key encryption across a remoting channel.
	/// </summary>
	internal class SecureClientChannelSink : 
		BaseChannelSinkWithProperties, IClientChannelSink
	{
		#region Member Variables
		/// <summary>The name of the symmetric algorithm to use.</summary>
		private readonly string _algorithm;
		/// <summary>Whether OAEP padding should be used.</summary>
		private readonly bool _oaep;
		/// <summary>The maximum number of times we should attempt to process the message.</summary>
		private readonly int _maxAttempts;
		/// <summary>Reference to the next sink in the sink chain.</summary>
		private readonly IClientChannelSink _next;
		/// <summary>The transaction ID to identify this client to the server.</summary>
		private Guid _transactID = Guid.Empty;
		/// <summary>
		/// The symmetric algorithm provider to be used for all transactions from this client.
		/// Note that all connections to objects from this client will use the same provider.
		/// The server, on the other hand, will use a different provider (a different key) for
		/// each connected client (though it, too, will use the same provider for all messages
		/// from the same client, regardless of destination object).
		/// </summary>
		private volatile SymmetricAlgorithm _provider = null;
		/// <summary>RSA provider used for encryption and decryption of shared key information.</summary>
		private volatile RSACryptoServiceProvider _rsaProvider = null;
		/// <summary>Used to take out a lock on transaction id and provider.</summary>
		private readonly object _transactionLock = null;
		/// <summary>Text for generic secure remoting exception.</summary>
		private const string _defaultExceptionText = "The client sink is unable to maintain a secure channel with server.";
		#endregion

		#region Construction
		/// <summary>Initialize the secure channel sink.</summary>
		/// <param name="nextSink">The next sink in the chain.</param>
		/// <param name="algorithm">The name of the symmetric algorithm to use for encryption.</param>
		/// <param name="oaep">Whether OAEP padding should be used for asymmetric encryption.</param>
		/// <param name="maxAttempts">The maximum number of times we should attempt to process the message.</param>
		public SecureClientChannelSink(
			IClientChannelSink nextSink, 
			string algorithm, bool oaep, int maxAttempts)
		{ 
			_algorithm = algorithm;
			_oaep = oaep;
			_next = nextSink;
			_maxAttempts = maxAttempts;
			_transactionLock = new object();
			_rsaProvider = new RSACryptoServiceProvider();
		}
		#endregion

		#region Synchronous Processing
		/// <summary>Adds the headers for a shared-key request.</summary>
		/// <param name="requestHeaders">Output headers for the request.</param>
		private void CreateSharedKeyRequest(ITransportHeaders requestHeaders)
		{
			// Generate an RSA provider/key
			string rsaKey = _rsaProvider.ToXmlString(false);

			// Include client information and the public key
			requestHeaders[CommonHeaders.Transaction] = ((int)SecureTransaction.SendingPublicKey).ToString();
			requestHeaders[CommonHeaders.ID] = _transactID.ToString();
			requestHeaders[CommonHeaders.PublicKey] = rsaKey;
		}

		/// <summary>Decrypts the incoming response given the response stream and headers.</summary>
		/// <param name="responseStream">The response stream containing the response information.</param>
		/// <param name="responseHeaders">The response headers containing the response header information.</param>
		/// <returns>The decrypted stream if possible; null, otherwise.</returns>
		private Stream DecryptResponse(Stream responseStream, ITransportHeaders responseHeaders)
		{
			try 
			{
				// Check to make sure that the server is sending back to us the encrypted results.
				// If it is, grab the results and return the decrypted stream.  Otherwise, return a null stream.
				if (responseHeaders != null &&
					SecureTransaction.SendingEncryptedResult == 
					(SecureTransaction)Convert.ToInt32((string)responseHeaders[CommonHeaders.Transaction])) 
				{
					Stream decryptedStream = CryptoHelper.GetDecryptedStream(responseStream, _provider);
					responseStream.Close(); // close the old stream as we won't be using it anymore
					return decryptedStream;
				} 
			} 
			catch{}
			return null;
		}

		/// <summary>Processes response transport headers for a shared-key request.</summary>
		/// <param name="responseHeaders">The headers from a shared-key request.</param>
		/// <returns>A SymmetricAlgorithm with the key information sent from the server.</returns>
		private SymmetricAlgorithm ProcessSharedKeyResponse(ITransportHeaders responseHeaders)
		{
			// Grab the returned shared key and IV (encrypted with our public key)
			string encryptedKey = (string)responseHeaders[CommonHeaders.SharedKey];
			string encryptedIV = (string)responseHeaders[CommonHeaders.SharedIV];
			if (encryptedKey == null || encryptedKey == string.Empty) throw new SecureRemotingException("Expected shared key from server.");
			if (encryptedIV == null || encryptedIV == string.Empty) throw new SecureRemotingException("Expected shared IV from server.");

			// Generate the encryption objects
			SymmetricAlgorithm sharedProvider = CryptoHelper.GetNewSymmetricProvider(_algorithm);
			sharedProvider.Key = _rsaProvider.Decrypt(Convert.FromBase64String(encryptedKey), _oaep);
			sharedProvider.IV = _rsaProvider.Decrypt(Convert.FromBase64String(encryptedIV), _oaep);
			return sharedProvider;
		}

		/// <summary>
		/// Creates an RSA key pair.  Sends a message to the server secure sink which includes
		/// the public key from the pair along with a newly created GUID to identify this client
		/// to the server.  The server responds with an encrypted shared key which can be used for
		/// further communications between this client and server.
		/// </summary>
		/// <param name="msg">The original message passed to the sink.</param>
		/// <returns>Byte array containing shared key</returns>
		private SymmetricAlgorithm ObtainSharedKey(IMessage msg)
		{
			// We create new headers and a new stream for this roundtrip to the server.
			// We don't need to use any headers that may have already been added to the collection
			// because we can reasonably assume that they are destined for the server side sink paired
			// with the client sink that created them; we're stopping at our matching server side
			// sink and as sink order is most-likely mirrored on the server, any existing headers won't be
			// used anyway (unless our assumption is incorrect and they are actually destined
			// for an unrelated sink, but if that's the case, oh well).
			TransportHeaders requestHeaders = new TransportHeaders();
			MemoryStream requestStream = new MemoryStream();
			ITransportHeaders responseHeaders;
			Stream responseStream;

			// Create the headers and stream for a shared-key request
			CreateSharedKeyRequest(requestHeaders);

			// Send the message.  We do this by sending the message along the sink chain
			// as if this were the real message.
			_next.ProcessMessage(msg, requestHeaders, requestStream, out responseHeaders, out responseStream);
			
			// Processes the response headers and pulls from them a symmetric algorithm
			return ProcessSharedKeyResponse(responseHeaders);
		}

		/// <summary>Clears out the shared key and connection information.</summary>
		/// <remarks>Should always be called inside a lock on _transactionLock.</remarks>
		private void ClearSharedKey()
		{
			_provider = null;
			_transactID = Guid.Empty;
		}

		/// <summary>Sets up the stream and headers for the encrypted message</summary>
		/// <param name="requestHeaders">The headers to be sent to the server containing connection information.</param>
		/// <param name="requestStream">The stream to be encrypted.</param>
		/// <returns>The encrypted stream to be sent to the server.</returns>
		private Stream SetupEncryptedMessage(ITransportHeaders requestHeaders, Stream requestStream)
		{
			// Encrypt the message
			requestStream = CryptoHelper.GetEncryptedStream(requestStream, _provider);

			// Setup the header information.  The server is semi-stateless in that it can serve
			// multiple clients at the same time.  As such, we need to tell it who we are and what
			// we're doing.
			requestHeaders[CommonHeaders.Transaction] = ((int)SecureTransaction.SendingEncryptedMessage).ToString();
			requestHeaders[CommonHeaders.ID] = _transactID.ToString();

			// Return the encrypted message in a stream
			return requestStream;
		}
		
		/// <summary>
		/// Given a request stream, encrypts the stream with the shared key and sends
		/// the encrypted message to the server.  The server responds with an encrypted response
		/// stream which is decrypted.  This response stream is handed back to the caller.
		/// </summary>
		/// <param name="msg">The original message passed to the sink.</param>
		/// <param name="requestHeaders">The original request headers passed to the sink.</param>
		/// <param name="requestStream">The original request stream passed to the sink.</param>
		/// <param name="responseHeaders">Output response headers.</param>
		/// <param name="responseStream">Output response stream.</param>
		/// <returns>true if success; false, otherwise.</returns>
		private bool ProcessEncryptedMessage(
			IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, 
			out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			// Encrypt the message.  We track the id of the transaction so that we know 
			// whether the key has changed between encryption and decryption.  If it has, 
			// we have a problem and need to try again.
			Guid id;
			lock(_transactionLock) 
			{
				id = EnsureIDAndProvider(msg, requestHeaders);
				requestStream = SetupEncryptedMessage(requestHeaders, requestStream);
			}

			// Send the encrypted request to the server
			_next.ProcessMessage(
				msg, requestHeaders, requestStream, 
				out responseHeaders, out responseStream);

			// Decrypt the response stream.  If decryption fails, and if no one has changed the 
			// transaction key (meaning someone else already had a problem and tried to fix 
			// it by clearing it out), then we need to clear it out such that it will be 
			// updated the next time through.
			lock(_transactionLock)
			{
				responseStream = DecryptResponse(responseStream, responseHeaders);
				if (responseStream == null && id.Equals(_transactID)) ClearSharedKey();
			}

			// Return whether we were successful
			return responseStream != null;
		}

		/// <summary>Ensures that we've obtained shared-key information and a transaction ID.</summary>
		/// <param name="msg">The message to process.</param>
		/// <param name="requestHeaders">The headers to send to the server.</param>
		/// <returns>The transaction ID.</returns>
		/// <remarks>
		/// May require a synchronous roundtrip to the server.
		/// Should always be called inside a lock on _transactionLock.
		/// </remarks>
		private Guid EnsureIDAndProvider(IMessage msg, ITransportHeaders requestHeaders)
		{
				// If there is no transaction guid, create one.
				// If we haven't yet received a shared key, get one.
				if (_provider == null || _transactID.Equals(Guid.Empty))
				{
					_transactID = Guid.NewGuid();
					_provider = ObtainSharedKey(msg); // roundtrip to server sink
				}
				return _transactID;
		}

		/// <summary>Requests message processing from the current sink.</summary>
		/// <param name="msg">The message to process.</param>
		/// <param name="requestHeaders">The headers to send to the server.</param>
		/// <param name="requestStream">The stream to process and send to the server.</param>
		/// <param name="responseHeaders">Response headers from the server.</param>
		/// <param name="responseStream">Response stream from the server.</param>
		/// <exception cref="SecureRemotingException">Thrown if a connection cannot be maintained with the server.</exception>
		public void ProcessMessage(
			IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, 
			out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			try 
			{
				// Store the initial position on the stream so that we can restore
				// our position and try again if the transaction fails.
				long initialStreamPos = requestStream.CanSeek ? requestStream.Position : -1;

				// Try multiple times, up to the max specified in the web.config.
				for(int i=0; i<_maxAttempts; i++) 
				{
					// Process the encrypted message.
					if (ProcessEncryptedMessage(
						msg, requestHeaders, requestStream, 
						out responseHeaders, out responseStream)) return;

					// We failed for some reason, but we can try again.  If we can't reset the initial
					// position of the stream, we'll just give up.  We could of course buffer the whole
					// input stream first and then use that through the process, but is it worth it? Nah.
					if (requestStream.CanSeek) requestStream.Position = initialStreamPos;
					else break;
				}

				// If we made it this far, we're in trouble, as this means that we were unable to successfully
				// send and receive an encrypted message, most likely due to the server not being
				// able to identify us.
				throw new SecureRemotingException(_defaultExceptionText);
			}
			finally
			{
				// Close the initial request stream just in case it hasn't been.
				// After all, we're not sending it to anyone.
				requestStream.Close();
			}
		}
		
		/// <summary>Returns the Stream onto which the provided message is to be serialized.</summary>
		/// <param name="msg">The message being sent.</param>
		/// <param name="headers">The headers being sent to the server.</param>
		/// <returns>The stream onto which the provided message is to be serialized.</returns>
		public Stream GetRequestStream(System.Runtime.Remoting.Messaging.IMessage msg, System.Runtime.Remoting.Channels.ITransportHeaders headers)
		{
			return null;
		}

		/// <summary>Returns the next channel sink in the sink chain.</summary>
		public IClientChannelSink NextChannelSink
		{
			get { return _next; }
		}
		#endregion

		#region Asynchronous Processing
		/// <summary>Stores information on the current request; used in case an async request fails.</summary>
		private class AsyncProcessingState
		{
			#region Member Variables
			/// <summary>The input stream.</summary>
			private Stream _stream;
			/// <summary>The transport headers.</summary>
			private ITransportHeaders _headers;
			/// <summary>The remoted message.</summary>
			private IMessage _msg;
			/// <summary>Transaction ID when processing started.</summary>
			private Guid _id;
			#endregion

			#region Construction
			/// <summary>Initialize the state.</summary>
			/// <param name="msg">The message to be stored.</param>
			/// <param name="headers">The transport headers to be stored.</param>
			/// <param name="stream">The stream to be stored (copies the stream).</param>
			/// <param name="id">Transaction ID when processing started.</param>
			public AsyncProcessingState(
				IMessage msg, ITransportHeaders headers, ref Stream stream, Guid id)
			{
				_msg = msg;
				_headers = headers;
				_stream = DuplicateStream(ref stream);
				_id = id;
			}
			#endregion

			#region Properties
			/// <summary>Gets the input stream.</summary>
			public Stream Stream { get { return _stream; } }
			/// <summary>Gets the transport headers.</summary>
			public ITransportHeaders Headers { get { return _headers; } }
			/// <summary>Gets the remoted message.</summary>
			public IMessage Message { get { return _msg; } }
			/// <summary>Gets the transaction id from when the transaction started.</summary>
			public Guid ID { get { return _id; } }
			#endregion

			#region Methods
			/// <summary>Duplicates the stream.</summary>
			/// <param name="stream">The stream to be duplicated.</param>
			/// <returns>A copy of the stream.</returns>
			/// <remarks>
			/// Since we can't guarantee that Position will work on the input stream, we need
			/// to create a new stream and set the old reference to a copy of the new one.
			/// </remarks>
			private Stream DuplicateStream(ref Stream stream)
			{
				// TODO: Test if Position will work and use that instead if it will

				// Create two new streams, one to act as the duplicate and one to replace
				// the original.
				MemoryStream memStream1 = new MemoryStream();
				MemoryStream memStream2 = new MemoryStream();
				
				// Copy the old stream to the new streams
				byte [] buffer = new byte[1024];
				int read;
				while((read = stream.Read(buffer, 0, buffer.Length)) > 0) 
				{
					memStream1.Write(buffer, 0, read);
					memStream2.Write(buffer, 0, read);
				}
				stream.Close();

				// Reset the new ones to be at the beginning
				memStream1.Position = 0;
				memStream2.Position = 0;

				// Reset the original reference and return the duplicate
				stream = memStream1;
				return memStream2;
			}
			#endregion
		}

		/// <summary>Requests asynchronous processing of a method call on the current sink.</summary>
		/// <param name="sinkStack">A stack of channel sinks.</param>
		/// <param name="msg">The message to process.</param>
		/// <param name="headers">The headers to send to the server.</param>
		/// <param name="stream">The stream headed to the transport sink.</param>
		public void AsyncProcessRequest(
			IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			AsyncProcessingState state = null;
			Stream encryptedStream = null;
			Guid id;

			lock(_transactionLock) // could be a big lock... probably a faster way, but for now suffices
			{
				// Establish connection information with the server; the roundtrip will hopefully
				// only be done once, so we ensure that we have the necessary information.
				id = EnsureIDAndProvider(msg, headers);
			
				// Protect ourselves a bit.  If the asynchronous call fails because the server forgot about
				// us, we'll be in a bit of a fix.  We'll need the current arguments as we'll need
				// to retry the request synchronously.  Store them into a state object and use
				// that as the state when pushing ourself onto the stack.  That way, AsyncProcessResponse
				// have access to it.
				state = new AsyncProcessingState(msg, headers, ref stream, id);
			
				// Send encrypted message by encrypting the stream
				encryptedStream = SetupEncryptedMessage(headers, stream);
			}

			// Push ourselves onto the stack with the necessary state and forward on to the next sink
			sinkStack.Push(this, state);
			_next.AsyncProcessRequest(sinkStack, msg, headers, encryptedStream);
		}

		/// <summary>Requests asynchronous processing of a response to a method call on the current sink.</summary>
		/// <param name="sinkStack">A stack of sinks that called this sink.</param>
		/// <param name="state">Information generated on the request side that is associated with this sink.</param>
		/// <param name="headers">The headers retrieved from the server response stream.</param>
		/// <param name="stream">The stream coming back from the transport sink.</param>
		public void AsyncProcessResponse(
			IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
		{
			// Get the async state we put on the stack
			AsyncProcessingState asyncState = (AsyncProcessingState)state;

			try
			{
				// Decrypt the response if possible
				SecureTransaction transactType = (SecureTransaction)Convert.ToInt32((string)headers[CommonHeaders.Transaction]);
				switch(transactType) 
				{
						// The only valid value; the server is sending results encrypted,
						// so we need to decrypt them
					case SecureTransaction.SendingEncryptedResult:
						lock(_transactionLock)
						{
							if (asyncState.ID.Equals(_transactID)) stream = DecryptResponse(stream, headers);
							else throw new SecureRemotingException("The key has changed since the message was decrypted.");
						}
						break;

						// The server has no record of the client, so error out.
						// If this were synchronous, we could try again, first renewing
						// the connection information.  The best we can do here is null
						// out our current connection information so that the next time
						// through we'll create a new provider and identifier.
					case SecureTransaction.UnknownIdentifier:
						throw new SecureRemotingException(
							"The server sink was unable to identify the client, " +
							"most likely due to the connection information timing out.");

						// Something happened and the response is not encrypted, i.e. there
						// are no transport headers, or at least no transaction header, or it has
						// been explicitly set by the server to Uninitialized. 
						// Regardless, do nothing.
					default:
					case SecureTransaction.Uninitialized:
						break;
				}
			}
			catch(SecureRemotingException)
			{
				// We got back a secure remoting exceptionIt would be difficult to retry this as an
				// asynchronous call as we need to have the output ready to go before
				// we return.  Thus, we'll make a synchronous one by calling ProcessMessage()
				// just as if we were the previous sink.  Luckily, we kept all of the
				// necessary information sitting around.
				lock(_transactionLock) // This is a big, big lock, as are many locks in this app!  Oh well.
				{
					if (_provider == null || asyncState.ID.Equals(_transactID)) ClearSharedKey();
					ProcessMessage(
						asyncState.Message, asyncState.Headers, asyncState.Stream,
						out headers, out stream);
				}
			}
			finally
			{
				// Close the old stream just in case it hasn't been closed yet.
				asyncState.Stream.Close();
			}

			// Process through the rest of the sinks.
			sinkStack.AsyncProcessResponse(headers, stream);
		}
		#endregion
	}
}