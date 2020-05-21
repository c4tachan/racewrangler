using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using racewrangler.Data;
using racewrangler.Models;

namespace racewrangler.Pages.Competitions
{
    public class DetailsModel : PageModel
    {
        private readonly racewrangler.Data.racewranglerContext _context;

        public DetailsModel(racewrangler.Data.racewranglerContext context)
        {
            _context = context;
        }

        public Competition Competition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Competition = await _context.Competitions.FirstOrDefaultAsync(m => m.ID == id);

            if (Competition == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
