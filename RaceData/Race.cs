using System;
using System.Collections.Generic;
using System.Linq;

namespace racewrangler
{
    class Race
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public List<RaceClass> RaceClasses { get; set; }
        public List<Entrant> Entrants { get; set; }

        public Race()
        {
            Number = -1;
            Name = "Unnamed Race";
            Date = DateTime.Now;
            RaceClasses = new List<RaceClass>();
            Entrants = new List<Entrant>();
        }

        public void AddRaceClass(RaceClass rc)
        {
            RaceClasses.Add(rc);
        }

        public void AddEntrant(Entrant e)
        {
            Entrants.Add(e);
        }

        public void DumpData()
        {
            foreach (var rc in RaceClasses)
            {
                Console.WriteLine($"Race Class: {rc.Name}");
                DumpEntrants(rc);
            }
        }

        void DumpEntrants(RaceClass raceClass)
        {
            var entrants = from ent in Entrants
                where ent.RaceClass == raceClass
                select ent;

            foreach (var ent in entrants)
            {
                Console.WriteLine($"\tEntrant: {ent.Name}\t{ent.Number}");
            }
        }
    }
}