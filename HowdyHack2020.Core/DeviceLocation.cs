using Newtonsoft.Json;

namespace HowdyHack2020.Core
{
	public class DeviceLocation : Device
	{
		[JsonProperty("coordinates")]
		public int[] Coordinates { get; set; }
	}
}
