using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_dba.Entities;
using Newtonsoft.Json;

namespace mav_incident_backend.IncidentRest.JsonObjects
{

    public class LocationShortEntry
    {

        protected Location dbEntry;

        public LocationShortEntry(Location dbEntry)
        {
            this.dbEntry = dbEntry;
        }

        [JsonProperty("id")]
        protected virtual int ID => dbEntry.ID;

        [JsonProperty("name")]
        protected virtual string Name => dbEntry.Name;

    }

}
