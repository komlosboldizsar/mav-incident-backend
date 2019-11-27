using CommandLine;
using mav_incident_dba;
using mav_incident_processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mav_incident_processor_service
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o => Do(o))
                   .WithNotParsed<Options>((errs) => HandleParseError(errs));
        }
        private static void Do(Options options)
        {

            if (!IncidentDatabase.Instance.Init())
            {
                Console.WriteLine("Couldn't connect to database. See debug output for details.");
                Console.ReadKey();
                return;
            }

            OldEntryUpdater oldEntryUpdater = new OldEntryUpdater(options.UpdateOldMaxAge, false);
            RssFeedProcessor feedProcessor = new RssFeedProcessor(options.FeedURL);
            int oldEntryUpdaterTimer = 0;
            int feedProcessorTimer = 0;
            do
            {
                if ((oldEntryUpdaterTimer == 0) && !options.NoUpdateOld)
                    oldEntryUpdater.Do();
                if ((feedProcessorTimer == 0) && !options.NoProcessNews)
                    feedProcessor.Do();
                oldEntryUpdaterTimer++;
                feedProcessorTimer++;
                if (oldEntryUpdaterTimer == options.UpdateOldFrequency)
                    oldEntryUpdaterTimer = 0;
                if (feedProcessorTimer == options.ProcessNewsFrequency)
                    feedProcessorTimer = 0;
                if (!options.RunOnce)
                    Thread.Sleep(60000);
            } while (!options.RunOnce);

            IncidentDatabase.Instance.DeInit();

        }
        private static void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (Error e in errs)
                Console.WriteLine(e.ToString());
        }
        class Options
        {

            [Option('o', "run-once", Default = false, HelpText = "Run only once or in a loop.", Required = false)]
            public bool RunOnce { get; set; }

            [Option('f', "feed-url", Default = "https://www.mavcsoport.hu/mavinform/rss.xml", HelpText = "URL of MÁVINFORM RSS feed.", Required = false)]
            public string FeedURL { get; set; }

            [Option("no-update-old", Default = false, HelpText = "Update older, known entries.", Required = false)]
            public bool NoUpdateOld { get; set; }

            [Option("update-old-age", Default = 14, HelpText = "Maximal age of entries that are updated.", Required = false)]
            public int UpdateOldMaxAge { get; set; }

            [Option("update-old-frequency", Default = 10, HelpText = "Frequency of updating old incident entries in minutes.", Required = false)]
            public int UpdateOldFrequency { get; set; }

            [Option("no-process-news", Default = false, HelpText = "Process new feed items and add them to the database.", Required = false)]
            public bool NoProcessNews { get; set; }

            [Option("process-news-frequency", Default = 5, HelpText = "Frequency of processing news feed in minutes.", Required = false)]
            public int ProcessNewsFrequency { get; set; }

        }

    }
}
