using Newtonsoft.Json;

namespace HowdyHack2020.Core
{
	public class Checkpoint
	{
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("coordinates")]
		public int[] Coordinates { get; set; }

		[JsonProperty("clue")]
		public string Clue { get; set; }

		[JsonProperty("qrid")]
		public string Qrid { get; set; }
	}
}
