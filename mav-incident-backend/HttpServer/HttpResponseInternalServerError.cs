using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_backend.HttpServer
{
    public class HttpResponseInternalServerError : HttpResponse
    {
        public HttpResponseInternalServerError(Exception e)
        {
            this.ResponseCode = HttpResponseCode.S_500_InternalError;
            this.Headers["Server"] = "IGNO1V";
            this.Body = "<h1>500 Internal Server Error</h1>Ooops, something bad happened. Sorry!<br>Error message: " + e.Message;
        }
    }
}
