using System.ComponentModel;

namespace PopcornReady.Core.Params
{
    public class TvShowParams
    {
        // TOOD: remove hardcoded 1
        public int? UserId { get; set; } = 1;
        public string Title { get; set; }

        [DisplayName("Next episode?")]
        public bool HasNextEpisode { get; set; }
    }
}
