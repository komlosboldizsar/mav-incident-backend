using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private const string CONFIG_PATH = "db-config.xml";

        ConfigReader configReader;

        public IncidentDatabase()
        {
            try
            {
                configReader = new ConfigReader(CONFIG_PATH);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Couldn't read database configuration. Reason: " + ex.Message);
            }
        }

        public bool Init()
        {
            if (configReader == null)
                return true;
            string connStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};",
                configReader.Host,
                configReader.Database,
                configReader.Username,
                configReader.Password);
            conn = new MySqlConnection(connStr);
            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
                return false;
            Context = new IncidentContext(conn, false);
            return true;
        }

        public void DeInit()
        {
            conn.Close();
        }
        
    }
}
