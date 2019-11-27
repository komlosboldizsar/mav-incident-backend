using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mav_incident_processor_service
{

    public class ConfigReader
    {

        private string configPath;

        public ConfigReader(string configPath)
        {
            this.configPath = configPath;
            read();
        }

        public string FeedURL { get; private set; }

        private const string TAG_ROOT = "config";

        private const string TAG_FEEDURL = "feed_url";

        private void read()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);
            XmlNode root = doc.DocumentElement;
            if (root.LocalName != TAG_ROOT)
                throw new Exception(string.Format("Root tag in configuration XML file must be {0}!", TAG_ROOT));
        
            foreach (XmlNode childNode in root.ChildNodes)
            {
                string value = childNode.InnerText;
                switch (childNode.LocalName)
                {
                    case TAG_FEEDURL:
                        FeedURL = value;
                        break;
                }
            }

            if (FeedURL == null)
                throw new Exception("No RSS feed URL setting found in configuration file!");

        }

    }

}
