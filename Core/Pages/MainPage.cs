using System;
using Xamarin.Forms;

namespace Core
{
	public class MainPage : ContentPage
	{
		MainViewModel _viewModel;

		public MainPage ()
		{
			_viewModel = new MainViewModel (new SignalRService ());

			BindingContext = _viewModel;

			var label = new Label {
				Text = "Change this page.",
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			label.SetBinding<MainViewModel> (Label.TextProperty, x => x.Message);

			Content = label;
		}

		protected override void OnAppearing ()
		{
			_viewModel.ConnectAsync ();
		}
	}
}

