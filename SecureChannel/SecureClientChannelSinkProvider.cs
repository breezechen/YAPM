// Stephen Toub
// stoub@microsoft.com
// SecureClientChannelSinkProvider.cs

using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace MsdnMag.Remoting
{
	/// <summary>Used to create a secure client channel sink.</summary>
	public class SecureClientChannelSinkProvider : IClientChannelSinkProvider
	{
		#region Member Variables
		/// <summary>Reference to the next sink provider in the provider chain.</summary>
		private IClientChannelSinkProvider _next = null;
		/// <summary>The name of the symmetric algorithm to use.</summary>
		private string _algorithm = "DES";
		/// <summary>Whether OAEP padding should be used.</summary>
		private bool _oaep = false;
		/// <summary>The maximum number of times we should attempt to process a message.</summary>
		private int _maxAttempts = 2; // includes first try and one retry
		#endregion

		#region Construction
		/// <summary>Initialize the sink provider.</summary>
		public SecureClientChannelSinkProvider()
		{
			// uses defaults
		}

		/// <summary>Initialize the sink provider.</summary>
		/// <param name="properties">Parameters specified in the config file.</param>
		/// <param name="providerData">Sink provider data.</param>
		public SecureClientChannelSinkProvider(IDictionary properties, ICollection providerData) 
		{
			// Read in web.config parameters
			foreach (DictionaryEntry entry in properties)
			{
				switch ((String)entry.Key)
				{
					case "algorithm": 
						_algorithm = (string)entry.Value; 
						break;

					case "oaep": 
						_oaep = bool.Parse((string)entry.Value); 
						break;

					case "maxRetries": 
						_maxAttempts = Convert.ToInt32((string)entry.Value); 
						if (_maxAttempts < 1) throw new ArgumentException("Maximum number of attempts must be at least 1.", "maxAttempts");
						_maxAttempts++; // number of attempts should include the first try
						break;

					default: 
						throw new ArgumentException("Invalid configuration entry: " + (String)entry.Key);
				}
			}
		}
		#endregion

		#region Sink Creation
		/// <summary>Creates a sink chain.</summary>
		/// <param name="channel">Channel for which the current sink chain is being constructed.</param>
		/// <param name="url">The URL of the object to connect to.</param>
		/// <param name="remoteChannelData">A channel data object describing a channel on the remote server.</param>
		/// <returns>A reference to the new sink, or null if it could not be created.</returns>
		public System.Runtime.Remoting.Channels.IClientChannelSink CreateSink(System.Runtime.Remoting.Channels.IChannelSender channel, string url, object remoteChannelData)
		{
			IClientChannelSink nextSink = null;

			if (_next != null)
			{
				// Call CreateSink on the next sink provier in the chain.  This will return
				// to us the actual next sink object.  If the next sink is null, uh oh!
				if ((nextSink = _next.CreateSink(channel, url, remoteChannelData)) == null) return null;
			}

			// Create this sink, passing to it the previous sink in the chain so that it knows
			// to whom messages should be passed.
			return new SecureClientChannelSink(nextSink, _algorithm, _oaep, _maxAttempts);
		}

		/// <summary>Gets or sets the next sink provider in the channel sink provider chain.</summary>
		public System.Runtime.Remoting.Channels.IClientChannelSinkProvider Next
		{
			get { return _next; }
			set { _next = value; }
		}
		#endregion
	}
}
