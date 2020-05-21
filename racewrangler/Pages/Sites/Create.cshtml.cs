using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using racewrangler.Data;
using racewrangler.Models;

namespace racewrangler.Pages.Sites
{
    public class CreateModel : PageModel
    {
        private readonly racewrangler.Data.racewranglerContext _context;

        public CreateModel(racewrangler.Data.racewranglerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Site Site { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Sites.Add(Site);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
