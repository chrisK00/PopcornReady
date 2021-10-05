using System.Text.Json.Serialization;

namespace PopcornReady.Core.ApiModels.Episodate
{
    public class EpisodateTvshow
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public object EndDate { get; set; }

        public string Status { get; set; }

        [JsonPropertyName("image_thumbnail_path")]
        public string ImagePath { get; set; }

        [JsonPropertyName("countdown")]
        public EpisodateEpisode NextEpisode { get; set; }

        public EpisodateEpisode[] Episodes { get; set; }
        public string Url { get; set; }
    }
}