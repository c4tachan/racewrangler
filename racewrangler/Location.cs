using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ICollection<Competition> Competition { get; set; }
    }
}
