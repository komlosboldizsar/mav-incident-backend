using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_backend.HttpServer
{
    public class HttpHeaderReadOnlyCollection : HttpHeaderCollection
    {

        public HttpHeaderReadOnlyCollection(Dictionary<string, string> headers)
        {
            this.headers = headers;
        }

        protected override void setHeader(string key, string value)
        {
            throw new Exception("Header collection is read-only!");
        }

    }
}
