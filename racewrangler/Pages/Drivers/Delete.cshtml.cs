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
    public class DeleteModel : PageModel
    {
        private readonly racewrangler.Data.racewranglerContext _context;

        public DeleteModel(racewrangler.Data.racewranglerContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Driver = await _context.Drivers.FindAsync(id);

            if (Driver != null)
            {
                _context.Drivers.Remove(Driver);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
