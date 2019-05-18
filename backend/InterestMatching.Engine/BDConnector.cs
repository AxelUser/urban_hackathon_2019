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

        public List<T> Get<T>(string tableName)
        {
            return Get($"select * from {tableName}", x =>
            {
                var dbValue = x.GetString(tableName);
                return JsonConvert.DeserializeObject<T>(dbValue);
            });
        }

        public List<string> GetString(string tableName)
        {
            return Get($"select * from {tableName}", x => x.GetString(tableName));
        }

        public List<DateTime> GetDataTime(string tableName)
        {
            return Get($"select * from {tableName}", x => x.GetDateTime(tableName));
        }

        public List<int> GetInt(string tableName)
        {
            return Get($"select * from {tableName}", x => x.GetInt32(tableName));
        }

        private List<T> Get<T>(string command, Func<MySqlDataReader, T> get)
        {
            var myCommand = new MySqlCommand(command, connection);
            var reader = myCommand.ExecuteReader();
            var result = new List<T>();
            while (reader.Read())
            {
                
                var dbValue = get(reader); 
                result.Add(dbValue);

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
