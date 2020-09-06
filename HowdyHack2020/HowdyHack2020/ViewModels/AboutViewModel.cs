using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HowdyHack2020.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public AboutViewModel()
		{
			Name = "About";
			OpenWebCommand = new Command(async () => await Browser.OpenAsync("http://45.63.0.107:4000/"));
		}

		public ICommand OpenWebCommand { get; }
	}
}