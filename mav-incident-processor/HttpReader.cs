using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace mav_incident_processor
{
    class HttpReader
    {
        public static string Read(string url)
        {
            WebClient client = null;
            Stream data = null;
            StreamReader reader = null;
            try
            {
                client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                data = client.OpenRead(url);
                reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                return s;
            }
            finally
            {
                if (client != null)
                    client.Dispose();
                if (data != null)
                    data.Close();
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
