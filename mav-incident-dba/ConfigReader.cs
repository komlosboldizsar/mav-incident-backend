using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mav_incident_dba
{

    public class ConfigReader
    {

        private string configPath;

        public ConfigReader(string configPath)
        {
            this.configPath = configPath;
            read();
        }

        public string Host { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Database { get; private set; }

        private const string TAG_ROOT = "config";

        private const string TAG_HOST = "host";
        private const string TAG_USERNAME = "username";
        private const string TAG_PASSWORD = "password";
        private const string TAG_DATABASE = "database";

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
                    case TAG_HOST:
                        Host = value;
                        break;
                    case TAG_USERNAME:
                        Username = value;
                        break;
                    case TAG_PASSWORD:
                        Password = value;
                        break;
                    case TAG_DATABASE:
                        Database = value;
                        break;
                }
            }

            if (Host == null)
                throw new Exception("No host setting found in configuration file!");

            if (Username == null)
                throw new Exception("No database username setting found in configuration file!");

            if (Password == null)
                throw new Exception("No password for database user found in configuration file!");

            if (Database == null)
                throw new Exception("No database name setting found in configuration file!");

        }

    }

}
