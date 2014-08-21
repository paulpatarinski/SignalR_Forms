using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace Core.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		//TODO : Use IOC
		public MainViewModel () : this (new SignalRService ())
		{
		}

		public MainViewModel (SignalRService signalRService)
		{
			_signalRService = signalRService;
			ConnectAsync ();
			SubmitButtonCommand = new Command (() => {
				SubmitMessage ();
			});

		}

		private readonly SignalRService _signalRService;

		string _message;

		public string Message{ get { return _message; } set { ChangeAndNotify (ref _message, value); } }

		private ObservableCollection<SignalRMessage> _messages;

		public ObservableCollection<SignalRMessage> Messages {
			get {
				if (_messages == null)
					_messages = new ObservableCollection<SignalRMessage> ();
        
				return _messages;
			}
			set { ChangeAndNotify (ref _messages, value); }
		}

		public async Task ConnectAsync ()
		{
			await _signalRService.Connect ();

			_signalRService.OnMessageReceive ((name, message) => {
				var messageObj = new SignalRMessage { Name = name, Message = message };
				Messages.Add (messageObj);
			});
		}

		public ICommand SubmitButtonCommand { protected set; get; }

		public async Task SubmitMessage ()
		{
			_signalRService.SendMessageAsync (Message);

			Message = string.Empty;
		}
	}
}