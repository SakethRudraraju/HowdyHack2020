using HowdyHack2020.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HowdyHack2020.App
{
	public static class AppDataManager
	{
		const string DEVICEID = "deviceId.txt";
		public static string GetDeviceId()
		{
			var backingFile = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, DEVICEID);
			if (File.Exists(backingFile))
			{
				return File.ReadAllText(backingFile);
			}
			return null;
		}
		public static void SetDeviceId(string deviceId)
		{
			var backingFile = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, DEVICEID);
			File.WriteAllText(backingFile, deviceId);
		}
		public static async Task<string> EnsureAndGetDeviceId()
		{
			string deviceId = GetDeviceId();
			if (deviceId == null)
			{
				deviceId = Guid.NewGuid().ToString();
				switch (await Api.CreateUser(deviceId))
				{
					case System.Net.HttpStatusCode.Conflict:
						return await EnsureAndGetDeviceId();
				}
				SetDeviceId(deviceId);
			}
			return deviceId;
		}
		public static void ResetDeviceId()
		{
			if (File.Exists(DEVICEID))
				File.Delete(DEVICEID);
		}
	}
}