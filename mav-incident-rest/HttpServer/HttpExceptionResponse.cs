using mav_incident_rest.HttpServer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer
{
    public class HttpExceptionResponse : HttpResponse
    {
        public HttpExceptionResponse(Exception e)
        {
            HttpErrorCodeException eTyped = e as HttpErrorCodeException;
            ResponseCode = (eTyped != null) ? eTyped.ReponseCode : HttpResponseCode.S_500_InternalError;
            Body = string.Format("<h1>{0} {1}</h1>We are sorry. Something happened.<br>Maybe this can help: {2}",
                (int)ResponseCode,
                ResponseCode.ToReasonPhrase(),
                e.Message);
        }
    }
}
