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
    public class IncidentListingByLocation : RestService.Endpoints.RestEndpoint
    {

        public override string Path => "/incident/by-location/{id:i}";

        public override HttpRequestMethod Method => HttpRequestMethod.GET;

        protected override RestResponse process(HttpRequest request, Dictionary<string, string> urlParams)
        {

            if (!urlParams.ContainsKey("id") || !int.TryParse(urlParams["id"], out int id) || id <= 0)
                throw new BadRequestException();

            Location location = IncidentDatabase.Instance.Context.Locations.FirstOrDefault(l => (l.ID == id));
            if (location == null)
                throw new NotFoundException();

            RestResponse resp = RestResponse.GetDefault();

            List<IncidentListingEntry> incidentEntries = new List<IncidentListingEntry>();
            foreach (var incident in location.Incidents)
                incidentEntries.Add(new IncidentListingEntry(incident));

            resp.Body = incidentEntries;
            return resp;

        }

    }
}
