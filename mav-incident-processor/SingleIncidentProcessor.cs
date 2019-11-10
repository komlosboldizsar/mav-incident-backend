using mav_incident_dba;
using mav_incident_dba.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;

namespace mav_incident_processor
{
    public class SingleIncidentProcessor
    {

        private int id;

        private Incident incident;

        private bool newEntity = false;

        private IncidentData data;

        public SingleIncidentProcessor(int id)
        {
            this.id = id;
            this.data = new IncidentData();
        }

        public bool Do(bool force = false)
        {
            if (!loadEntity())
            {
                incident = new Incident();
                newEntity = true;
            }
            if (!getData())
                return false;
            if (force || (incident.Hash != data.Hash()))
            {
                updateEntity();
                saveEntity();
            }
            return false;
        }

        private bool getData()
        {
            string url = idToUrl(id);
            string content = HttpReader.Read(url);
            processContent(content);
            processTimestamps();
            if (data.SomethingNull())
                return false;
            return true;
        }

        private bool loadEntity()
        {
            incident = IncidentDatabase.Instance.Context.Incidents.FirstOrDefault(i => (i.ID == id));
            return (incident != null);
        }

        private void updateEntity()
        {
            if (newEntity)
                incident.ID = id;
            incident.Name = data.Title;
            incident.Description = data.Content;
            incident.CreationTimestamp = (int)data.CreateTimestamp;
            incident.UpdateTimestamp = (int)data.UpdateTimestamp;
            incident.ProcessTimestamp = DateTime.Now.UnixTimestamp();
            incident.Hash = data.Hash();
        }

        private void saveEntity()
        {
            if (newEntity)
                IncidentDatabase.Instance.Context.Incidents.Add(incident);
            IncidentDatabase.Instance.Context.SaveChanges();
        }

        private void processContent(string content)
        {

            string titleStartTag = "<h1 class=\"title\" id=\"page-title\">";
            string titleEndTag = "</h1>";
            data.Title = getBetweenTags(content, titleStartTag, titleEndTag);

            string timestampStartTag = "<div class=\"node-date\">";
            string timestampEndTag = "</div>";
            data.Timestamps = getBetweenTags(content, timestampStartTag, timestampEndTag);

            string contentStartTag = "<div class=\"field-body\">";
            string contentEndTag = "</div>";
            data.Content = getBetweenTags(content, contentStartTag, contentEndTag);

        }

        private string getBetweenTags(string text, string startTag, string endTag)
        {
            
            int startTagPosition = text.IndexOf(startTag);
            if (startTagPosition < 0)
                return null;

            int endTagPosition = text.IndexOf(endTag, startTagPosition + 1);
            if (endTagPosition < 0)
                return null;

            int valueStartPosition = startTagPosition + startTag.Length;
            int valueLength = endTagPosition - valueStartPosition;
            string value = text.Substring(valueStartPosition, valueLength);
            return value.TrimStart().TrimEnd();

        }

        private void processTimestamps()
        {
            if (data.Timestamps == null)
                return;
            string[] parts = data.Timestamps.Split(new string[] { " / Utolsó módosítás: " }, StringSplitOptions.None);
            if (parts.Length != 2)
                return;
            data.CreateTimestamp = textToTimestamp(parts[0]);
            data.UpdateTimestamp = textToTimestamp(parts[1]);
        }

        private int? textToTimestamp(string text)
        {

            string[] parts = text.Split(new char[] { '.', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!int.TryParse(parts[0], out int year) || (year < 1900) || (year > 2100))
                return null;
            int? month = parts[1].ToMonth();
            if (month == null)
                return null;
            if (!int.TryParse(parts[2], out int day) || (day < 1) || (day > 31))
                return null;
            if (!int.TryParse(parts[4], out int hour) || (hour < 0) || (hour > 23))
                return null;
            if (!int.TryParse(parts[5], out int minute) || (minute < 0) || (minute > 59))
                return null;

            return (new DateTime(year, (int)month, day, hour, minute, 0)).UnixTimestamp();

        }

        private static string idToUrl(int id)
        {
            return string.Format("https://www.mavcsoport.hu/node/{0}", id);
        }

    }
}
