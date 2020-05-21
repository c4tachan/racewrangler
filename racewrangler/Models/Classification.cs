using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace racewrangler.Models
{
    public class Classification
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public float? Handicap { get; set; }
    }
}
