using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_rest.RestService.Endpoints;
using mav_incident_rest.IncidentRest.Endpoints;
using mav_incident_dba;
using System.Diagnostics;

namespace mav_incident_rest.IncidentRest
{
    public class IncidentRestService : RestService.RestService
    {

        public IncidentRestService(int port) : base(port)
        { }

        public override bool Start()
        {
            if (!IncidentDatabase.Instance.Init())
            {
                Debug.WriteLine("Couldn't connect to database.");
                return false;
            }
            return base.Start();
        }

        public override void Stop()
        {
            base.Stop();
            IncidentDatabase.Instance.DeInit();
        }

        protected override List<RestEndpoint> getEndpoints()
        {
            List<RestEndpoint> endpoints = new List<RestEndpoint>();
            endpoints.Add(new IncidentListing());
            endpoints.Add(new IncidentDetails());
            endpoints.Add(new IncidentListingByCategory());
            endpoints.Add(new IncidentListingByDate());
            endpoints.Add(new IncidentListingByLocation());
            endpoints.Add(new IncidentLocations());
            endpoints.Add(new IncidentRefresh());
            endpoints.Add(new LocationListing());
            endpoints.Add(new CategoryListing());
            return endpoints;
        }

    }
}
