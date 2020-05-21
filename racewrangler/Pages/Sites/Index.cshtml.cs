using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using racewrangler.Data;
using racewrangler.Models;

namespace racewrangler.Pages.Sites
{
    public class IndexModel : PageModel
    {
        private readonly racewrangler.Data.racewranglerContext _context;

        public IndexModel(racewrangler.Data.racewranglerContext context)
        {
            _context = context;
        }

        public IList<Site> Site { get;set; }

        public async Task OnGetAsync()
        {
            Site = await _context.Sites.ToListAsync();
        }
    }
}
