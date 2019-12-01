using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer.Exceptions
{
    public class NotFoundException : HttpErrorCodeException
    {

        public NotFoundException() : base(HttpResponseCode.S_404_NotFound)
        { }

        public NotFoundException(string message) : base(message, HttpResponseCode.S_404_NotFound)
        { }
        
        public NotFoundException(string message, Exception innerException) : base(message, HttpResponseCode.S_404_NotFound, innerException)
        { }

    }
}
