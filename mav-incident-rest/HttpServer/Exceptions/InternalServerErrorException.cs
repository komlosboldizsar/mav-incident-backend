using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer.Exceptions
{
    public class InternalServerErrorException : HttpErrorCodeException
    {

        public InternalServerErrorException() : base(HttpResponseCode.S_500_InternalError)
        { }

        public InternalServerErrorException(string message) : base(message, HttpResponseCode.S_500_InternalError)
        { }
        
        public InternalServerErrorException(string message, Exception innerException) : base(message, HttpResponseCode.S_500_InternalError, innerException)
        { }

    }
}
