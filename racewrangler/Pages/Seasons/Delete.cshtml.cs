using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using racewrangler.Data;
using racewrangler.Models;

namespace racewrangler.Pages.Seasons
{
    public class DeleteModel : PageModel
    {
        private readonly racewrangler.Data.racewranglerContext _context;

        public DeleteModel(racewrangler.Data.racewranglerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Season Season { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Season = await _context.Seasons.FirstOrDefaultAsync(m => m.Year == id);

            if (Season == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Season = await _context.Seasons.FindAsync(id);

            if (Season != null)
            {
                _context.Seasons.Remove(Season);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
