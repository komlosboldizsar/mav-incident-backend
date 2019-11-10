using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_dba.Entities;
using Newtonsoft.Json;

namespace mav_incident_backend.IncidentRest.JsonObjects
{

    public class IncidentDetailsEntry : IncidentListingEntry
    {

        public IncidentDetailsEntry(Incident dbEntry) : base(dbEntry)
        { }

        [JsonProperty("content")]
        protected virtual string Content => dbEntry.Description;

        [JsonProperty("url")]
        protected virtual string URL => dbEntry.URL;


    }

}
