using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PopcornReady.Core.Data.Entities;
using PopcornReady.Core.Services;

namespace PopcornReady.Razor.Pages.TvShows
{
    public class AddModel : PageModel
    {
        private readonly ITvShowsService _tvShowsService;
        private readonly INotyfService _notyf;

        public AddModel(ITvShowsService tvShowsService, INotyfService notyf)
        {
            _tvShowsService = tvShowsService;
            _notyf = notyf;
        }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty]
        public TvShow TvShow { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrWhiteSpace(Search)) return RedirectToPage("./Index");

            TvShow = await _tvShowsService.FindAsync(Search);

            if (TvShow == null)
            {
                _notyf.Error($"The Tv Show: {Search} was not found");
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // TODO: Change to take in user id from token
            await _tvShowsService.AddAsync(TvShow, 1);
            return RedirectToPage("./Index");
        }
    }
}