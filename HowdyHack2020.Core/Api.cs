using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowdyHack2020.Core
{
	public static class Api
	{
		public const string HOST = "https://howdyhack-2020.herokuapp.com/";

		public static async Task<bool> CreateUser(string deviceId)
		{
			var response = await HOST
				.AppendPathSegment("newuser")
				.PostJsonAsync(new { deviceID = deviceId });
			return response.IsSuccessStatusCode;
		}

		public static async Task<List<Hunt>> GetHunts()
		{
			return await HOST
				.AppendPathSegment("hunts")
				.GetJsonAsync<List<Hunt>>();
		}

		/// <summary>
		/// Returns the distance in miles to the nearest monument
		/// </summary>
		public static async Task<double> CheckNearby(DeviceLocation location)
		{
			var response = await HOST
				.AppendPathSegment("checkNearby")
				.PostJsonAsync(location);
			return JsonConvert.DeserializeObject<double>(await response.Content.ReadAsStringAsync());
		}
	}
}
