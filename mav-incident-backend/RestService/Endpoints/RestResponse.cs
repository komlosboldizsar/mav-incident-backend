using mav_incident_rest.HttpServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.RestService.Endpoints
{
    public class RestResponse
    {

        public HttpResponseCode ResponseCode { get; set; }
        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();
        public object Body { get; set; }
        
        public static RestResponse GetDefault()
        {
            RestResponse resp = new RestResponse();
            resp.Headers["Content-Type"] = "application/json; charset=utf-8";
            resp.ResponseCode = HttpResponseCode.S_200_Success;
            return resp;
        }

    }
}
