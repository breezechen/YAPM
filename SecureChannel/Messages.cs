// Stephen Toub
// stoub@microsoft.com
// Messages.cs

using System;
using System.Security.Cryptography;

namespace MsdnMag.Remoting
{
	/// <summary>Names of transport headers used by client and server.</summary>
	internal class CommonHeaders
	{
		#region Names of Headers
		/// <summary>Header to hold the id of the client.</summary>
		public const string ID = "sc_TransactionID";
		/// <summary>Header to hold the SecureTransaction state.</summary>
		public const string Transaction = "sc_TransactionType";
		/// <summary>Header to hold the RSA public key.</summary>
		public const string PublicKey = "sc_PublicKey";
		/// <summary>Header to hold the encrypted shared key.</summary>
		public const string SharedKey = "sc_SharedKey";
		/// <summary>Header to hold the encrypted IV.</summary>
		public const string SharedIV = "sc_SharedIV";
		#endregion
	}

	/// <summary>Enumeration of handshake and secure transaction message types.</summary>
	internal enum SecureTransaction
	{
		#region States of a Transaction
		/// <summary>
		/// An uninitialized transaction; no information is being sent.
		/// </summary>
		/// <remarks>
		/// This is explicitly set to 0 (even though in its current position it
		/// will automatically have the value of 0) to prevent future mistakes should
		/// the order of the items in the enumeration be rearranged.
		/// The system counts on Uninitialized being 0 because Convert.ToInt32() returns
		/// 0 for a null string.  We can then be sure that if Convert.ToInt32() on the
		/// respective header returns Uninitialized, either it was explicitly set
		/// to 0 or no header was set at all.  Regardless, it cuts down on headaches.
		/// </remarks>
		Uninitialized=0,
		/// <summary>The client is sending a public key to the server.</summary>
		SendingPublicKey,
		/// <summary>The server is sending an encrypted shared key to the client.</summary>
		SendingSharedKey,
		/// <summary>The client is sending an encrypted request to the server.</summary>
		SendingEncryptedMessage,
		/// <summary>The server is sending an encrypted response to the client.</summary>
		SendingEncryptedResult,
		/// <summary>The server does not recognize the client's identification.</summary>
		UnknownIdentifier
		#endregion
	}
	
	/// <summary>Contains connection information for a specific client sink in contact with the server.</summary>
	internal class ClientConnectionInfo : IDisposable
	{
		#region Member Variables
		/// <summary>Transaction ID for indentifying the client.</summary>
		private Guid _transactID;
		/// <summary>Provider to use to encrypt communication with client.</summary>
		private SymmetricAlgorithm _provider;
		/// <summary>The time of the last communication with this client.</summary>
		private DateTime _lastUsed;
		/// <summary>Determines whether the object has been disposed.</summary>
		private bool _disposed = false;
		#endregion

		#region Construction and Finalization
		/// <summary>Initialize the ClientConnectionInfo object.</summary>
		/// <param name="transactID">The client's identification.</param>
		/// <param name="provider">The provider used to encrypt communication with this client.</param>
		public ClientConnectionInfo(Guid transactID, SymmetricAlgorithm provider) 
		{
			_transactID = transactID; 
			_provider = provider;
			_lastUsed = DateTime.UtcNow;
		}

		/// <summary>Disposes of the connection information.</summary>
		~ClientConnectionInfo()
		{
			// Free up resources (false because we don't want GC.SuppressFinalize called)
			Dispose(false);
		}
		#endregion

		#region Methods
		/// <summary>Changes the last used time to reflect the current time.</summary>
		public void UpdateLastUsed()
		{
			CheckDisposed();
			_lastUsed = DateTime.UtcNow;
		}

		/// <summary>Dispose of the connection information.</summary>
		void IDisposable.Dispose()
		{
			// Free up resources (true because we want to suppress finalization)
			Dispose(true);
		}

		/// <summary>Dispose of the connection information.</summary>
		/// <param name="disposing">Whether finalization should be suppressed.</param>
		protected void Dispose(bool disposing)
		{
			// If the object hasn't been disposed of yet
			if (!_disposed) 
			{
				// Free up resources
				if (_provider != null) ((IDisposable)_provider).Dispose();

				// If dispose is not called from the finalizer, suppress finalization
				if (disposing) GC.SuppressFinalize(this);
			}
		}

		/// <summary>Throws an exception if the connection has already been disposed.</summary>
		private void CheckDisposed()
		{
			if (_disposed) throw new ObjectDisposedException("ClientConnectionInfo");
		}
		#endregion

		#region Properties
		/// <summary>Gets the transaction id for this client.</summary>
		public Guid TransactID 
		{
			get 
			{ 
				CheckDisposed();
				return _transactID; 
			}
		}
		
		/// <summary>Gets the provider to use with this client.</summary>
		/// <remarks>
		/// Use this provider only for transformations based on its existing key and iv.
		/// Do not call GenerateKey or GenerateIV on it.
		/// </remarks>
		public SymmetricAlgorithm Provider
		{
			get 
			{ 
				CheckDisposed();
				return _provider; 
			}
		}

		/// <summary>Gets the timestamp for the last communication with this client.</summary>
		public DateTime LastUsed
		{
			get 
			{ 
				CheckDisposed();
				return _lastUsed; 
			}
		}
		#endregion
	}
}
