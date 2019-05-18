using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

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

        public List<T> Get<T>(string command)
        {
            var myCommand = new MySqlCommand(command, connection);
            var reader = myCommand.ExecuteReader();
            var result = new List<T>();
            while (reader.Read())
            {
                
                var dbValue = reader.GetString(1); 
                result.Add(JsonConvert.DeserializeObject<T>(dbValue));

            }
            return result;
        }

        public void Dispose()
        {
            connection.Close();
        }

        private MySqlConnection connection;
    }
}
