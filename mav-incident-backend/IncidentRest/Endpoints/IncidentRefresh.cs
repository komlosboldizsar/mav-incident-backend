using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_dba;
using mav_incident_dba.Entities;
using mav_incident_processor;
using mav_incident_rest.HttpServer;
using mav_incident_rest.HttpServer.Exceptions;
using mav_incident_rest.RestService.Endpoints;

namespace mav_incident_rest.IncidentRest.Endpoints
{
    public class IncidentRefresh : RestService.Endpoints.RestEndpoint
    {

        public override string Path => "/incident/{id:i}/refresh";

        public override HttpRequestMethod Method => HttpRequestMethod.POST;

        protected override RestResponse process(HttpRequest request, Dictionary<string, string> urlParams)
        {

            if (!urlParams.ContainsKey("id") || !int.TryParse(urlParams["id"], out int id) || id <= 0)
                throw new BadRequestException("ID of incident must be a positive integer.");

            Incident incident = IncidentDatabase.Instance.Context.Incidents.FirstOrDefault(i => (i.ID == id));
            if (incident == null)
                throw new NotFoundException("Incident with the given ID is can't be found in the database.");

            SingleIncidentProcessor sproc = new SingleIncidentProcessor(id);
            sproc.Do();

            // TODO: error handling

            RestResponse resp = RestResponse.GetDefault();
            return resp;

        }

    }
}
