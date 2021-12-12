using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PopcornReady.Core.ApiServices;
using PopcornReadyV2.Business.Data;
using PopcornReadyV2.Business.Data.Entities;
using PopcornReadyV2.Shared.Params;
using PopcornReadyV2.Shared.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopcornReady.Core.Services
{
    public class TvShowsService : ITvShowsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TvShowsService> _logger;
        private readonly IMapper _mapper;
        private readonly ITvShowsApiService _tvShowsApiService;
        public TvShowsService(AppDbContext context, ITvShowsApiService tvShowsApiService, ILogger<TvShowsService> logger, IMapper mapper)
        {
            _context = context;
            _tvShowsApiService = tvShowsApiService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(TvShowResponse TvShow, string Error)> AddAsync(string title, string userId)
        {
            // TODO: should be an index if we want to use it for searching and adding a normalized field is a godo idea
            var tvShow = await _context.TvShows.FirstOrDefaultAsync(x => x.Name == title);
            UserTvShow userTvShow = null;

            if (tvShow == null)
            {
                _logger.LogInformation($"Adding a new Tv Show named: {title} to the database");
                tvShow = await _tvShowsApiService.GetTvShowAsync(title);
                await _context.AddAsync(tvShow);
                await _context.SaveChangesAsync();
                userTvShow = new UserTvShow { UserId = userId, TvShowId = tvShow.Id };
            }
            else
            {
                if (await _context.UserTvShows.AnyAsync(x => x.TvShowId == tvShow.Id && x.UserId == userId))
                {
                    _logger.LogInformation($"A user tried to track an already tracked Tv Show called: {title}");
                    return (null, "you are already tracking this show");
                }

                userTvShow = new UserTvShow { UserId = userId, TvShowId = tvShow.Id };
            }

            await _context.AddAsync(userTvShow);
            await _context.SaveChangesAsync();

            return (_mapper.Map<TvShowResponse>(tvShow), null);
        }

        public async Task<TvShowResponse> FindAsync(string name)
        {
            var tvShow = await _context.TvShows.AsNoTracking()
                .Include(x => x.NextEpisode)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

            if (tvShow == null)
            {
                _logger.LogInformation($"The Tv Show: {name} does not exist in the Db, sending a request to the API");
                tvShow = await _tvShowsApiService.GetTvShowAsync(name);
            }

            return _mapper.Map<TvShowResponse>(tvShow);
        }

        public async Task<IEnumerable<TvShowResponse>> GetAllAsync(TvShowParams param)
        {
            var query = _context.TvShows.AsNoTracking().Include(x => x.NextEpisode).AsQueryable();
            query = ApplyFiltering(query, param);

            // TODO: implement real pagination
            return await query.Take(3).ProjectTo<TvShowResponse>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<TvShowResponse>> GetAllForUserAsync(TvShowParams param, string userId)
        {
            var userTvShows = _context.UserTvShows.AsNoTracking().Where(x => x.UserId == userId);
            var query = _context.TvShows.AsNoTracking().Where(x => userTvShows.Any(y => y.TvShowId == x.Id));
            ApplyFiltering(query, param);

            return await query.ProjectTo<TvShowResponse>(_mapper.ConfigurationProvider).ToListAsync();
        }

        private IQueryable<TvShow> ApplyFiltering(IQueryable<TvShow> query, TvShowParams param)
        {
            if (!string.IsNullOrWhiteSpace(param.Title))
            {
                // TODO: Just have a normalized prop instead
                query = query.Where(x => x.Name.ToLower().Contains(param.Title.ToLower()));
            }

            if (param.HasNextEpisode)
            {
                query = query.Where(x => x.NextEpisode != null);
            }

            if (param.OrderBy == "trending")
            {
                query = query.OrderByDescending(x => _context.UserTvShows.Where(y => y.TvShowId == x.Id).Count());
            }

            return query;
        }

        public async Task RemoveAsync(int tvShowId, string userId)
        {
            var userTvShow = await _context.UserTvShows.FirstOrDefaultAsync(x => x.TvShowId == tvShowId && x.UserId == userId);

            _ = userTvShow ?? throw new KeyNotFoundException($"Tv Show with the id {tvShowId} was not found");

            _context.UserTvShows.Remove(userTvShow);
            await _context.SaveChangesAsync();
        }
    }
}
