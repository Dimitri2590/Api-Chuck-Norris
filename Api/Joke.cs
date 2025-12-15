using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Api
{
    public class Joke
    {
        // Json Tags
        [JsonPropertyName("id")]

        public string Id { get; set; }

        [JsonPropertyName("value")]

        public string Description { get; set; }

    }
}
