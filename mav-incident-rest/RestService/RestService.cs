using mav_incident_rest.HttpServer;
using mav_incident_rest.RestService.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest.RestService
{
    abstract public class RestService
    {

        private RestServer restServer;

        public RestService(int port)
        {
            restServer = new RestServer(port);
            addEndpoints();
        }

        public virtual bool Start()
            => restServer.Start();

        public virtual void Stop()
            => restServer.Stop();

        protected virtual void addEndpoints()
        {
            foreach (var endpoint in getEndpoints())
                restServer.AddRoute(endpoint.Path, endpoint.Method, endpoint.Call);
        }

        protected abstract List<RestEndpoint> getEndpoints();


    }
}
