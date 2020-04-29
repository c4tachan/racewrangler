using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using racewrangler;

namespace racewranglerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        // GET: api/Car
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var carStrings = new List<string>();
            using (var context = new autocrossContext())
            {
                foreach (var car in context.Car.ToList())
                {
                    carStrings.Add(JsonSerializer.Serialize(car));
                }
            }

            return carStrings;
        }

        // GET: api/Car/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            using (var context = new autocrossContext())
            {
                Guid ident = Guid.Parse(id);

                Car car = (from c in context.Car
                           where c.Id == ident
                           select c).ToList().FirstOrDefault();

                string retVal = JsonSerializer.Serialize(car);
                return retVal;
            }
        }

        // POST: api/Car
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Car car = JsonSerializer.Deserialize<Car>(value);

            using (var context = new autocrossContext())
            {
                context.Car.Add(car);

                context.SaveChanges();
            }
        }

        // PUT: api/Car/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {
            Car car = JsonSerializer.Deserialize<Car>(value);

            using (var context = new autocrossContext())
            {
                Guid ident = Guid.Parse(id);

                Car cr = (from c in context.Car
                          where c.Id == ident
                          select c).ToList().FirstOrDefault();

                cr.Make = (null == car.Make) ? cr.Make : car.Make;
                cr.Model = (null == car.Model) ? cr.Model : car.Model;
                cr.Year = (null == car.Year) ? cr.Year : car.Year;
                cr.Description = (null == car.Description) ? cr.Description : car.Description;
                cr.Number = (null == car.Number) ? cr.Number : car.Number;

                context.SaveChanges();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            using (var context = new autocrossContext())
            {
                Guid ident = Guid.Parse(id);

                Car car = (from c in context.Car
                                where c.Id == ident
                                select c).ToList().FirstOrDefault();

                context.Car.Remove(car);

                context.SaveChanges();
            }
        }
    }
}
