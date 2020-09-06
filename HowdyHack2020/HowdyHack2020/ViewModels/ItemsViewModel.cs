using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using HowdyHack2020.Views;
using HowdyHack2020.Core;

namespace HowdyHack2020.ViewModels
{
	public class ItemsViewModel : BaseViewModel
	{
		public ObservableCollection<Place> Visited { get; set; }
		public Command LoadItemsCommand { get; set; }

		public ItemsViewModel()
		{
			Name = "Visited";
			Visited = new ObservableCollection<Place>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			//MessagingCenter.Subscribe<NewItemPage, Place>(this, "AddItem", async (obj, item) =>
			//{
			//	var newItem = item as Place;
			//	Items.Add(newItem);
			//	await DataStore.AddItemAsync(newItem);
			//});
		}

		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;

			try
			{
				Visited.Clear();

				string deviceId = await SettingsManager.EnsureAndGetDeviceId();

				var places = await Api.GetPlaces();
				var visited = await Api.GetVisitedPlaces(deviceId);
				foreach (int i in visited)
				{
					Visited.Add(places[i]);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}