using System;

namespace racewrangler
{
    class Program
    {
        static void Main(string[] args)
        {
            Race r = new Race();

            r.AddRaceClass(new RaceClass() {
                Name = "A Street",
                Handicap = 1.0
            });

            r.AddRaceClass(new RaceClass() {
                Name = "B Street",
                Handicap = 0.90
            });
            r.AddRaceClass(new RaceClass() {
                Name = "C Street",
                Handicap = 0.80
            });
            r.AddRaceClass(new RaceClass() {
                Name = "D Street",
                Handicap = 0.70
            });
            r.AddRaceClass(new RaceClass() {
                Name = "E Street",
                Handicap = 0.60
            });

            foreach (RaceClass rc in r.RaceClasses)
            {
                for (int i = 1; i <= 20; ++i)
                {
                    r.AddEntrant(new Entrant() {
                        RaceClass = rc,
                        Name = $"Entry {i}",
                        Number = i
                    });
                }
            }

            r.DumpData();
        }
    }
}
