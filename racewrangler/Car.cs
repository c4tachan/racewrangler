using System;
using System.Collections.Generic;

namespace racewrangler
{
    public partial class Car
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
    }
}
