// Stephen Toub
// stoub@microsoft.com
// CryptoHelper.cs

using System;
using System.IO;
using System.Security.Cryptography;

namespace MsdnMag.Remoting
{
	/// <summary>Helper functions for working with encryption and streams.</summary>
	public class CryptoHelper
	{
		#region Member Variables
		/// <summary>Size to use for byte buffers when performing IO.</summary>
		private const int _bufferSize = 2048;
		#endregion

		#region Construction
		/// <summary>Prevent external instantiation.  Class only has static helpers.</summary>
		private CryptoHelper() {}
		#endregion

		#region Creating Providers
		/// <summary>Factory for symmetric algorithm providers.  Creates a new provider by name.</summary>
		/// <param name="algorithm">The name of the algorithm to use (e.g. "DES")</param>
		/// <returns>A SymmetricAlgorithm provider to be used for communication
		/// between client and server.</returns>
		/// <remarks>Currently supports "DES", "3DES", "RIJNDAEL", and "RC2".</remarks>
		public static SymmetricAlgorithm GetNewSymmetricProvider(string algorithm)
		{
			// Return a provider based on the chosen algorithm
			switch(algorithm.Trim().ToLower())
			{
				case "3des": return new TripleDESCryptoServiceProvider();
				case "rijndael": return new RijndaelManaged();
				case "rc2": return new RC2CryptoServiceProvider();
				case "des": return new DESCryptoServiceProvider();
				default: throw new ArgumentException("Provider must be '3DES', 'DES', 'RIJNDAEL', or 'RC2'.", "algorithm");
			}
		}
		#endregion

		#region Encryption and Decryption Helpers
		/// <summary>
		/// Encrypts a stream with the specified symmetric provider.  The returned stream
		/// is at position zero and ready to be read.
		/// </summary>
		/// <param name="inStream">The stream to encrypt.</param>
		/// <param name="provider">The cryptographic provider to use for encryption.</param>
		/// <returns>Encrypted stream ready to be read.</returns>
		public static Stream GetEncryptedStream(Stream inStream, SymmetricAlgorithm provider) 
		{
			// Make sure we got valid input
			if (inStream == null) throw new ArgumentNullException("Invalid stream.", "inStream");
			if (provider == null) throw new ArgumentNullException("Invalid provider.", "provider");

			// Create the output stream
			MemoryStream outStream = new MemoryStream();
			CryptoStream encryptStream = new CryptoStream(outStream, provider.CreateEncryptor(), CryptoStreamMode.Write);

			// Encrypt the stream by reading all bytes from the input stream and
			// writing them to the output encryption stream.  Note that we're depending
			// on the fact that ~CryptoStream does not close the underlying stream.
			int numBytes;
			byte [] inputBytes = new byte[_bufferSize];
			while((numBytes = inStream.Read(inputBytes, 0, inputBytes.Length)) != 0) 
			{
				encryptStream.Write(inputBytes, 0, numBytes);
			}
			encryptStream.FlushFinalBlock();

			// Go back to the beginning of the newly encrypted stream and return it
			outStream.Position = 0;
			return outStream;
		}

		
		/// <summary>
		/// Decrypts a stream with the specified symmetric provider.
		/// </summary>
		/// <param name="inStream">The stream to decrypt.</param>
		/// <param name="provider">The cryptographic provider to use for encrypting.</param>
		/// <returns>Plaintext stream ready to be read.</returns>
		public static Stream GetDecryptedStream(Stream inStream, SymmetricAlgorithm provider) 
		{
			// Make sure we got valid input
			if (inStream == null) throw new ArgumentNullException("Invalid stream.", "inStream");
			if (provider == null) throw new ArgumentNullException("Invalid provider.", "provider");

			// Create the input and output streams
			CryptoStream decryptStream = new CryptoStream(inStream, provider.CreateDecryptor(), CryptoStreamMode.Read);
			MemoryStream outStream = new MemoryStream();
			
			// Read the stream and write it to the output. Note that we're depending
			// on the fact that ~CryptoStream does not close the underlying stream.
			int numBytes;
			byte [] inputBytes = new byte[_bufferSize];
			while((numBytes = decryptStream.Read(inputBytes, 0, inputBytes.Length)) != 0) 
			{
				outStream.Write(inputBytes, 0, numBytes);
			}

			// Go to the beginning of the decrypted stream and return it
			outStream.Position = 0;
			return outStream;
		}
		#endregion
	}
}
