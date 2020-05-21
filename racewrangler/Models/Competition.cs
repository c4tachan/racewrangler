using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace racewrangler.Models
{
    public class Competition
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public Season Season { get; set; }
        public int EventNum { get; set; }
        public Site EventSite { get; set; }
        public ICollection<RaceEntry> Entrants { get; set; }
        public Organizer Org { get; set; }
    }
}
