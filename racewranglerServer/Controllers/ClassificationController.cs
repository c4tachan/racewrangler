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
    public class ClassificationController : ControllerBase
    {
        // GET: api/Classification
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var ClassificationStrings = new List<string>();
            using (var context = new autocrossContext())
            {
                foreach (var cls in context.Classification.ToList())
                {
                    ClassificationStrings.Add(JsonSerializer.Serialize(cls));
                }
            }

            return ClassificationStrings;
        }

        // GET: api/Classification/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            using (var context = new autocrossContext())
            {
                Guid ident = Guid.Parse(id);

                Classification cls = (from c in context.Classification
                                where c.Id == ident
                                select c).ToList().FirstOrDefault();

                string retVal = JsonSerializer.Serialize(cls);
                return retVal;
            }
        }

        // POST: api/Classification
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Classification cls = JsonSerializer.Deserialize<Classification>(value);

            using (var context = new autocrossContext())
            {
                context.Classification.Add(cls);

                context.SaveChanges();
            }
        }

        // PUT: api/Classification/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {
            Classification cls = JsonSerializer.Deserialize<Classification>(value);

            using (var context = new autocrossContext())
            {
                Guid ident = Guid.Parse(id);

                Classification clsf = (from l in context.Classification
                               where l.Id == ident
                               select l).ToList().FirstOrDefault();

                clsf.Name = (null == cls.Name) ? clsf.Name : cls.Name;
                clsf.Abreviation = (null == cls.Abreviation) ? clsf.Abreviation : cls.Abreviation;
                clsf.Handicap = (null == cls.Handicap) ? clsf.Handicap : cls.Handicap;
                
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

                Classification cls = (from c in context.Classification
                                      where c.Id == ident
                                      select c).ToList().FirstOrDefault();

                context.Classification.Remove(cls);

                context.SaveChanges();
            }
        }
    }
}
