using mav_incident_backend.HttpServer;
using mav_incident_backend.IncidentRest;
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
            RestService.RestService rest = new IncidentRestService(80);
            rest.Start();
        }
    }
}
