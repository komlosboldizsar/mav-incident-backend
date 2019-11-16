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
            RssFeedProcessor feedProcessor = new RssFeedProcessor("https://www.mavcsoport.hu/mavinform/rss.xml");
            feedProcessor.Do();
            Console.ReadKey();
            IncidentDatabase.Instance.DeInit();
        }
    }
}
