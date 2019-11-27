using CommandLine;
using mav_incident_dba;
using mav_incident_processor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private const string CONFIG_PATH = "rss-config.xml";
        private static ConfigReader configReader;
        private static void Do(Options options)
        {

            try
            {
                configReader = new ConfigReader(CONFIG_PATH);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't read RSS configuration. Reason: " + ex.Message);
                Console.ReadKey();
                return;
            }

            string feedUrl = options.FeedURL ?? configReader.FeedURL;

            if (!IncidentDatabase.Instance.Init())
            {
                Console.WriteLine("Couldn't connect to database. See debug output for details.");
                Console.ReadKey();
                return;
            }

            OldEntryUpdater oldEntryUpdater = new OldEntryUpdater(options.UpdateOldMaxAge, false);
            RssFeedProcessor feedProcessor = new RssFeedProcessor(feedUrl);
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

            [Option('f', "feed-url", Default = null, HelpText = "URL of MÁVINFORM RSS feed. Read from configuration XML file if not specified.", Required = false)]
            public string FeedURL { get; set; }

            [Option("no-update-old", Default = false, HelpText = "Don't update older, known entries.", Required = false)]
            public bool NoUpdateOld { get; set; }

            [Option("update-old-age", Default = 14, HelpText = "Maximal age of entries that are updated.", Required = false)]
            public int UpdateOldMaxAge { get; set; }

            [Option("update-old-frequency", Default = 10, HelpText = "Frequency of updating old incident entries in minutes.", Required = false)]
            public int UpdateOldFrequency { get; set; }

            [Option("no-process-news", Default = false, HelpText = "Don't process new feed items.", Required = false)]
            public bool NoProcessNews { get; set; }

            [Option("process-news-frequency", Default = 5, HelpText = "Frequency of processing news feed in minutes.", Required = false)]
            public int ProcessNewsFrequency { get; set; }

        }

    }
}
