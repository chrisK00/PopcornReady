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
        private readonly INotyfService _notyf;
        private readonly ITvShowsService _tvShowsService;
        public IndexModel(ITvShowsService tvShowsService, INotyfService notyf)
        {
            _tvShowsService = tvShowsService;
            _notyf = notyf;
        }

        [MinLength(2, ErrorMessage = "Search for a name with more than 2 characters")]
        [BindProperty]
        public string Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public TvShowParams TvShowParams { get; set; }
        public IEnumerable<TvShow> TvShows { get; set; }

        public async Task OnGetAsync()
        {
            TvShows = await _tvShowsService.GetAllAsync(TvShowParams);
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