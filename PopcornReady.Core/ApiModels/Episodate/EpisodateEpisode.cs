using System.Text.Json.Serialization;

namespace PopcornReady.Core.ApiModels.Episodate
{
    public class EpisodateEpisode
    {
        public int Season { get; set; }
        public int Episode { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("air_date")]
        public string AirDate { get; set; }
    }
}