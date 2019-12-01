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

        [JsonProperty("id")]
        protected virtual int ID => dbEntry.ID;

        [JsonProperty("title")]
        protected virtual string Title => dbEntry.Name;

        [JsonProperty("created")]
        protected virtual int Created => dbEntry.CreationTimestamp;

        [JsonProperty("updated")]
        protected virtual int Updated => dbEntry.UpdateTimestamp;

        [JsonProperty("processed")]
        protected virtual int Processed => dbEntry.ProcessTimestamp;

        [JsonProperty("locations")]
        protected virtual List<LocationShortEntry> Locations 
        {
            get
            {
                List<LocationShortEntry> locationsJson = new List<LocationShortEntry>();
                List<Location> locationsOrdered = dbEntry.Locations.OrderBy(loc => loc.Name).ToList();
                locationsOrdered.ForEach(loc => locationsJson.Add(new LocationShortEntry(loc)));
                return locationsJson;
            }
        }

        [JsonProperty("categories")]
        protected virtual List<CategoryShortEntry> Categories
        {
            get
            {
                List<CategoryShortEntry> categoriesJson = new List<CategoryShortEntry>();
                List<Category> categoriesOrdered = dbEntry.Categories.OrderBy(cat => cat.Name).ToList();
                categoriesOrdered.ForEach(cat => categoriesJson.Add(new CategoryShortEntry(cat)));
                return categoriesJson;
            }
        }

    }

}
