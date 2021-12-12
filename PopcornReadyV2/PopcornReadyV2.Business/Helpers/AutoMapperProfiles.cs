using AutoMapper;
using PopcornReadyV2.Business.Data.Entities;
using PopcornReadyV2.Shared.Responses;

namespace PopcornReadyV2.Business.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TvShow, TvShowResponse>();

            // TODO: Should not map from resp to tvshow
            CreateMap<TvShowResponse, TvShow>();

            CreateMap<Episode, EpisodeResponse>();
        }
    }
}
