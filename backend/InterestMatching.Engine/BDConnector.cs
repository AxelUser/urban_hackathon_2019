using System;
using System.Text;
using MySql.Data.MySqlClient;

namespace InterestMatching.Engine
{
    public class BDConnector : IDisposable
    {
        public BDConnector()
        {
            var Connect = "Database=temp_db;Data Source=209.250.236.34;User Id=temp_user;Password=1234";
            connection = new MySqlConnection(Connect);
            connection.Open();
        }

        public string Get(string command)
        {
            var myCommand = new MySqlCommand(command, connection);
            var reader = myCommand.ExecuteReader();
            var sbuilder = new StringBuilder();
            while (reader.Read())
            {
                var r = reader.GetInt32(0);
                var id = reader.GetInt32(1); 

                sbuilder.Append($"{r} / {id}\n");
            }
            return sbuilder.ToString();
        }

        public void Dispose()
        {
            connection.Close();
        }

        private MySqlConnection connection;
    }
}
