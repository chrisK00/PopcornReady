using System.Threading.Tasks;
using PopcornReady.Core.Data.Entities;

namespace PopcornReady.Core.ApiServices
{
    public interface ITvShowsApiService
    {
        Task<TvShow> GetTvSeriesAsync(string name);
        // TODO: get image for tv show
    }
}
