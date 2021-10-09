using PopcornReady.Core.ApiModels.Episodate;
using PopcornReady.Core.Data.Entities;
using PopcornReady.Core.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PopcornReady.Core.ApiServices
{
    public class EpisodateApiService : ITvShowsApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _showDetailsUri = $"show-details";

        public EpisodateApiService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TvShow> GetTvShowAsync(string name)
        {
            name = name.Replace(' ', '-');
            var client = _clientFactory.CreateClient(ApiOptions.EpisodateClientName);
            var request = await client.GetAsync($"{_showDetailsUri}?q={name}");
            if (request.Content.Headers.ContentLength < 14)
            {
                return null;
            }

            var episodateTvShow = (await request.Content.ReadFromJsonAsync<EpisodateRootobject>()).TvShow;

            return CreateTvShow(episodateTvShow);
        }

        private TvShow CreateTvShow(EpisodateTvshow episodateTvShow)
        {
            var tvShow = new TvShow
            {
                Name = episodateTvShow.Name,
                ApiId = episodateTvShow.Id,
                Status = episodateTvShow.Status,
                Url = episodateTvShow.Url,
                ImageUrl = episodateTvShow.ImagePath,
                Description = $"{episodateTvShow.Description[..200]}...",
                DescriptionUrl = episodateTvShow.DescriptionUrl
            };

            if (episodateTvShow.NextEpisode == null)
            {
                return tvShow;
            }

            tvShow.NextEpisode = new Episode
            {
                AirDate = DateTime.Parse(episodateTvShow.NextEpisode.AirDate),
                Name = episodateTvShow.NextEpisode.Name,
                Season = episodateTvShow.NextEpisode.Season,
                Number = episodateTvShow.NextEpisode.Episode
            };

            return tvShow;
        }
    }
}