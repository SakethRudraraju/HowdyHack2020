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
		public ObservableCollection<Place> Items { get; set; }
		public Command LoadItemsCommand { get; set; }

		public ItemsViewModel()
		{
			Name = "Browse";
			Items = new ObservableCollection<Place>();
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
				Items.Clear();
				var items = await Api.GetPlaces();
				foreach (var item in items)
				{
					Items.Add(item);
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