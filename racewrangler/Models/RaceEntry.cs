using System.Collections.Generic;

namespace racewrangler.Models
{
    public class RaceEntry
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string Sponsor { get; set; }
        public Driver Driver { get; set; }
        public Car Car { get; set; }
        public Classification Class { get; set; }
        public Competition Competition { get; set; }
        public ICollection<Run> Runs { get; set; }
    }
}