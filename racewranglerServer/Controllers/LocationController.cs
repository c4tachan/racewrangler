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
    public class LocationController : ControllerBase
    {
        // GET: api/Location
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var locationStrings = new List<string>();
            using (var context = new autocrossContext())
            {
                foreach (var loc in context.Location.ToList())
                {
                    locationStrings.Add(JsonSerializer.Serialize(loc));
                }
            }

            return locationStrings;
        }

        // GET: api/Location/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            using (var context = new autocrossContext())
            {
                Guid ident = Guid.Parse(id);

                Location loc = (from l in context.Location
                               where l.Id == ident
                               select l).ToList().FirstOrDefault();

                string retVal = JsonSerializer.Serialize(loc);
                return retVal;
            }
        }

        // POST: api/Location
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Location loc = JsonSerializer.Deserialize<Location>(value);

            using (var context = new autocrossContext())
            { 
                context.Location.Add(loc);

                context.SaveChanges();
            }
        }

        // PUT: api/Location/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {
            Location loc = JsonSerializer.Deserialize<Location>(value);

            using (var context = new autocrossContext())
            {
                Guid ident = Guid.Parse(id);

                Location lc = (from l in context.Location
                               where l.Id == ident
                               select l).ToList().FirstOrDefault();

                lc.Name = (null == loc.Name) ? lc.Name: loc.Name;
                lc.City = (null == loc.City) ? lc.City : loc.City;
                lc.State = (null == loc.State) ? lc.State : loc.State;
                lc.Zip = (null == loc.Zip) ? lc.Zip : loc.Zip;

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

                Location lc = (from l in context.Location
                               where l.Id == ident
                               select l).ToList().FirstOrDefault();

                context.Location.Remove(lc);

                context.SaveChanges();
            }
        }
    }
}
