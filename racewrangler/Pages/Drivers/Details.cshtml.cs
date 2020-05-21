using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using racewrangler.Data;
using racewrangler.Models;

namespace racewrangler.Pages.Drivers
{
    public class DetailsModel : PageModel
    {
        private readonly racewrangler.Data.racewranglerContext _context;

        public DetailsModel(racewrangler.Data.racewranglerContext context)
        {
            _context = context;
        }

        public Driver Driver { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Driver = await _context.Drivers.FirstOrDefaultAsync(m => m.ID == id);

            if (Driver == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
