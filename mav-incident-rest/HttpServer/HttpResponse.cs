using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer
{
    public class HttpResponse
    {

        public string Version { get; set; } = "HTTP/1.1";

        public HttpResponseCode ResponseCode { get; set; }

        public string Body { get; set; }

        public HttpHeaderCollection Headers { get; private set; }

        public HttpResponse()
        {
            this.ResponseCode = HttpResponseCode.S_500_InternalError;
            this.Headers = new HttpHeaderCollection();
            this.Body = "";
        }

        public string GetResponseString()
        {

            StringBuilder responseString = new StringBuilder();
            
            responseString.Append(Version);
            responseString.Append(" ");
            responseString.Append((int)ResponseCode);
            responseString.Append(" ");
            responseString.Append(ResponseCode.ToReasonPhrase());
            responseString.AppendLine();

            foreach (var header in Headers)
            {
                responseString.Append(header.Key);
                responseString.Append(": ");
                responseString.Append(header.Value);
                responseString.AppendLine();
            }

            responseString.AppendLine();

            responseString.Append(Body);
            responseString.AppendLine();
            responseString.AppendLine();

            return responseString.ToString();

        }

    }
}
