using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using racewrangler;
using racewranglerServer.Controllers;
using System;
using System.Linq;
using System.Text.Json;

namespace testRaceWrangler
{
    [TestClass]
    public class TestClassificationHTTP
    {
        private Classification aStreet = new Classification()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
            Name = "A Street",
            Abreviation = "AS",
            Handicap = 1.0
        };

        private Classification bStreet = new Classification()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
            Name = "B Street",
            Abreviation = "BS",
            Handicap = 0.90
        };

        private Classification cStreet = new Classification()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
            Name = "C Street",
            Abreviation = "CS",
            Handicap = 0.80
        };

        [TestInitialize]
        public void Initialize()
        {
            using (var context = new autocrossContext())
            {
                context.Classification.Add(aStreet);
                context.Classification.Add(bStreet);
                context.Classification.Add(cStreet);

                context.SaveChanges();
            }
        }


        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new autocrossContext())
            {
                context.RemoveRange(context.Classification);
                context.SaveChanges();
            }
        }


        [TestMethod]
        public void testGetClassifications()
        {
            string serializedAStreet = JsonSerializer.Serialize(aStreet);
            string serializedBStreet = JsonSerializer.Serialize(bStreet);
            string serializedCStreet = JsonSerializer.Serialize(cStreet);

            var CC = new ClassificationController();

            var results = CC.Get();

            Assert.IsTrue(results.Contains(serializedAStreet));
            Assert.IsTrue(results.Contains(serializedBStreet));
            Assert.IsTrue(results.Contains(serializedCStreet));

        }

        [TestMethod]
        public void testGetSpecificLocation()
        {
            string serializedAStreet = JsonSerializer.Serialize(aStreet);
            string serializedBStreet = JsonSerializer.Serialize(bStreet);
            string serializedCStreet = JsonSerializer.Serialize(cStreet);

            var CC = new ClassificationController();

            var aRslt = CC.Get(aStreet.Id.ToString());
            var bRslt = CC.Get(bStreet.Id.ToString());
            var cRslt = CC.Get(cStreet.Id.ToString());

            Assert.AreEqual(aRslt, serializedAStreet);
            Assert.AreEqual(bRslt, serializedBStreet);
            Assert.AreEqual(cRslt, serializedCStreet);

        }

        [TestMethod]
        public void testPostLocation()
        {
            var dStreet = new Classification()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                Name = "D Street",
                Abreviation = "DS",
                Handicap = 0.70
            };

            var serializedDS = JsonSerializer.Serialize(dStreet);

            var CC = new ClassificationController();

            CC.Post(serializedDS);

            // Now that we've posted it, let's dig straight into the DB and see if it is there!
            using (var context = new autocrossContext())
            {
                var ds = (from c in context.Classification
                          where c.Id == dStreet.Id
                          select c).ToList().First();

                Assert.AreEqual(ds.Id, dStreet.Id);
                Assert.AreEqual(ds.Name, dStreet.Name);
                Assert.AreEqual(ds.Abreviation, dStreet.Abreviation);
                Assert.AreEqual(ds.Handicap, dStreet.Handicap);

            }
        }

        [TestMethod]
        public void testPutLocation()
        {
            Location csUpdate = new Location()
            {
                Name = "The Best Class",
            };

            var CC = new ClassificationController();

            CC.Put(cStreet.Id.ToString(), JsonSerializer.Serialize(csUpdate));

            // Now check the database to see if we have changed only what we expected of the Black Lake entry
            using (var context = new autocrossContext())
            {
                var cs = (from c in context.Classification
                          where c.Id == cStreet.Id
                          select c).ToList().First();

                Assert.AreEqual(cs.Id, cStreet.Id);
                Assert.AreEqual(cs.Name, "The Best Class");
                Assert.AreEqual(cs.Abreviation, cStreet.Abreviation);
                Assert.AreEqual(cs.Handicap, cStreet.Handicap);

            }
        }

        [TestMethod]
        public void testDeleteLocation()
        {
            var CC = new ClassificationController();

            CC.Delete(aStreet.Id.ToString());

            // We deleted ZMaxx so check to see if it is gone from the Database!
            using (var context = new autocrossContext())
            {
                var zmx = (from l in context.Classification
                           where l.Id == aStreet.Id
                           select l).ToList().Count();

                Assert.IsTrue(zmx == 0);
            }
        }
    }
}
