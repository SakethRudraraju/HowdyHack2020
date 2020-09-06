using Newtonsoft.Json;

namespace HowdyHack2020.Core
{
	public class Status
	{
		[JsonProperty("distance")]
		public double? Distance { get; set; }

		[JsonProperty("place")]
		public int? Place { get; set; }
	}
}
