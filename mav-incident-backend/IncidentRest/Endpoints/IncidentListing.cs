using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_backend.IncidentRest.JsonObjects;
using mav_incident_dba;
using mav_incident_dba.Entities;
using mav_incident_rest.HttpServer;
using mav_incident_rest.HttpServer.Exceptions;
using mav_incident_rest.RestService.Endpoints;

namespace mav_incident_rest.IncidentRest.Endpoints
{
    public class IncidentListing : RestService.Endpoints.RestEndpoint
    {

        public override string Path => "/incident";

        public override HttpRequestMethod Method => HttpRequestMethod.GET;

        protected override RestResponse process(HttpRequest request, Dictionary<string, string> urlParams)
        {

            RestResponse resp = RestResponse.GetDefault();

            List<IncidentListingEntry> incidentEntries = new List<IncidentListingEntry>();
            foreach (var incident in IncidentDatabase.Instance.Context.Incidents)
                incidentEntries.Add(new IncidentListingEntry(incident));

            resp.Body = incidentEntries;
            return resp;

        }

    }
}
