using System;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;

namespace Core
{
	public class SignalRService
	{
		public SignalRService ()
		{
		}

		HubConnection _hubConnection {
			get;
			set;
		}

		public IHubProxy _chatHubProxy {
			get;
			set;
		}

		public async Task Connect ()
		{
			// Connect to the server
			_hubConnection = new HubConnection ("http://xamarinsignalr.azurewebsites.net/");

			// Create a proxy to the 'ChatHub' SignalR Hub
			_chatHubProxy = _hubConnection.CreateHubProxy ("ChatHub");

			// Wire up a handler for the 'UpdateChatMessage' for the server
			// to be called on our client
//			_chatHubProxy.On<string,string> ("broadcastMessage", (name, message) => {
//				textView.Text = string.Format ("Received Msg: {0}\r\n", message);
//			}
//			);

			try {
				await _hubConnection.Start ();
			} catch (Exception e) {
				throw;
			}
			// Start the connection

		
		}

		public async Task SendMessageAsync (string message)
		{
			var messageObj = new SignalRMessage{ Name = "Android", Message = message };
			// Invoke the 'UpdateNick' method on the server
			await _chatHubProxy.Invoke ("Send", messageObj);
		}




	}
}

