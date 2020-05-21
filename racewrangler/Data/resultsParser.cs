using HtmlAgilityPack;
using racewrangler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace racewrangler.Data
{
    public class resultsParser
    {
        public static void ParseResultsDirectory(racewranglerContext context)
        {
            var di = new DirectoryInfo(@"..\results");

            foreach (var d in di.EnumerateDirectories())
            {
                var season = new Models.Season()
                {
                    Year = int.Parse(d.Name),
                    Competitions = new List<Competition>()
                };

                foreach (var f in d.EnumerateFiles())
                {
                    var comp = new Models.Competition();
                    ParseCompetition(context, comp, f);

                    season.Competitions.Add(comp);
                }

                context.Seasons.Add(season);
            }

            context.SaveChanges();

            return;
        }

        private static void ParseCompetition(racewranglerContext context, Competition comp, FileInfo f)
        {
            var doc = new HtmlDocument();
            doc.Load(f.FullName);

            var body = doc.DocumentNode.SelectSingleNode("//body");

            ParseHeaderTable(context, comp, body.SelectSingleNode("//table"));
            ParseReultsTables(context, comp, body.SelectSingleNode("//a"));

            return;
        }

        // The header table is at the top of the page and contains organizer and event info
        private static void ParseHeaderTable(racewranglerContext context, Competition comp, HtmlNode headerTable)
        {
            var tbodyNode = headerTable.SelectSingleNode("//tbody");

            // Get the nodes of each table
            var trNodes = tbodyNode.SelectNodes(".//tr");
            var orgNode = trNodes[0].SelectSingleNode(".//th");
            var detNode = trNodes[2].SelectSingleNode(".//th");

            comp.Org = ParseOrganizer(context, orgNode);

            ParseDetails(context, comp, detNode);

            return;
        }

        

        // Parse the organizer name from the first row of the header table
        private static Organizer ParseOrganizer(racewranglerContext context, HtmlNode orgNode)
        {
            string orgName = orgNode.InnerHtml;

            var organizer = (from o in context.Organizers
                             where o.Name == orgName
                             select o).FirstOrDefault();
            // Make sure we found an organizer, or create one if we didn't.
            if (organizer == default(Organizer))
            {
                organizer = new Organizer()
                {
                    Name = orgName
                };

                context.Organizers.Add(organizer);
            }

            return organizer;
        }

        // Parse the details from the 3rd row of the header table
        private static void ParseDetails(racewranglerContext context, Competition comp, HtmlNode detNode)
        {
            string fullDetails = detNode.InnerHtml;

            var detItems = fullDetails.Split(" - ");

            // skip the first character (will be a '#')
            comp.EventNum = int.Parse(detItems[0].Substring(1));
            comp.Name = detItems[1];
            comp.Date = DateTime.Parse(detItems[2]);

            return;
        }

        private static void ParseReultsTables(racewranglerContext context, Competition comp, HtmlNode resultsRegion)
        {
            ParseClassList(context, resultsRegion.SelectNodes(".//center//a"));

            var resultsNodes = resultsRegion.SelectNodes(".//table//tbody//tr");

            Classification cls = new Classification();


            foreach (var nd in resultsNodes)
            {
                int entryCount = 0;

                var headerNodes = nd.SelectNodes(".//th");
                // Check to see if we're crossing into a new class's results
                if (headerNodes.Count > 0)
                {
                    entryCount = ParseClassName(context, headerNodes.First());
                }
                else if (entryCount > 0)    // Parse runs
                {
                    var detNodes = nd.SelectNodes(".//td");


                    entryCount--;
                }
                
            }


            return;
        }

        private static int ParseClassName(racewranglerContext context, HtmlNode htmlNode)
        {
            string abbrv = "";
            int entryCount = 0;

            var parts = htmlNode.InnerText.Split(" - ");

            abbrv = parts[0];

            // Identify the class (add it to the db if necessary!)
            var cls = (from c in context.Classes
                       where c.Abbreviation == abbrv
                       select c).FirstOrDefault();

            if(cls == default(Classification))
            {
                cls = new Classification()
                {
                    Abbreviation = abbrv,
                };
                context.Classes.Add(cls);
            }

            // Update the class's name
            cls.Name = parts[1].Replace("'", string.Empty);

            // Parse out how many entries there are in this class
            entryCount = int.Parse(parts.Last().Split(": ").Last());

            return entryCount;
        }

        private static void ParseClassList(racewranglerContext context, HtmlNodeCollection classlist)
        { 
            foreach(var cls in classlist)
            {
                context.Classes.Add(new Classification()
                {
                    Abbreviation = cls.InnerText
                });
            }


            return;
        }
    }
}
