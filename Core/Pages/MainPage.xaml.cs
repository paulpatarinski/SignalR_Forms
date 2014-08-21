using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ViewModels;
using Xamarin.Forms;

namespace Core.Pages
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
    }

    public void ShowMessage(string message)
    {
      var viewModel = this.BindingContext as ChatViewModel;

      if (viewModel != null)
      {
        viewModel.SubmitMessage(message);
      }

    }
  }
}
