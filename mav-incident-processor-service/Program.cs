using mav_incident_dba;
using mav_incident_processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_processor_service
{
    class Program
    {
        static void Main(string[] args)
        {
            IncidentDatabase.Instance.Init();
            SingleIncidentProcessor proc = new SingleIncidentProcessor(64881);
            proc.Do();
            Console.ReadKey();
            IncidentDatabase.Instance.DeInit();
        }
    }
}
