using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.HttpServer.RequestRouter
{
    class Router
    {

        private List<Route> routes = new List<Route>();

        public Router()
        { }

        public void AddRoute(Route route)
        {
            routes.Add(route);
        }

        public void AddRoute(string path, HttpRequestMethod method, RouteHandlerDelegate handler)
        {
            routes.Add(new Route(path, method, handler));
        }

        public bool ProcessRequest(HttpRequest request, HttpResponse response)
        {
            foreach (Route route in routes) { 
                if (route.Matches(request)) { 
                    route.Process(request, response);
                    return true;
                }
            }
            response = new HttpResponseNotFound(request);
            return false;
        }

    }
}
