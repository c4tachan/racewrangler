using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using racewrangler.Data;
using racewrangler.Models;

namespace racewrangler.Pages.Competitions
{
    public class CreateModel : PageModel
    {
        private readonly racewrangler.Data.racewranglerContext _context;

        public CreateModel(racewrangler.Data.racewranglerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? season)
        {
            
            return Page();
        }

        [BindProperty]
        public Competition Competition { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Competition.Season = (from s in _context.Seasons
                                  where s.Year == Competition.Date.Year
                                  select s).Single();


            _context.Competitions.Add(Competition);
            await _context.SaveChangesAsync();


            return RedirectToPage("../Seasons/Details/", new {id = Competition.Date.Year});
        }
    }
}
