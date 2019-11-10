using mav_incident_dba.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_backend.IncidentRest.JsonObjects
{

    public class IncidentListingEntry
    {

        private Incident dbEntry;

        public IncidentListingEntry(Incident dbEntry)
        {
            this.dbEntry = dbEntry;
        }

        [JsonProperty("title")]
        private string Title => dbEntry.Name;

        [JsonProperty("updated")]
        private int Updated => dbEntry.Timestamp;

        [JsonProperty("processed")]
        private int Processed => dbEntry.Timestamp;

        [JsonProperty("locations")]
        private Dictionary<int, string> Locations {
            get
            {
                Dictionary<int, string> locations = new Dictionary<int, string>();
                foreach (var location in dbEntry.Locations)
                    locations.Add(location.ID, location.Name);
                return locations;
            }
        }

        [JsonProperty("categories")]
        private Dictionary<int, string> Categories
        {
            get
            {
                Dictionary<int, string> categories = new Dictionary<int, string>();
                foreach (var category in dbEntry.Categories)
                    categories.Add(category.ID, category.Name);
                return categories;
            }
        }

    }

}
