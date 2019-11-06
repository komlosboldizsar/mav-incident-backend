using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mav_incident_rest.HttpServer;
using mav_incident_rest.RestService.Endpoints;

namespace mav_incident_rest.IncidentRest.Endpoints
{
    public class IncidentDetails : RestService.Endpoints.RestEndpoint
    {

        public override string Path => "/incident/{id:i}";

        public override HttpRequestMethod Method => HttpRequestMethod.GET;

        protected override RestResponse process(HttpRequest request, Dictionary<string, string> urlParams)
        {
            RestResponse resp = RestResponse.GetDefault();
            resp.Body = string.Format("[77, {0}]", urlParams["id"]);
            return resp;
        }

    }
}
