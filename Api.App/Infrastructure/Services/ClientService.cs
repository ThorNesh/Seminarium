using Api.App.Infrastructure.IServices;
using Api.Infrastructure.MySql;
using Api.App.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.App.Exceptions;
using Api.App.Core;

namespace Api.App.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        public int Delete(int id)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    int count = con.ExecuteNonQueryResult($"DELETE FROM `klient` WHERE id_klienta = {id}");

                    if (count < 1) throw new Exception($"Brak użytkownika id : {id}");
                    return count;
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Client> FilterByContactMail(string searchedMail)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Client>($"select * from `klient` where `adres_email` = '{searchedMail}' ");
                    
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Client> FilterByContactNumber(string searchedNumber)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Client>($"select * from `klient` where `numer_telefonu` = '{searchedNumber}' ");
                    
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Client> FilterByLastname(string searchedLastname)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Client>($"select * from `klient` where `nazwisko` = '{searchedLastname}' ");
 
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Client> FilterByName(string searchedName)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Client>($"select * from `klient` where `imie` = '{searchedName}' ");
                    
                }
                else
                    throw new ConnectMySqlException();
            }

        }

        public Client Get(int id)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    Client[] result = con.ExecuteQueryResult<Client>($"Select * from klient where id_klienta = {id}");

                    if (result.Length < 1) throw new Exception($"Nie znaleziono klienta z id:{id}");
                    else
                        return result[0];
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Client> GetAll()
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Client>("Select * from klient");
                    
                }
                else
                    throw new ConnectMySqlException();
            }

        }

        public int Post(Client var)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"insert into klient values(0,'{var.Imie}','{var.Nazwisko}','{var.Numer_telefonu}','{var.Adres_email}')");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdateContactMail(int id, string contactMail)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"update `klient` set `adres_email` = '{contactMail}' where id_klienta = {id} ");
                  
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdateContactNumber(int id, string contactNumber)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"update `klient` set `numer_telefonu` = '{contactNumber}' where id_klienta = {id} ");
                    
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdateLastname(int id, string lastname)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"update `klient` set `nazwisko` = '{lastname}' where id_klienta = {id} ");
                   
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdateName(int id, string name)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"update `klient` set `imie` = '{name}' where id_klienta = {id} ");
                }
                else
                    throw new ConnectMySqlException();
            }
        }
    }
}
