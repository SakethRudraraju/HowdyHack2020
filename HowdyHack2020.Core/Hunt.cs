using System.Collections.Generic;
using Newtonsoft.Json;

namespace HowdyHack2020.Core
{
	public class Hunt
	{
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("checkpoints")]
		public List<Checkpoint> Checkpoints { get; set; }
	}
}
