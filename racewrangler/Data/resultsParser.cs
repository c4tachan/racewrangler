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
                var season = (from s in context.Seasons
                              where s.Year == int.Parse(d.Name)
                              select s).FirstOrDefault();

                // Don't continuously add seasons, use an existing one if possible!
                if (season == default(Season))
                {
                    season = new Models.Season()
                    {
                        Year = int.Parse(d.Name),
                        Competitions = new List<Competition>()
                    };
                }

                // Now check each file in the directory to see if it has been loaded
                // If not, load it into the database!
                foreach (var f in d.EnumerateFiles())
                {
                    var fileLoaded = (from c in context.Competitions
                                      where c.ResultsSource == f.FullName
                                      select c).Any();
                    if (!fileLoaded)
                    {

                        var comp = new Models.Competition()
                        {
                            Entrants = new List<RaceEntry>()
                        };

                        ParseCompetition(context, comp, f);

                        comp.ResultsSource = f.FullName;

                        season.Competitions.Add(comp);
                    }
                }

                context.Seasons.Update(season);
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

            var resultsNodes = resultsRegion.SelectSingleNode(".//table").SelectNodes(".//tbody//tr");

            Classification cls = new Classification();
            Driver drvr = new Driver();
            RaceEntry re = null;

            int colorCol = 5;
            int timesColStart = 6;

            foreach (var nd in resultsNodes)
            {
                //int entryCount = 0;
                

                var headerNodes = nd.SelectNodes(".//th");
                // Check to see if we're crossing into a new class's results
                if (headerNodes != null)
                {
                    ParseClassName(context, cls, headerNodes.First());
                    colorCol = int.Parse(headerNodes.First().Attributes["colspan"].Value);

                    int col = 0;
                    foreach (var nod in headerNodes)
                    {
                        if(nod.InnerText == "Times")
                        {
                            timesColStart = col + colorCol;
                            break;
                        }
                        col++;
                    }

                    
                }
                else    // Parse runs
                {
                    var detNodes = nd.SelectNodes(".//td");

                    // Check to see if there is a car number here. This is the first row and the start of a new entry
                    if(detNodes[2].InnerText != string.Empty)
                    {
                        // If we have a car number, then this is a new driver, save the old driver before proceeding!
                        if (re != null)
                        {
                            LinkDriver(context, drvr);

                            re.Driver = drvr;

                            comp.Entrants.Add(re);
                        }

                        drvr = new Driver();

                        re = new RaceEntry()
                        {
                            Class = cls,
                            Competition = comp,
                            Driver = new Driver(),
                            Number = detNodes[2].InnerText
                        };

                        var names = detNodes[3].InnerText.Split(", ");

                        drvr.LastName = names[0];
                        // Be defensive, what if someone has only one name?
                        if(names.Length > 1)
                        {
                            drvr.FirstName = names[1];
                        }

                        // Parse the car
                        string carDesc = detNodes[4].InnerText;

                        re.Car = (from c in context.Cars
                                  where c.Description == carDesc
                                  select c).FirstOrDefault();
                        // If the car didn't exist, Create a new one!
                        if(re.Car == default(Car))
                        {
                            re.Car = new Car()
                            {
                                Description = carDesc,
                                Color = detNodes[colorCol].InnerText
                            };
                        }

                        re.Runs = ParseRuns(detNodes.ToList().GetRange(timesColStart, (detNodes.Count - 1 - timesColStart)));

                    }
                    // If there isn't then it's the continuation of a driver's runs
                    else
                    {
                        if (re != null)
                        {
                            // If there is a member number, parse it out!
                            if (detNodes[3].InnerText != string.Empty)
                            {
                                drvr.MemberNumber = detNodes[3].InnerText;
                            }
                            // Get the sponsor
                            re.Sponsor = detNodes[4].InnerText;

                            re.Runs = ParseRuns(detNodes.ToList().GetRange(timesColStart, (detNodes.Count - 1 - timesColStart)));
                        }
                    }


                    //entryCount--;
                }
                
            }


            return;
        }

        // This is going to get tricky, the idea is to link drivers with already exiting
        // driver records if possible.
        private static void LinkDriver(racewranglerContext context, Driver drvr)
        {
            
            // First check to see if there is a record with a member number matching this driver
            var possibleDriversByNumber = from d in context.Drivers
                                          where d.MemberNumber == drvr.MemberNumber
                                          select d;

            if(possibleDriversByNumber.Count() == 1)
            {
                drvr = possibleDriversByNumber.First();
            }
            // No member number exists, so let's try to name match
            else
            {
                var possibleDriversByName = (from d in context.Drivers
                                             where d.LastName == drvr.LastName
                                             where d.FirstName == drvr.FirstName
                                             select d);

                // If there is only one driver by name, this is easy!
                if(possibleDriversByName.Count() == 1)
                {
                    drvr = possibleDriversByName.First();
                }
                // If there is more than one driver by name, things get complicated.
                else
                {
                    // I really have no idea what to do here...
                }
            }

            return;
        }

        private static List<Run> ParseRuns(List<HtmlNode> list)
        {
            var runs = new List<Run>();
            int count = 1;
            foreach (var nd in list)
            {
                if(nd.InnerText != string.Empty)
                {
                    runs.Add(ParseRun(nd, count));
                }
            }

            return runs;
        }

        private static Run ParseRun(HtmlNode nd, int count)
        {
            var runFields = nd.InnerText.Split("+");

            var run = new Run()
            {
                LapTime = float.Parse(runFields.First()),
                RunNumber = count
            };


            // If we have a penalty record it!
            if(runFields.Length > 1)
            {
                int penaltyCount = 0;
                if(!int.TryParse(runFields[1], out penaltyCount))
                {
                    run.DNF = "dnf" == runFields[1];
                }
                run.Penalties = penaltyCount;
            }

            return run;
        }

        private static void ParseClassName(racewranglerContext context, Classification cls, HtmlNode htmlNode)
        {
            string abbrv = "";
            //int entryCount = 0;

            var parts = htmlNode.InnerText.Split(" - ");

            if (parts.Length == 3)
            {
                abbrv = parts[0].Replace("'", string.Empty);

                // Identify the class (add it to the db if necessary!)
                cls = (from c in context.Classes
                       where c.Abbreviation == abbrv
                       select c).FirstOrDefault();

                if (cls == default(Classification))
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
                var items = parts.Last().Split(": ");

                //entryCount = int.Parse(parts.Last().Split(": ")[1].Trim());
            }
            else if (parts.Length == 2) // Sometimes class headers don't have a full name in them
            {
                abbrv = parts[0];

                // Identify the class (add it to the db if necessary!)
                cls = (from c in context.Classes
                       where c.Abbreviation == abbrv
                       select c).FirstOrDefault();

                if (cls == default(Classification))
                {
                    cls = new Classification()
                    {
                        Abbreviation = abbrv,
                    };
                    context.Classes.Add(cls);
                }
                //entryCount = int.Parse(parts.Last().Split(": ")[1]);
            }

            return;
        }

        private static void ParseClassList(racewranglerContext context, HtmlNodeCollection classlist)
        { 
            foreach(var cls in classlist)
            {
                // Check to see if the class already exists
                var cs = (from c in context.Classes
                          where c.Abbreviation == cls.InnerText
                          select c).FirstOrDefault();

                if(cs == default(Classification))
                {
                    context.Classes.Add(new Classification()
                    {
                        Abbreviation = cls.InnerText.Replace("'", string.Empty)
                    });
                }
            }


            return;
        }
    }
}
