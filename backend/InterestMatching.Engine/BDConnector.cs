using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public List<T> GetListFromJsons<T>(string tableName, string columnName, string condition = "")
        {
            return GetAll($"select * from {tableName} {GetCondition(condition)}", x =>
            {
                var dbValue = x.GetString(columnName);
                return JsonConvert.DeserializeObject<T>(dbValue);
            });
        }

        public List<string> GetStringList(string tableName, string columnName, string condition = "")
        {
            return GetAll($"select * from {tableName} {GetCondition(condition)}", x => x.GetString(columnName));
        }

        public List<DateTime> GetDataTimeList(string tableName, string columnName, string condition = "")
        {
            return GetAll($"select * from {tableName} {GetCondition(condition)}", x => x.GetDateTime(columnName));
        }

        public List<int> GetIntList(string tableName, string columnName, string condition = "")
        {
            return GetAll($"select * from {tableName} {GetCondition(condition)}", x => x.GetInt32(columnName));
        }

        public T FindFromJsonByObjectProperty<T>(string tableName, string columnName, Func<List<T>, T> select, string condition = "")
        {
            return select(GetListFromJsons<T>(tableName, columnName, condition));
        }

        public T GetFirstOrDefaultFromJson<T>(string tableName, string columnName, string condition = "")
        {
            return GetListFromJsons<T>(tableName, columnName, condition).FirstOrDefault();
        }

        public string GetFirstString(string tableName, string columnName, string condition = "")
        {
            return GetStringList(tableName, columnName, condition).FirstOrDefault();
        }

        public DateTime GetFirstDataTime(string tableName, string columnName, string condition = "")
        {
            return GetDataTimeList(tableName, columnName, condition).FirstOrDefault();
        }

        public int GetFirstOrDefaultInt(string tableName, string columnName, string condition = "")
        {
            return GetIntList(tableName, columnName, condition).FirstOrDefault();
        }

        private string GetCondition(string condition)
        {
            return string.IsNullOrWhiteSpace(condition) ? "" : $"where {condition}";
        }

        private List<T> GetAll<T>(string command, Func<MySqlDataReader, T> get)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Add(string tableName, params Tuple<string, string>[] columnValuePair)
        {
            if (columnValuePair.Length == 0) return;
            var resultInserts = ResultInserts(columnValuePair);

            Send($"insert into {tableName} {resultInserts}");
        }

        private string ResultInserts(Tuple<string, string>[] columnValuePair)
        {
            var columnNames = new StringBuilder();
            var values = new StringBuilder();
            foreach (var pair in columnValuePair)
            {
                columnNames.Append(pair.Item1);
                columnNames.Append(", ");
                values.Append($"'{pair.Item2}'");
                values.Append(", ");
            }

            columnNames.Remove(columnNames.Length - 2, 2);
            values.Remove(values.Length - 2, 2);

            return $"({columnNames}) values ({values})";
        }

        public void Update(string tableName, string columnName, string predicate, string condition)
        {
            Send($"update {tableName} set {columnName} = {predicate} {GetCondition(condition)}");
        }

        public void Send(string command)
        {
            try
            {
                var myCommand = new MySqlCommand(command, connection);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            connection.Close();
        }

        private MySqlConnection connection;
    }
}
