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

        public string NameSort { get; set; }
        public string MemberNumSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Driver> Drivers { get;set; }
        public int PageSize { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, int pageSize = 0)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            MemberNumSort = sortOrder == "MemberNum" ? "memNum_desc" : "MemberNum";
            PageSize = pageSize > 0 ? pageSize : 10;
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Driver> driversIQ = from d in _context.Drivers
                                         select d;

            if(!string.IsNullOrEmpty(searchString))
            {
                driversIQ = driversIQ.Where(d => d.LastName.Contains(searchString)
                                              || d.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    driversIQ = driversIQ.OrderByDescending(s => s.LastName);
                    break;
                case "MemberNum":
                    driversIQ = driversIQ.OrderBy(s => s.MemberNumber);
                    break;
                case "memNum_desc":
                    driversIQ = driversIQ.OrderByDescending(s => s.MemberNumber);
                    break;
                default:
                    driversIQ = driversIQ.OrderBy(s => s.LastName);
                    break;
            }

            Drivers = await PaginatedList<Driver>.CreateAsync(
                driversIQ.AsNoTracking(), pageIndex ?? 1, PageSize);
        }
    }
}
