namespace PopcornReadyV2.Shared.Responses
{
    public class TvShowResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public EpisodeResponse NextEpisode { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string DescriptionUrl { get; set; }
        public string EpisodateUrl { get; set; }
    }
}
