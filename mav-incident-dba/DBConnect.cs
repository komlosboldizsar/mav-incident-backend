using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_dba
{
    // @source https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
    public class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            initialize();
        }

        //Initialize values
        private void initialize()
        {
            server = "localhost";
            database = "mav-incident";
            uid = "mav-incident";
            password = "mav-incident";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert()
        {
            throw new NotImplementedException();
        }

        //Update statement
        public void Update()
        {
            throw new NotImplementedException();
        }

        //Delete statement
        public void Delete()
        {
            throw new NotImplementedException();
        }

        //Select statement
        public List<string>[] Select()
        {
            throw new NotImplementedException();
        }

        //Count statement
        public int Count()
        {
            throw new NotImplementedException();
        }

    }
}
