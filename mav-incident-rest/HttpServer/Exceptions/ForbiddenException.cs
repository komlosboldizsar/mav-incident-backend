using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer.Exceptions
{
    public class ForbiddenException : HttpErrorCodeException
    {

        public ForbiddenException() : base(HttpResponseCode.S_403_Forbidden)
        { }

        public ForbiddenException(string message) : base(message, HttpResponseCode.S_403_Forbidden)
        { }
        
        public ForbiddenException(string message, Exception innerException) : base(message, HttpResponseCode.S_403_Forbidden, innerException)
        { }

    }
}
