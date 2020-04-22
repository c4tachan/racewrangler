using System;
using System.Collections.Generic;

namespace racewrangler
{
    class Program
    {
        static void Main(/*string[] args*/)
        {
            using (var context = new autocrossContext())
            {
                Competition c = new Competition();
                List<Classification> classes = new List<Classification>();

                context.Classification.Add(new Classification()
                {
                    Id = Guid.NewGuid(),
                    Name = "A Street",
                    Abreviation = "AS",
                    Handicap = 1.0
                });

                context.Classification.Add(new Classification()
                {
                    Id = Guid.NewGuid(),
                    Name = "B Street",
                    Abreviation = "BS",
                    Handicap = 0.90
                });
                context.Classification.Add(new Classification()
                {
                    Id = Guid.NewGuid(),
                    Name = "C Street",
                    Abreviation = "CS",
                    Handicap = 0.80
                });
                context.Classification.Add(new Classification()
                {
                    Id = Guid.NewGuid(),
                    Name = "D Street",
                    Abreviation = "DS",
                    Handicap = 0.70
                });
                context.Classification.Add(new Classification()
                {
                    Id = Guid.NewGuid(),
                    Name = "E Street",
                    Abreviation = "ES",
                    Handicap = 0.60
                });

                context.SaveChanges();

                //foreach (RaceClass rc in e.RaceClasses)
                //{
                //    for (int i = 1; i <= 20; ++i)
                //    {
                //        e.AddEntrant(new Entrant() {
                //            RaceClass = rc,
                //            Name = $"Entry {i}",
                //            Number = i
                //        });
                //    }
                //}

                //e.DumpData();
            }
        }
    }
}
