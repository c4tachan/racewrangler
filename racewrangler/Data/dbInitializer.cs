using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace racewrangler.Data
{
    public class dbInitializer
    {
        static public void Initialize(racewranglerContext context)
        {
            InitializeSites(context);

            //InitializeOrganizers(context);

            resultsParser.ParseResultsDirectory(context);
        }

        private static void InitializeOrganizers(racewranglerContext context)
        {
            if (context.Organizers.Any())
            {
                return;
            }

            context.Organizers.Add(new Models.Organizer()
            {
                Name = "Central Carolinas Region"
            });

            context.SaveChanges();
        }

        private static void InitializeSites(racewranglerContext context)
        {
            if (context.Sites.Any())
            {
                return;
            }

            context.Sites.Add(new Models.Site()
            {
                Name = "ZMax Dragway",
                Address = "6570 Bruton Smith Blvd",
                City = "Concord",
                State = "NC",
                Zip = "28027"
            });

            context.Sites.Add(new Models.Site()
            {
                Name = "Michelin Proving Grounds",
                Address = "2440 SC-39",
                City = "Mountville",
                State = "SC",
                Zip = "29370"
            });

            context.SaveChanges();
        }
    }
}
