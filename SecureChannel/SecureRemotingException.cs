// Stephen Toub
// stoub@microsoft.com
// SecureRemotingException.cs

using System;
using System.Security.Permissions;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace MsdnMag.Remoting
{
	/// <summary>The exception that is thrown when something goes wrong in the secure remoting channel.</summary>
	[Serializable]
	public class SecureRemotingException : RemotingException, ISerializable
	{
		#region Construction and Serialization
		/// <summary>Initializes a new instance of the SecureRemotingException class with default properties.</summary>
		public SecureRemotingException()
		{
		}
		
		/// <summary>Initializes a new instance of the SecureRemotingException class with the given message.</summary>
		/// <param name="message">The error message that explains why the exception occurred.</param>
		public SecureRemotingException(string message) : 
			base(message)
		{
		}

		/// <summary>Initializes a new instance of the SecureRemotingException class with the specified properties.</summary>
		/// <param name="message">The error message that explains why the exception occurred.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public SecureRemotingException(string message, System.Exception innerException) : 
			base(message, innerException)
		{
		}

		/// <summary>Initializes the exception with serialized information.</summary>
		/// <param name="info">Serialization information.</param>
		/// <param name="context">Streaming context.</param>
		protected SecureRemotingException(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
		}

		/// <summary>Provides serialization functionality.</summary>
		/// <param name="info">Serialization information.</param>
		/// <param name="context">Streaming context.</param>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
		#endregion
	}
}
