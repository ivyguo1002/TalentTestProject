using Newtonsoft.Json;

namespace Talent.Models
{
    public class Skill
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }
    }
}