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
    public class WorkerLevelService : IWorkerLevelService
    {
        public int Delete(int id)
        {
            MySqlConnector con = new MySqlConnector();
            if (con.Open())
            {
                return con.ExecuteNonQueryResult($"Delete from poziom_pracownika where id_poziom_pracownika = {id}");
            }
            else
                throw new ConnectMySqlException();
        }

        public IEnumerable<WorkerLevel> FilterByWorkerPermitType(string permitType)
        {
            MySqlConnector con = new MySqlConnector();
            if (con.Open())
            {
                return con.ExecuteQueryResult<WorkerLevel>($"Select * from poziom_pracownika where rodzaj_przepustki = '{permitType}'");
            }
            else
                throw new ConnectMySqlException();
        }

        public IEnumerable<WorkerLevel> FilterByWorkerStatus(string workerStatus)
        {
            MySqlConnector con = new MySqlConnector();
            if (con.Open())
            {
                return con.ExecuteQueryResult<WorkerLevel>($"Select * from poziom_pracownika where status_pracownika = '{workerStatus}'");
            }
            else
                throw new ConnectMySqlException();
        }

        public IEnumerable<WorkerLevel> FilterByWorkerType(string workerType)
        {
            MySqlConnector con = new MySqlConnector();
            if (con.Open())
            {
                return con.ExecuteQueryResult<WorkerLevel>($"Select * from poziom_pracownika where rodzaj_pracownika = '{workerType}'");
            }
            else
                throw new ConnectMySqlException();
        }

        public WorkerLevel Get(int id)
        {
            MySqlConnector con = new MySqlConnector();
            if (con.Open())
            {
                var result = con.ExecuteQueryResult<WorkerLevel>($"Select * from poziom_pracownika where id_poziom_pracownika = {id}");
                return result.Length > 0 ? result[0] : null;
            }
            else
                throw new ConnectMySqlException();
        }

        public IEnumerable<WorkerLevel> GetAll()
        {
            MySqlConnector con = new MySqlConnector();
            if (con.Open())
            {
                return con.ExecuteQueryResult<WorkerLevel>($"Select * from poziom_pracownika");
            }
            else
                throw new ConnectMySqlException();
        }

        public int Post(WorkerLevel workerLevel)
        {
            MySqlConnector con = new MySqlConnector();
            if (con.Open())
            {
                return con.ExecuteNonQueryResult($"insert into poziom_pracownika values(0,'{workerLevel.WorkerType}','{workerLevel.WorkerStatus}','{workerLevel.PermitType}')");
            }
            else
                throw new ConnectMySqlException();
        }

        public int UpdateWorkerStatus(int id, string workerStatus)
        {
            MySqlConnector con = new MySqlConnector();
            if (con.Open())
            {
                return con.ExecuteNonQueryResult($"update poziom_pracownika set status_pracownika = '{workerStatus}' where id_poziom_pracownika = {id}");
            }
            else
                throw new ConnectMySqlException();
        }

        public int UpdateWorkerType(int id, string workerType)
        {
            MySqlConnector con = new MySqlConnector();
            if (con.Open())
            {
                return con.ExecuteNonQueryResult($"update poziom_pracownika set rodzaj_pracownika = '{workerType}' where id_poziom_pracownika = {id}");
            }
            else
                throw new ConnectMySqlException();
        }

        public int UpdateWorkerPermit(int id, string workerPermit)
        {
            MySqlConnector con = new MySqlConnector();
            if (con.Open())
            {
                return con.ExecuteNonQueryResult($"update poziom_pracownika set rodzaj_przepustki = '{workerPermit}' where id_poziom_pracownika = {id}");
            }
            else
                throw new ConnectMySqlException();
        }
    }
}
