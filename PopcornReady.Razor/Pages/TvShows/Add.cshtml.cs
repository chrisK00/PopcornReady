using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PopcornReady.Razor.Entities;
using PopcornReady.Razor.Services;

namespace PopcornReady.Razor.Pages.TvShows
{
    public class AddModel : PageModel
    {
        private readonly ITvShowsService _tvShowsService;

        [BindProperty(SupportsGet = true)]
        public TvShow TvShow { get; set; }

        public AddModel(ITvShowsService tvShowsService)
        {
            _tvShowsService = tvShowsService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _tvShowsService.AddAsync(TvShow);
            return RedirectToPage("./Index");
        }
    }
}
