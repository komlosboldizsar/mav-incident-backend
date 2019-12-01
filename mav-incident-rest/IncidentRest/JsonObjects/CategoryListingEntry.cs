using mav_incident_dba.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_backend.IncidentRest.JsonObjects
{

    public class CategoryListingEntry
    {

        protected Category dbEntry;

        public CategoryListingEntry(Category dbEntry)
        {
            this.dbEntry = dbEntry;
        }

        [JsonProperty("id")]
        protected virtual int ID => dbEntry.ID;

        [JsonProperty("name")]
        protected virtual string Name => dbEntry.Name;

        [JsonProperty("description")]
        protected virtual string Description => dbEntry.Description;

        [JsonProperty("incidents_count")]
        protected virtual int Updated => dbEntry.Incidents.Count;

    }

}
