using System;
using System.Collections.Generic;

namespace racewrangler
{
    public partial class Competition
    {
        public Competition()
        {
            Entrant = new HashSet<Entrant>();
        }

        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public TimeSpan? PenaltyCost { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Entrant> Entrant { get; set; }
    }
}
