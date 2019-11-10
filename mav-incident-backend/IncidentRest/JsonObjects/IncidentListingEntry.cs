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
        private Dictionary<int, string> Locations => new Dictionary<int, string>() { { 3, "Győr" } };

        [JsonProperty("categories")]
        private Dictionary<int, string> Categories => new Dictionary<int, string>() { { 1, "késés" }, { 4, "biztber" } };

    }

}
