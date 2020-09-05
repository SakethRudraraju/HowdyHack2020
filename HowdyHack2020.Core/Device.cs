using Newtonsoft.Json;

namespace HowdyHack2020.Core
{
	public class Device
	{
		[JsonProperty("deviceID")]
		public string DeviceId { get; set; }
	}
}
