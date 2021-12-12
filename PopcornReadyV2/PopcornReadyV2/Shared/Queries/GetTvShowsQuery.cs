using PopcornReadyV2.Shared.Params;

namespace PopcornReadyV2.Shared.Queries
{
    public class GetTvShowsQuery
    {
        public GetTvShowsQuery(TvShowParams parameters)
        {
            Params = parameters;
        }

        public TvShowParams Params { get; init; }
    }
}
