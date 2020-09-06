using Newtonsoft.Json;

namespace HowdyHack2020.Core
{
	public class Device
	{
		public Device() { }

		public Device(string id)
		{
			DeviceId = id;
		}

		[JsonProperty("deviceID")]
		public string DeviceId { get; set; }
	}
}
