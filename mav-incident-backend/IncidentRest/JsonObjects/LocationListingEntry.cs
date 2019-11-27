using mav_incident_dba.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_backend.IncidentRest.JsonObjects
{

    public class LocationListingEntry
    {

        protected Location dbEntry;

        public LocationListingEntry(Location dbEntry)
        {
            this.dbEntry = dbEntry;
        }

        [JsonProperty("id")]
        protected virtual int ID => dbEntry.ID;

        [JsonProperty("name")]
        protected virtual string Name => dbEntry.Name;

        [JsonProperty("latitude")]
        protected virtual decimal Latitude => dbEntry.Latitude;

        [JsonProperty("longitude")]
        protected virtual decimal Longitude => dbEntry.Longitude;

        [JsonProperty("incidents_count")]
        protected virtual int Updated => dbEntry.Incidents.Count;

    }

}
