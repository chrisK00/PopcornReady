using System.Text.Json.Serialization;

namespace PopcornReadyV2.Business.Data.ApiModels.Episodate
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