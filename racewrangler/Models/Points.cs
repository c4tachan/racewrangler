namespace racewrangler.Models
{
    public class Points
    {
        public int ID { get; set; }
        public Driver Driver { get; set; }
        public Competition Comp { get; set; }
        public float Score { get; set; }
    }
}