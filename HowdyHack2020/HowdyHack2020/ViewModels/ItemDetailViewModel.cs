using System;

using HowdyHack2020.Core;

namespace HowdyHack2020.ViewModels
{
	public class ItemDetailViewModel : BaseViewModel
	{
		public Place Item { get; set; }
		public ItemDetailViewModel(Place item = null)
		{
			Name = item?.Name;
			Item = item;
		}
	}
}
