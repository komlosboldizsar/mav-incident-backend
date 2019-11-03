using mav_incident_backend.HttpServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_backend
{
    class Program
    {
        static void Main(string[] args)
        {
            RestServer server = new RestServer(80);
            server.AddRoute("/index.html", HttpRequestMethod.GET, PrintHello);
            server.AddRoute("/{id:i}", HttpRequestMethod.GET, PrintHello);
            server.Start();
        }

        static void PrintHello(HttpRequest request, HttpResponse response, Dictionary<string, string> parameters)
        {
            response.ResponseCode = HttpResponseCode.S_200_Success;
            response.Body = "Hello World!<br>" + parameters["id"];
        }
    }
}
