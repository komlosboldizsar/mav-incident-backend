using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_backend.HttpServer;
using mav_incident_backend.RestService.Endpoints;

namespace mav_incident_backend.IncidentRest.Endpoints
{
    public class IncidentListingByDate : RestService.Endpoints.RestEndpoint
    {

        public override string Path => "/incident/by-date/{start:i},{end:i}";

        public override HttpRequestMethod Method => HttpRequestMethod.GET;

        protected override RestResponse process(HttpRequest request, Dictionary<string, string> urlParams)
        {
            RestResponse resp = RestResponse.GetDefault();
            resp.Body = "[0, 1, 2]";
            return resp;
        }

    }
}
