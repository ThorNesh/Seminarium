using Api.App.Core;
using Api.App.Exceptions;
using Api.App.Infrastructure.IServices;
using Api.App.Infrastructure.Models;
using Api.Infrastructure.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.Services
{
    public class EngineService : IEngineService
    {
        public int Delete(int id)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"Delete from rodzaj_silnika where id_rodzaj_silnika = {id}");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Engine> FilterByEngineCapacity(float min, float max)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Engine>($"Select * from rodzaj_silnika where pojemność >= {Helpers.FloatFormater(min)} and pojemność <={Helpers.FloatFormater(max)}");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Engine> FilterByEnginePower(int min, int max)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Engine>($"Select * from rodzaj_silnika where moc >= {min} and moc <={max}");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Engine> FilterByFuelType(string fuelType)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Engine>($"Select * from rodzaj_silnika where rodzaj_paliwa = '{fuelType}'");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Engine> GetAll()
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Engine>($"Select * from rodzaj_silnika");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public Engine GetId(int id)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    Engine[] result = con.ExecuteQueryResult<Engine>($"Select * from rodzaj_silnika where id_rodzaj_silnika = {id}");
                    return result.Length > 0 ? result[0] : null;
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int Post(Engine engine)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult(@$"INSERT INTO `rodzaj_silnika`
                    (`moc`, `pojemność`, `rodzaj_paliwa`) 
                    VALUES ({engine.Power},{engine.EngineCapacity.ToString().Replace(',','.')},'{engine.FuelType}')");
                }
                else
                {
                    throw new ConnectMySqlException();
                }
            }
        }

        public int UpdateEngineCapacity(int id, float newValue)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"update rodzaj_silnika set pojemność={Helpers.FloatFormater(newValue)} where id_rodzaj_silnika = {id}");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdateEnginePower(int id, int newValue)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"update rodzaj_silnika set moc={newValue} where id_rodzaj_silnika = {id}");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdateFuelType(int id, string newValue)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"update rodzaj_silnika set rodzaj_paliwa='{newValue}' where id_rodzaj_silnika = {id}");
                }
                else
                    throw new ConnectMySqlException();
            }
        }
    }
}
