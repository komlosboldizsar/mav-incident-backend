using mav_incident_dba;
using mav_incident_dba.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mav_incident_processor
{
    public class RssFeedProcessor
    {

        private string url;

        public RssFeedProcessor(string url)
        {
            this.url = url;
        }

        public int Do()
        {
            int processedItems = 0;
            string xml = HttpReader.Read(url);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode channelNode = xmlDoc.DocumentElement.FirstChild;
            foreach(XmlNode node in channelNode.ChildNodes)
            {
                if (node.Name == "item")
                {
                    bool processed = processItem(node);
                    if (processed)
                        processedItems++;
                }
            }
            return processedItems;
        }

        private bool processItem(XmlNode node)
        {

            int? guid = null;
            foreach (XmlNode attributeNode in node.ChildNodes)
                if (attributeNode.Name == "guid")
                    if (int.TryParse(attributeNode.InnerText, out int nodeValue))
                        guid = nodeValue;
            if (guid == null)
                return false;

            Incident incidentWithId = IncidentDatabase.Instance.Context.Incidents.FirstOrDefault(i => (i.ID == (int)guid));
            if (incidentWithId != null)
                return false;

            SingleIncidentProcessor sproc = new SingleIncidentProcessor((int)guid);
            return sproc.Do();

        }

    }
}
