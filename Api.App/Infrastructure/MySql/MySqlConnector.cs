using System;
using System.Collections.Generic;
using Api.App.Exceptions;
using Api.Core;
using MySql.Data.MySqlClient;

namespace Api.Infrastructure.MySql
{
    public class MySqlConnector:IDisposable
    {
        const string LOG_FILE = "Mysql.txt";

        MySqlConnectionStringBuilder _connectionString = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            UserID = "root",
            Password = "",
            Database = "seminarium",
            SslMode = MySqlSslMode.None
        };

        MySqlConnection _con;
        public bool Open()
        {
            try
            {
                _con = new MySqlConnection(_connectionString.ConnectionString);
                _con.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Logger.WriteLog(LOG_FILE, "Open Error", e.ToString());
                return false;
            }
        }


        public void Close()
        {
            try
            {
                _con?.Close();
                _con?.Dispose();
            }
            catch (Exception e)
            {
                Logger.WriteLog(LOG_FILE, "Closing error", e.Message);
                Console.WriteLine(e);
            }
        }

        public int ExecuteNonQueryResult(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, _con);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteLog(LOG_FILE, "NonQueryResult Error", e.ToString());
                Console.WriteLine(e);
                Console.WriteLine("======================");
                Console.WriteLine(query);
                return -1;
            }
        }

        public T[] ExecuteQueryResult<T>(string query)where T:class, new()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, _con);
                List<T> result = new List<T>();
                using (MySqlDataReader sqlReader = cmd.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        object[] record = new object[sqlReader.FieldCount];
                        for (int i = 0; i < record.Length; i++)
                        {
                            record[i] = sqlReader[i];
                        }
                        
                        result.Add(GetObjFromMySql<T>(record));
                    }
                }
                return result.ToArray();
            }
            catch (Exception e)
            {
                Logger.WriteLog(LOG_FILE, "ExecuteQueryResult<T> error", e.ToString());
                Console.WriteLine(e);
                Console.WriteLine("======================");
                Console.WriteLine(query);
                return null;
            }
        }

        static T GetObjFromMySql<T>(params object[] values) where T : class, new()
        {
            T result = new T();
            TypeValueSetter(0, result, values);
            return result;
        }

        static int TypeValueSetter(int index, object obj, object[] values)
        {
            Type type = obj.GetType();

            var properties = type.GetProperties();
            for (int i = 0; i < properties.Length; i++, index++)
            {
                if (properties[i].PropertyType.IsPrimitive || properties[i].PropertyType == typeof(string)||properties[i].PropertyType == typeof(DateTime))
                {
                    properties[i].SetValue(obj, values[index]);
                }
                else
                {
                    index = TypeValueSetter(index, properties[i].GetValue(obj), values) - 1;
                }
            }

            return index;
        }

        public void Dispose()
        {
            Close();
        }
    }
}
