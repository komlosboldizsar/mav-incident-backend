using mav_incident_rest.HttpServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer.Exceptions
{
    public class HttpErrorCodeException : Exception
    {

        public HttpResponseCode ReponseCode { get; private set; }

        public HttpErrorCodeException(HttpResponseCode responseCode)
            : base()
        {
            ReponseCode = responseCode;
        }

        public HttpErrorCodeException(string message, HttpResponseCode responseCode)
            : base(message)
        {
            ReponseCode = responseCode;
        }

        public HttpErrorCodeException(string message, HttpResponseCode responseCode, Exception innerException)
            : base(message, innerException)
        {
            ReponseCode = responseCode;
        }

    }
}
