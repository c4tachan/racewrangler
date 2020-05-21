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
    public class IndexModel : PageModel
    {
        private readonly racewrangler.Data.racewranglerContext _context;

        public IndexModel(racewrangler.Data.racewranglerContext context)
        {
            _context = context;
        }

        public IList<Driver> Driver { get;set; }

        public async Task OnGetAsync()
        {
            Driver = await _context.Drivers.ToListAsync();
        }
    }
}
