using Newtonsoft.Json;

namespace HowdyHack2020.Core
{
	public class Status
	{
		[JsonProperty("distance")]
		public double? Distance { get; set; }

		[JsonProperty("place")]
		public Place Place { get; set; }
	}
}
