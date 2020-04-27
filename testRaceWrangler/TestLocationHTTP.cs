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
    public class TestLocationHTTP
    {
        private Location zmax = new Location()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Name = "ZMaXX Dragway",
            City = "Concord",
            State = "NC",
            Zip = "28027"
        };

        private Location blackLake = new Location()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            Name = "Black Lake",
            City = "Laurens",
            State = "SC",
            Zip = "29370"
        };

        [TestInitialize]
        public void Initialize()
        {
            using (var context = new autocrossContext())
            {
                context.Location.Add(zmax);
                context.Location.Add(blackLake);

                context.SaveChanges();
            }
        }


        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new autocrossContext())
            {
                context.RemoveRange(context.Location);
                context.SaveChanges();
            }
        }


        [TestMethod]
        public void testGetLocation()
        {
            string serializedZmax = JsonSerializer.Serialize(zmax);
            string serializedBL = JsonSerializer.Serialize(blackLake);

            var LC = new LocationController();

            var results = LC.Get();

            Assert.IsTrue(results.Contains(serializedBL));
            Assert.IsTrue(results.Contains(serializedZmax));
        }

        [TestMethod]
        public void testGetSpecificLocation()
        {
            string serializedZmax = JsonSerializer.Serialize(zmax);
            string serializedBL = JsonSerializer.Serialize(blackLake);

            var LC = new LocationController();

            var zmaxxRslt = LC.Get("00000000-0000-0000-0000-000000000001");
            var blRslt = LC.Get("00000000-0000-0000-0000-000000000002");

            Assert.AreEqual(zmaxxRslt, serializedZmax);
            Assert.AreEqual(blRslt, serializedBL);

        }

        [TestMethod]
        public void testPostLocation()
        {
            var dreamSite = new Location()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Name = "Knights Stadium",
                City = "Fort Mill",
                State = "SC",
                Zip = "29708"
            };

            var serializedDreamSite = JsonSerializer.Serialize(dreamSite);

            var LC = new LocationController();

            LC.Post(serializedDreamSite);

            // Now that we've posted it, let's dig straight into the DB and see if it is there!
            using (var context = new autocrossContext())
            {
                var ks = (from l in context.Location
                          where l.Id == dreamSite.Id
                          select l).ToList().First();

                Assert.AreEqual(ks.Id, dreamSite.Id);
                Assert.AreEqual(ks.Name, dreamSite.Name);
                Assert.AreEqual(ks.City, dreamSite.City);
                Assert.AreEqual(ks.State, dreamSite.State);
                Assert.AreEqual(ks.Zip, dreamSite.Zip);
            }
        }

        [TestMethod]
        public void testPutLocation()
        {
            Location blackLakeUpdate = new Location()
            {
                Name = "Michelin Proving Grounds",
            };

            var LC = new LocationController();

            LC.Put(blackLake.Id.ToString(), JsonSerializer.Serialize(blackLakeUpdate));

            // Now check the database to see if we have changed only what we expected of the Black Lake entry
            using (var context = new autocrossContext())
            {
                var bl = (from l in context.Location
                          where l.Id == blackLake.Id
                          select l).ToList().First();

                Assert.AreEqual(bl.Id, blackLake.Id);
                Assert.AreEqual(bl.Name, "Michelin Proving Grounds");
                Assert.AreEqual(bl.City, blackLake.City);
                Assert.AreEqual(bl.State, blackLake.State);
                Assert.AreEqual(bl.Zip, blackLake.Zip);
            }
        }

        [TestMethod]
        public void testDeleteLocation()
        {
            var LC = new LocationController();

            LC.Delete(zmax.Id.ToString());

            // We deleted ZMaxx so check to see if it is gone from the Database!
            using (var context = new autocrossContext())
            {
                var zmx = (from l in context.Location
                           where l.Id == zmax.Id
                           select l).ToList().Count();

                Assert.IsTrue(zmx == 0);
            }
        }
    }
}
