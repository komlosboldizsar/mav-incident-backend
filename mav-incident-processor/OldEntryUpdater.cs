using mav_incident_dba;
using mav_incident_dba.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_processor
{
    public class OldEntryUpdater
    {

        private int maximalAge;

        private bool force;
        public OldEntryUpdater(int maximalAge, bool force)
        {
            this.maximalAge = maximalAge;
            this.force = force;
        }

        public int Do()
        {
            int updated = 0;
            DateTime todayStart = DateTime.Now - DateTime.Now.TimeOfDay;
            TimeSpan maximalDifference = new TimeSpan(maximalAge, 0, 0, 0);
            DateTime lookupStart = todayStart - maximalDifference;
            int lookupStartTimestamp = lookupStart.UnixTimestamp();
            List<Incident> incidentsToUpdate = new List<Incident>();
            incidentsToUpdate.AddRange(IncidentDatabase.Instance.Context.Incidents.Where(i => (i.UpdateTimestamp >= lookupStartTimestamp)));
            foreach(Incident incident in incidentsToUpdate)
            {
                SingleIncidentProcessor sproc = new SingleIncidentProcessor(incident.ID);
                sproc.Do(force);
            }
            return updated;
        }

    }
}
