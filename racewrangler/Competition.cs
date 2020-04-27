using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual Location Location { get; set; }

        [JsonIgnore]
        public virtual ICollection<Entrant> Entrant { get; set; }
    }
}
