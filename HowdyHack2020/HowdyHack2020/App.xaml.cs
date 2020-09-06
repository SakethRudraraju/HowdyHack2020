using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HowdyHack2020.Services;
using HowdyHack2020.Views;

namespace HowdyHack2020
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			DependencyService.Register<MockDataStore>();
			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
