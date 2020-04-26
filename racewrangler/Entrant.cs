using System;
using System.Collections.Generic;

namespace racewrangler
{
    public partial class Entrant
    {
        public Guid Id { get; set; }
        public Guid CompId { get; set; }
        public Guid ClassId { get; set; }
        public Guid CarId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double? SeasonPoints { get; set; }

        public virtual Competition Comp { get; set; }
    }
}
