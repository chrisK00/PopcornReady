using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PopcornReady.Core.Data.Entities;
using PopcornReady.Core.Params;
using PopcornReady.Core.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PopcornReady.Razor.Pages.TvShows
{
    public class IndexModel : PageModel
    {
        private readonly ITvShowsService _tvShowsService;
        private readonly INotyfService _notyf;

        public IndexModel(ITvShowsService tvShowsService, INotyfService notyf)
        {
            _tvShowsService = tvShowsService;
            _notyf = notyf;
        }

        [MinLength(2, ErrorMessage = "Search for a name with more than 2 characters")]
        [BindProperty]
        public string Search { get; set; }

        public IEnumerable<TvShow> TvShows { get; set; }

        public async Task OnGetAsync()
        {
            // TOOD: remove hardcoded 1
            TvShows = await _tvShowsService.GetAllAsync(new TvShowParams { UserId = 1 });
        }

        public ActionResult OnPost()
        {
            return !ModelState.IsValid ? Page() : RedirectToPage("./Add", new { Search });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _tvShowsService.RemoveAsync(id, 1);
            _notyf.Warning("The Tv Show has been removed");
            return RedirectToPage();
        }
    }
}