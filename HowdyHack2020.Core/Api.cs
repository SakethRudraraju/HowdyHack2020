﻿using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowdyHack2020.Core
{
	public static class Api
	{
		public const string HOST = "http://45.63.0.107:3000/";

		public static async Task<System.Net.HttpStatusCode> CreateUser(string deviceId)
		{
			try
			{
				var response = await HOST
					.AppendPathSegment("newuser")
					.PostJsonAsync(new
					{
						deviceID = deviceId,
						username = "Dummy2"
					});
				return response.StatusCode;
			}
			catch (FlurlHttpException ex)
			{
				return ex.Call.HttpStatus.GetValueOrDefault();
			}
		}

		public static async Task<List<Place>> GetPlaces()
		{
			return await HOST
				.AppendPathSegment("places")
				.GetJsonAsync<List<Place>>();
		}

		/// <summary>
		/// Returns the distance in miles to the nearest place,
		/// or the location discovered, or null if not nearby any place
		/// </summary>
		public static async Task<Status> CheckNearby(double lat, double lon, string deviceId)
		{
			try
			{
				var response = await HOST
					.AppendPathSegments("checkNearby")
					.PostJsonAsync(new
					{
						coordinates = new double[] { lat, lon },
						deviceID = deviceId
					});
				return JsonConvert.DeserializeObject<Status>(await response.Content.ReadAsStringAsync());
			}
			catch (FlurlHttpException ex)
			{
				switch (ex.Call.HttpStatus)
				{
					default:
					case System.Net.HttpStatusCode.NotFound:
						return null;
				}
			}
		}

		/// <summary>
		/// Gets a list of the user's visited locations
		/// </summary>
		public static async Task<List<int>> GetVisitedPlaces(string deviceId)
		{
			return await HOST
				.AppendPathSegment("visited")
				.SetQueryParam("deviceID", deviceId)
				.GetJsonAsync<List<int>>();
		}
	}
}
