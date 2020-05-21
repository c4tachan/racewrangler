namespace racewrangler.Models
{
    public class Run
    {
        public int ID { get; set; }
        public Competition Comp { get; set; }
        public RaceEntry RaceEntry { get; set; }
        public int RunNumber { get; set; }
        public float LapTime { get; set; }
    }
}