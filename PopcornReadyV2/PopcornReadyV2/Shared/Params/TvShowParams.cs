using System.ComponentModel;

namespace PopcornReadyV2.Shared.Params
{
    public class TvShowParams : PaginationParams
    {
        public string OrderBy { get; set; } = "trending";

        public string Title { get; set; }

        [DisplayName("Next episode?")]
        public bool HasNextEpisode { get; set; }
    }
}
