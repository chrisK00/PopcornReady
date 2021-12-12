using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopcornReady.Core.Services;
using PopcornReadyV2.Server.Extensions;
using PopcornReadyV2.Shared.Params;
using PopcornReadyV2.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopcornReadyV2.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TvShowsController : ControllerBase
    {
        private readonly ITvShowsService _tvShowsService;

        public TvShowsController(ITvShowsService tvShowsService)
        {
            _tvShowsService = tvShowsService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TvShowResponse>>> GetAll([FromQuery] TvShowParams parameters)
        {
            var tvShows = await _tvShowsService.GetAllAsync(parameters);
            return Ok(tvShows);
        }

        [HttpGet("my-shows")]
        public async Task<ActionResult<IEnumerable<TvShowResponse>>> GetMyShows([FromQuery] TvShowParams parameters)
        {
            var tvShows = await _tvShowsService.GetAllForUserAsync(parameters, User.GetId());
            return Ok(tvShows);
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<TvShowResponse>> GetAsync(string title)
        {
            var tvShow = await _tvShowsService.FindAsync(title);
            return tvShow;
        }

        [HttpPost("{title}")]
        public async Task<ActionResult> AddAsync(string title)
        {
            var (TvShow, Error) = await _tvShowsService.AddAsync(title, User.GetId());
            if (!string.IsNullOrWhiteSpace(Error)) return BadRequest(Error);

            // TODO: use created at action, need to add Name = nameof() on the other method, also should make use of the id instead of name unless make it an index
            // async suffix issue https://stackoverflow.com/questions/39459348/asp-net-core-web-api-no-route-matches-the-supplied-values
            return Created(nameof(GetAsync), new { title = TvShow.Name });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTvShowAsync(int id)
        {
            await _tvShowsService.RemoveAsync(id, User.GetId());
            return NoContent();
        }
    }
}
