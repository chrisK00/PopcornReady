using System.Threading.Tasks;
using PopcornReady.Razor.Entities;

namespace PopcornReady.Razor.ApiServices
{
    public interface ITvShowsApiService
    {
        Task<TvShow> GetTvSeriesAsync(string name);
        // TODO: get image for tv show
    }
}
