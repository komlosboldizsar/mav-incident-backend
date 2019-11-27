using mav_incident_rest.HttpServer.RequestRouter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer
{
    public class RestServer
    {

        private HttpServer httpServer;
        private Router router;

        public RestServer(int port)
        {
            httpServer = new HttpServer(port);
            router = new Router();
            httpServer.RequestHandler += HttpServer_RequestHandler;
        }

        private void HttpServer_RequestHandler(HttpRequest request, HttpResponse response)
        {
            router.ProcessRequest(request, response);
        }

        public bool Start()
            => httpServer.Start();

        public void Stop()
            => httpServer.Stop();

        public void AddRoute(string path, HttpRequestMethod method, RouteHandlerDelegate handler)
        {
            router.AddRoute(path, method, handler);
        }

    }
}
