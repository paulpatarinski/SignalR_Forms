using System;
using Microsoft.AspNet.SignalR.Client;
using Core.ViewModels;
using System.Threading.Tasks;


namespace Core
{
	public class MainViewModel : BaseViewModel
	{
		public MainViewModel (SignalRService signalRService)
		{
			_signalRService = signalRService;

		}

		readonly SignalRService _signalRService;

		private string _message;

		public string Message {
			get {
				return _message;
			}
			set {
				ChangeAndNotify (ref _message, value); 
			}
		}

		public async Task  ConnectAsync ()
		{
			await _signalRService.Connect ();
			_signalRService._chatHubProxy.On<string,string> ("broadcastMessage", (name, message) => {
				Message = string.Format ("Received Msg: {0}\r\n", message);
			}
			);
		}

	}
}

