using System.Collections.Generic;

namespace racewrangler.Models
{
    public class RaceEntry
    {
        public int ID { get; set; }
        public Driver Driver { get; set; }
        public Competition Competition { get; set; }
        public ICollection<Run> Runs { get; set; }
    }
}