using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace racewrangler.Models
{
    public class Driver
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MemberNumber { get; set; }
        public ICollection<Points> SeasonPoints { get; set; }
        public ICollection<RaceEntry> RaceEntries { get; set; }
    }
}
