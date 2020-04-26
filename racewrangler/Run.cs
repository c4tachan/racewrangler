using System;
using System.Collections.Generic;

namespace racewrangler
{
    public partial class Run
    {
        public Guid Id { get; set; }
        public Guid CompId { get; set; }
        public Guid EntryId { get; set; }
        public DateTime TimeStart { get; set; }
        public TimeSpan RunTime { get; set; }
        public int? PenaltyCount { get; set; }
        public bool? Disqualified { get; set; }
    }
}
