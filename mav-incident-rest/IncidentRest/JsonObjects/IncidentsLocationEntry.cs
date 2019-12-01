using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_dba.Entities;
using Newtonsoft.Json;

namespace mav_incident_backend.IncidentRest.JsonObjects
{

    public class IncidentsLocationEntry
    {

        protected Location dbEntry;

        public IncidentsLocationEntry(Location dbEntry)
        {
            this.dbEntry = dbEntry;
        }

        [JsonProperty("label")]
        protected virtual string Label => dbEntry.Name;

        [JsonProperty("latitude")]
        protected virtual decimal Latitude => dbEntry.Latitude;

        [JsonProperty("longitude")]
        protected virtual decimal Longitude => dbEntry.Longitude;

    }

}
