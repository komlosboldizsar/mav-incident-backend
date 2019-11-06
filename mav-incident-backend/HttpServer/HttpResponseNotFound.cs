using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer
{
    public class HttpResponseNotFound : HttpResponse
    {
        public HttpResponseNotFound(HttpRequest request)
        {
            this.ResponseCode = HttpResponseCode.S_404_NotFound;
            this.Headers["Server"] = "IGNO1V";
            this.Body = "<h1>404 Internal Server Error</h1>Nooo, noo.";
        }
    }
}
