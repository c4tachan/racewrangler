using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using racewrangler;
using System;
using System.Linq;

namespace testRaceWrangler
{
    [TestClass]
    public class TestDbReadWrite
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var context = new autocrossContext())
            {
                InitLocation(context);

                InitClassifications(context);

                InitCars(context);

                foreach(var loc in context.Location.ToList())
                {
                    InitCompetition(context, loc);
                }

                context.SaveChanges();
            }
        }


        private void InitLocation(autocrossContext context)
        {
            context.Location.Add(new Location()
            {
                Id = Guid.NewGuid(),
                Name = "ZMaXX Dragway",
                City = "Concord",
                State = "NC",
                Zip = "28027"
            });
        }

        private void InitClassifications(autocrossContext context)
        {
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
        }

        private void InitCars(autocrossContext context)
        {
            context.Car.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Chevy",
                Model = "Corvette",
                Year = "2020",
                Description = "Casino's Car",
                Number = "101"
            });

            context.Car.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Mazda",
                Model = "MX-5 Miata",
                Year = "2013",
                Description = "Chrystal White Mica",
                Number = "24"
            });

            context.Car.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Mazda",
                Model = "MX-5 Miata",
                Year = "2019",
                Description = "30AE",
                Number = "42"
            });
        }

        private void InitCompetition(autocrossContext context, Location loc)
        {
            Competition c = new Competition()
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(2020, 2, 2),
                Name = "Michael's Birthday Bash",
                PenaltyCost = new TimeSpan(0,0,2)
            };


            foreach (var cls in context.Classification.ToList())
            {
                InitEntries(c, cls, context.Car);
            }

            context.Entrant.AddRange(c.Entrant);

            InitRuns(context);
            
            context.Competition.Add(c);
            loc.Competition.Add(c);

        }
                
        private void InitEntries(Competition comp, Classification cls, DbSet<Car> car)
        {
            foreach (var cr in car.ToList())
            {
                comp.Entrant.Add(new Entrant()
                {
                    Id = Guid.NewGuid(),
                    Comp = comp,
                    ClassId = cls.Id,
                    CarId = cr.Id,
                    FirstName = cls.Name + cr.Number,
                    LastName = cls.Abreviation + cr.Description,
                    SeasonPoints = 0.0
                });
            }
        }


        private void InitRuns(autocrossContext context)
        {
            foreach (var comp in context.Competition.ToList())
            {
                var entrants = (from e in context.Entrant
                               where e.Comp == comp
                               select e).ToList();

                foreach (var ent in entrants)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        context.Run.Add(new Run()
                        {
                            Id = Guid.NewGuid(),
                            CompId = comp.Id,
                            EntryId = ent.Id,
                            TimeStart = DateTime.Now,
                            RunTime = new TimeSpan(0,0,i*10),
                            PenaltyCount = i,
                            Disqualified = false
                        });
                    }
                }
            }
        }


        [TestCleanup]
        public void Cleanup()
        {
            using (var context = new autocrossContext())
            {
                context.RemoveRange(context.Run);
                context.RemoveRange(context.Entrant);
                context.RemoveRange(context.Competition);
                context.RemoveRange(context.Location);
                context.RemoveRange(context.Classification);
                context.RemoveRange(context.Car);

                context.SaveChanges();
            }
        }


        [TestMethod]
        public void TestReadLocation()
        {
            using (var context = new autocrossContext())
            {
                var loc = from l in context.Location
                          select l;

                Assert.AreEqual(loc.First().Name, "ZMaXX Dragway");
                Assert.AreEqual(loc.First().City, "Concord");
                Assert.AreEqual(loc.First().State, "NC");
                Assert.AreEqual(loc.First().Zip, "28027");
            }
        }

        [TestMethod]
        public void TestReadClassification()
        {
            using (var context = new autocrossContext())
            {
                Classification cls = (from c in context.Classification
                                     where c.Abreviation == "AS"
                                     select c).First();

                Assert.AreEqual(cls.Name, "A Street");
                Assert.AreEqual(cls.Handicap, 1.0);


                cls = (from c in context.Classification
                       where c.Abreviation == "BS"
                       select c).First();

                Assert.AreEqual(cls.Name, "B Street");
                Assert.AreEqual(cls.Handicap, 0.9);

                cls = (from c in context.Classification
                       where c.Abreviation == "CS"
                       select c).First();

                Assert.AreEqual(cls.Name, "C Street");
                Assert.AreEqual(cls.Handicap, 0.8);

                cls = (from c in context.Classification
                       where c.Abreviation == "DS"
                       select c).First();

                Assert.AreEqual(cls.Name, "D Street");
                Assert.AreEqual(cls.Handicap, 0.7);

                cls = (from c in context.Classification
                       where c.Abreviation == "ES"
                       select c).First();

                Assert.AreEqual(cls.Name, "E Street");
                Assert.AreEqual(cls.Handicap, 0.6);

            }
        }

        [TestMethod]
        public void TestReadCars()
        {
            using (var context = new autocrossContext())
            {
                Car car = (from c in context.Car
                          where c.Number == "101"
                          select c).First();

                Assert.AreEqual(car.Make, "Chevy");
                Assert.AreEqual(car.Model, "Corvette");
                Assert.AreEqual(car.Year, "2020");
                Assert.AreEqual(car.Description, "Casino's Car");

                car = (from c in context.Car
                       where c.Number == "24"
                       select c).First();

                Assert.AreEqual(car.Make, "Mazda");
                Assert.AreEqual(car.Model, "MX-5 Miata");
                Assert.AreEqual(car.Year, "2013");
                Assert.AreEqual(car.Description, "Chrystal White Mica");

                car = (from c in context.Car
                       where c.Number == "42"
                       select c).First();

                Assert.AreEqual(car.Make, "Mazda");
                Assert.AreEqual(car.Model, "MX-5 Miata");
                Assert.AreEqual(car.Year, "2019");
                Assert.AreEqual(car.Description, "30AE");
            }
        }
    }
}
