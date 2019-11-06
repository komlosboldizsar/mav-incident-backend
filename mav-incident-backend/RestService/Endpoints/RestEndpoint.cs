using mav_incident_rest.HttpServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace mav_incident_rest.RestService.Endpoints
{
    public abstract class RestEndpoint
    {

        public abstract string Path { get; }

        public abstract HttpRequestMethod Method { get; }

        public virtual void Call(HttpRequest request, HttpResponse response, Dictionary<string, string> urlParams)
        {
            RestResponse restresp = process(request, urlParams);
            response.Body = convertBodyToJson(restresp.Body);
            response.ResponseCode = restresp.ResponseCode;
            foreach (var header in restresp.Headers)
                response.Headers[header.Key] = header.Value;
        }

        protected abstract RestResponse process(HttpRequest request, Dictionary<string, string> urlParams);

        protected string convertBodyToJson(object responseObject)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonSerializer serializer = new JsonSerializer();
            try
            {
                using (JsonWriter jsw = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jsw, responseObject);
                    return sb.ToString();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
