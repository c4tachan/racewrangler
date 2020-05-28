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
        public List<Season> Seasons { get; set; }
        public List<RaceEntry> RaceEntries { get; set; }
        public List<Competition> Competitions { get; set; }
        public List<Classification> Classes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Driver = await _context.Drivers
                .Include(d => d.RaceEntries)
                    .ThenInclude(r => r.Runs)
                .Include(d => d.RaceEntries)
                    .ThenInclude(r => r.Car)
                .Include(d => d.RaceEntries)
                    .ThenInclude(r => r.Class)
                .Include(d => d.RaceEntries)
                    .ThenInclude(r => r.Competition)
                .Include(d => d.RaceEntries)
                    .ThenInclude(r => r.Driver)
                .FirstOrDefaultAsync(m => m.ID == id);

            RaceEntries = Driver.RaceEntries.ToList();

            Classes = (from r in RaceEntries
                       select r.Class).ToList();

            Competitions = (from r in RaceEntries
                            select r.Competition).ToList();
            
            Seasons = (from c in Competitions
                       select c.Season).ToList();

            if (Driver == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
