using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer.Exceptions
{
    public class BadRequestException : HttpErrorCodeException
    {

        public BadRequestException() : base(HttpResponseCode.S_400_BadRequest)
        { }

        public BadRequestException(string message) : base(message, HttpResponseCode.S_400_BadRequest)
        { }
        
        public BadRequestException(string message, Exception innerException) : base(message, HttpResponseCode.S_400_BadRequest, innerException)
        { }

    }
}
