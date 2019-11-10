using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_backend.IncidentRest.JsonObjects;
using mav_incident_dba;
using mav_incident_rest.HttpServer;
using mav_incident_rest.HttpServer.Exceptions;
using mav_incident_rest.RestService.Endpoints;

namespace mav_incident_rest.IncidentRest.Endpoints
{
    public class IncidentListingByDate : RestService.Endpoints.RestEndpoint
    {

        public override string Path => "/incident/by-date/{start:i},{end:i}";

        public override HttpRequestMethod Method => HttpRequestMethod.GET;

        protected override RestResponse process(HttpRequest request, Dictionary<string, string> urlParams)
        {

            if (!urlParams.ContainsKey("start") || !int.TryParse(urlParams["start"], out int start) || start <= 0)
                throw new BadRequestException();

            if (!urlParams.ContainsKey("end") || !int.TryParse(urlParams["end"], out int end) || end <= 0)
                throw new BadRequestException();

            RestResponse resp = RestResponse.GetDefault();

            List<IncidentListingEntry> incidentEntries = new List<IncidentListingEntry>();
            var incidents = IncidentDatabase.Instance.Context.Incidents.Where(i => ((i.Timestamp >= start) && (i.Timestamp <= end)));
            foreach (var incident in incidents)
                incidentEntries.Add(new IncidentListingEntry(incident));

            resp.Body = incidentEntries;
            return resp;

        }

    }
}
