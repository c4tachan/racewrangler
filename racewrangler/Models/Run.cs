namespace racewrangler.Models
{
    public class Run
    {
        public int ID { get; set; }
        public RaceEntry RaceEntry { get; set; }
        public int RunNumber { get; set; }
        public float LapTime { get; set; }
        public int Penalties { get; set; }
        public bool DNF { get; set; }
    }
}