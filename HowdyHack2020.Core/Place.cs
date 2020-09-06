using Newtonsoft.Json;
using System.Collections.Generic;

namespace HowdyHack2020.Core
{
	public class Place
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("coordinates")]
		public double[] Coordinates { get; set; }

		[JsonProperty("clues")]
		public List<string> Clues { get; set; }

		[JsonProperty("index")]
		public int Index { get; set; }
	}
}
