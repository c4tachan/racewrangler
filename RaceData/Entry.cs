namespace racewrangler
{
    class Entrant
    {
        public RaceClass RaceClass { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public bool Paid { get; set; }
        public bool Present { get; set; }

        public Entrant()
        {
            RaceClass = new RaceClass();
            Name = "Racer X";
            Number = -1;
        }


    }
}