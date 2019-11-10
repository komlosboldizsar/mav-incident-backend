using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_dba
{
    public class IncidentDatabase
    {

        public static IncidentDatabase Instance { get; } = new IncidentDatabase();

        public IncidentContext Context { get; private set; }

        private MySqlConnection conn;

        public void Init()
        {
            conn = new MySqlConnection("Server=localhost;Database=mav-incident;Uid=mav-incident;Pwd=mav-incident;");
            conn.Open();
            Context = new IncidentContext(conn, false);
        }

        public void DeInit()
        {
            conn.Close();
        }

    }
}
