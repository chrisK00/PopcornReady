﻿using PopcornReady.Core.ApiModels.Episodate;
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

        public async Task<TvShow> GetTvSeriesAsync(string name)
        {
            name = name.Replace(' ', '-');
            var client = _clientFactory.CreateClient(ApiOptions.EpisodateClientName);
            var request = await client.GetAsync($"{_showDetailsUri}?q={name}");
            if (request.Content.Headers.ContentLength < 14)
            {
                return null;
            }

            var episodedateTvShow = (await request.Content.ReadFromJsonAsync<EpisodateRootobject>()).TvShow;

            var tvShow = new TvShow
            {
                Name = episodedateTvShow.Name,
                ApiId = episodedateTvShow.Id,
                Status = episodedateTvShow.Status,
                Url = episodedateTvShow.Url,
                ImageUrl = episodedateTvShow.ImagePath,
                Description = $"{episodedateTvShow.Description[..200]}...",
                DescriptionUrl = episodedateTvShow.DescriptionUrl
            };

            if (episodedateTvShow.NextEpisode == null)
            {
                return tvShow;
            }

            tvShow.NextEpisode = new Episode
            {
                AirDate = DateTime.Parse(episodedateTvShow.NextEpisode.AirDate),
                Name = episodedateTvShow.NextEpisode.Name,
                Season = episodedateTvShow.NextEpisode.Season,
                Number = episodedateTvShow.NextEpisode.Episode
            };

            return tvShow;
        }
    }
}