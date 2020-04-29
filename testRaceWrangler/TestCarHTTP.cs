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
    public class TestCarHTTP
    {
        private Car midori = new Car()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000021"),
            Make = "Mazda",
            Model = "MX-5 Miata",
            Year = "1999",
            Description = "Brittish Racing Green",
            Number = "1337"
        };

        private Car shiro = new Car()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000022"),
            Make = "Mazda",
            Model = "MX-5 Miata",
            Year = "2013",
            Description = "Chrystal White Micah Club",
            Number = "24"
        };

        private Car kuro = new Car()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000023"),
            Make = "Mazda",
            Model = "MX-5 Miata",
            Year = "2017",
            Description = "BBS Brembo Package",
            Number = "124"
        };

        private Car tora = new Car()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000024"),
            Make = "Mazda",
            Model = "MX-5 Miata",
            Year = "2019",
            Description = "30AE",
            Number = "42"
        };

        [TestInitialize]
        public void Initialize()
        {
            using (var context = new autocrossContext())
            {
                context.Car.Add(midori);
                context.Car.Add(shiro);
                context.Car.Add(kuro);
                context.Car.Add(tora);

                context.SaveChanges();
            }
        }


        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new autocrossContext())
            {
                context.RemoveRange(context.Car);
                context.SaveChanges();
            }
        }


        [TestMethod]
        public void testGetCars()
        {
            string serializedMidori = JsonSerializer.Serialize(midori);
            string serializedShiro = JsonSerializer.Serialize(shiro);
            string serializedKuro = JsonSerializer.Serialize(kuro);
            string serializedTora = JsonSerializer.Serialize(tora);

            var CC = new CarController();

            var results = CC.Get();

            Assert.IsTrue(results.Contains(serializedMidori));
            Assert.IsTrue(results.Contains(serializedShiro));
            Assert.IsTrue(results.Contains(serializedKuro));

        }

        [TestMethod]
        public void testGetSpecificCar()
        {
            string serializedMidori = JsonSerializer.Serialize(midori);
            string serializedShiro = JsonSerializer.Serialize(shiro);
            string serializedKuro = JsonSerializer.Serialize(kuro);
            string serializedTora = JsonSerializer.Serialize(tora);

            var CC = new CarController();

            var midoriRslt = CC.Get(midori.Id.ToString());
            var shiroRslt = CC.Get(shiro.Id.ToString());
            var kuroRslt = CC.Get(kuro.Id.ToString());
            var toraRslt = CC.Get(tora.Id.ToString());

            Assert.AreEqual(midoriRslt, serializedMidori);
            Assert.AreEqual(shiroRslt, serializedShiro);
            Assert.AreEqual(kuroRslt, serializedKuro);
            Assert.AreEqual(toraRslt, serializedTora);

        }

        [TestMethod]
        public void testPostCar()
        {
            var grammamobile = new Car()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                Make = "Volvo",
                Model = "240",
                Year = "1989",
                Description = "Grammamobile",
                Number = "89"
            };

            var serializedGM = JsonSerializer.Serialize(grammamobile);

            var CC = new CarController();

            CC.Post(serializedGM);

            // Now that we've posted it, let's dig straight into the DB and see if it is there!
            using (var context = new autocrossContext())
            {
                var gm = (from c in context.Car
                          where c.Id == grammamobile.Id
                          select c).ToList().First();

                Assert.AreEqual(gm.Id, grammamobile.Id);
                Assert.AreEqual(gm.Make, grammamobile.Make);
                Assert.AreEqual(gm.Model, grammamobile.Model);
                Assert.AreEqual(gm.Year, grammamobile.Year);
                Assert.AreEqual(gm.Description, grammamobile.Description);
                Assert.AreEqual(gm.Number, grammamobile.Number);

            }
        }

        [TestMethod]
        public void testPutLocation()
        {
            Car kuroUpdate = new Car()
            {
                Description = "Brake Problems",
            };

            var CC = new CarController();

            CC.Put(kuro.Id.ToString(), JsonSerializer.Serialize(kuroUpdate));

            // Now check the database to see if we have changed only what we expected of the Black Lake entry
            using (var context = new autocrossContext())
            {
                var kr = (from c in context.Car
                          where c.Id == kuro.Id
                          select c).ToList().First();

                Assert.AreEqual(kr.Id, kuro.Id);
                Assert.AreEqual(kr.Description, "Brake Problems");
                Assert.AreEqual(kr.Make, kuro.Make);
                Assert.AreEqual(kr.Model, kuro.Model);
                Assert.AreEqual(kr.Year, kuro.Year);
                Assert.AreEqual(kr.Number, kuro.Number);

            }
        }

        [TestMethod]
        public void testDeleteLocation()
        {
            var CC = new CarController();

            CC.Delete(midori.Id.ToString());

            // We deleted ZMaxx so check to see if it is gone from the Database!
            using (var context = new autocrossContext())
            {
                var mdri = (from c in context.Car
                           where c.Id == midori.Id
                           select c).ToList().Count();

                Assert.IsTrue(mdri == 0);
            }
        }
    }
}
