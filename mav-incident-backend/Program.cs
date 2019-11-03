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
            HttpServer.HttpServer server = new HttpServer.HttpServer(80);
            server.Start();
        }
    }
}
