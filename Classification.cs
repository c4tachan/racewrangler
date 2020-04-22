using System;
using System.Collections.Generic;

namespace racewrangler
{
    public partial class Classification
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abreviation { get; set; }
        public double? Handicap { get; set; }
    }
}
