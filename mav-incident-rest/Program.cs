using mav_incident_rest.HttpServer;
using mav_incident_rest.IncidentRest;
using mav_incident_dba;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_rest
{
    class Program
    {
        static void Main(string[] args)
        {
            RestService.RestService rest = new IncidentRestService(80);
            if(!rest.Start())
            {
                Console.WriteLine("Couldn't start REST service. See debug output for details.");
                Console.ReadKey();
            }
        }
    }
}
