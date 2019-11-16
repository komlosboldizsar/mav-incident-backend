using mav_incident_dba;
using mav_incident_dba.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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
                getCategoriesAndLocations();
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
            {
                incident.ID = id;
                incident.Categories = new List<Category>();
                incident.Locations = new List<Location>();
            }
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

        private void getCategoriesAndLocations()
        {
            getWords();
            getCategories();
            getLocations();
        }

        private List<string> words;

        private void getWords()
        {
            string descr = incident.Description;
            descr = Regex.Replace(descr, @"<([^>]+)>", (m) => "");
            descr = descr.Replace("&nbsp", "");
            string[] descrSplit = descr.Split(new char[] { ' ', '.', ':', ',', ';', '-' }, StringSplitOptions.RemoveEmptyEntries);
            words = new List<string>(descrSplit);
        }

        private void getCategories()
        {
            List<Category> categories = new List<Category>();
            List<Category> allCategories = new List<Category>(IncidentDatabase.Instance.Context.Categories);
            foreach (Category category in allCategories)
                if (checkCategory(category))
                    categories.Add(category);
            incident.Categories = categories;
        }

        private bool checkCategory(Category category)
        {
            bool include = false;
            List<CategoryFilter> allFilters = new List<CategoryFilter>();
            allFilters.AddRange(category.Filters);
            foreach(CategoryFilter filter in allFilters)
            {
                int count = 0;
                foreach(string word in filter.Words)
                    if (words.Contains(word))
                        count++;
                if (count >= filter.Wordlimit)
                {
                    if (filter.Type == CriteriaType.Exclude)
                        return false;
                    else if (filter.Type == CriteriaType.Include)
                        include = true;
                }
            }
            return include;
        }

        private void getLocations()
        {
            List<Location> locations = new List<Location>();
            List<Location> allLocations = new List<Location>(IncidentDatabase.Instance.Context.Locations);
            foreach (Location location in allLocations)
                if (words.Contains(location.Name))
                    locations.Add(location);
            incident.Locations = allLocations;
        }

    }
}
