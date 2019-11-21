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

        protected Incident dbEntry;

        public IncidentListingEntry(Incident dbEntry)
        {
            this.dbEntry = dbEntry;
        }

        [JsonProperty("title")]
        protected virtual string Title => dbEntry.Name;

        [JsonProperty("created")]
        protected virtual int Created => dbEntry.CreationTimestamp;

        [JsonProperty("updated")]
        protected virtual int Updated => dbEntry.UpdateTimestamp;

        [JsonProperty("processed")]
        protected virtual int Processed => dbEntry.ProcessTimestamp;

        [JsonProperty("locations")]
        protected virtual List<LocationShortEntry> Locations {
            get
            {
                List<LocationShortEntry> locations = new List<LocationShortEntry>();
                foreach (var location in dbEntry.Locations)
                    locations.Add(new LocationShortEntry(location));
                return locations;
            }
        }

        [JsonProperty("categories")]
        protected virtual List<CategoryShortEntry> Categories
        {
            get
            {
                List<CategoryShortEntry> categories = new List<CategoryShortEntry>();
                foreach (var category in dbEntry.Categories)
                    categories.Add(new CategoryShortEntry(category));
                return categories;
            }
        }

    }

}
