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
    public class IncidentListingByCategory : RestService.Endpoints.RestEndpoint
    {

        public override string Path => "/incident/by-category/{id:i}";

        public override HttpRequestMethod Method => HttpRequestMethod.GET;

        protected override RestResponse process(HttpRequest request, Dictionary<string, string> urlParams)
        {

            if (!urlParams.ContainsKey("id") || !int.TryParse(urlParams["id"], out int id) || id <= 0)
                throw new BadRequestException();

            Category category = IncidentDatabase.Instance.Context.Categories.FirstOrDefault(c => (c.ID == id));
            if (category == null)
                throw new NotFoundException();

            RestResponse resp = RestResponse.GetDefault();

            List<IncidentListingEntry> incidentEntries = new List<IncidentListingEntry>();
            foreach (var incident in category.Incidents)
                incidentEntries.Add(new IncidentListingEntry(incident));

            resp.Body = incidentEntries;
            return resp;

        }

    }
}
