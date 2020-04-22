using System;
using System.Collections.Generic;

namespace racewrangler
{
    public partial class Location
    {
        public Location()
        {
            Competition = new HashSet<Competition>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual ICollection<Competition> Competition { get; set; }
    }
}
