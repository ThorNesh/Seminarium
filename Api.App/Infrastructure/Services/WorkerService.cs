using Api.App.Exceptions;
using Api.App.Infrastructure.IServices;
using Api.App.Infrastructure.Models.Worker;
using Api.Infrastructure.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.Services
{
    public class WorkerService : IWorkerService
    {
        public int Delete(int id)
        {
           using(MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"Delete from pracownicy where id_pracownika={id}");
                }
                else throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Worker> FilterByEmail(string email)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Worker>(@$"
select `id_pracownika`,`imie`,`nazwisko`,`pesel`,`numer_telefonu`,`adres_email`,pracownicy.id_poziom_pracownika, 
poziom_pracownika.rodzaj_pracownika,poziom_pracownika.status_pracownika,poziom_pracownika.rodzaj_przepustki 
from pracownicy 
join poziom_pracownika on pracownicy.id_poziom_pracownika=poziom_pracownika.id_poziom_pracownika
where adres_email = '{email}'");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Worker> FilterByLastName(string lastName)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Worker>(@$"
select `id_pracownika`,`imie`,`nazwisko`,`pesel`,`numer_telefonu`,`adres_email`,pracownicy.id_poziom_pracownika, 
poziom_pracownika.rodzaj_pracownika,poziom_pracownika.status_pracownika,poziom_pracownika.rodzaj_przepustki 
from pracownicy 
join poziom_pracownika on pracownicy.id_poziom_pracownika=poziom_pracownika.id_poziom_pracownika
where nazwisko = '{lastName}'");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Worker> FilterByName(string name)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Worker>(@$"
select `id_pracownika`,`imie`,`nazwisko`,`pesel`,`numer_telefonu`,`adres_email`,pracownicy.id_poziom_pracownika, 
poziom_pracownika.rodzaj_pracownika,poziom_pracownika.status_pracownika,poziom_pracownika.rodzaj_przepustki 
from pracownicy 
join poziom_pracownika on pracownicy.id_poziom_pracownika=poziom_pracownika.id_poziom_pracownika
where imie = '{name}'");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Worker> FilterByPermitType(string permitType)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Worker>(@$"
select `id_pracownika`,`imie`,`nazwisko`,`pesel`,`numer_telefonu`,`adres_email`,pracownicy.id_poziom_pracownika, 
poziom_pracownika.rodzaj_pracownika,poziom_pracownika.status_pracownika,poziom_pracownika.rodzaj_przepustki 
from pracownicy 
join poziom_pracownika on pracownicy.id_poziom_pracownika=poziom_pracownika.id_poziom_pracownika
where poziom_pracownika.rodzaj_przepustki = '{permitType}'");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Worker> FilterByPesel(string pesel)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Worker>(@$"
select `id_pracownika`,`imie`,`nazwisko`,`pesel`,`numer_telefonu`,`adres_email`,pracownicy.id_poziom_pracownika, 
poziom_pracownika.rodzaj_pracownika,poziom_pracownika.status_pracownika,poziom_pracownika.rodzaj_przepustki 
from pracownicy 
join poziom_pracownika on pracownicy.id_poziom_pracownika=poziom_pracownika.id_poziom_pracownika
where pesel = '{pesel}'");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Worker> FilterByPhoneNumber(string phoneNumber)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Worker>(@$"
select `id_pracownika`,`imie`,`nazwisko`,`pesel`,`numer_telefonu`,`adres_email`,pracownicy.id_poziom_pracownika, 
poziom_pracownika.rodzaj_pracownika,poziom_pracownika.status_pracownika,poziom_pracownika.rodzaj_przepustki 
from pracownicy 
join poziom_pracownika on pracownicy.id_poziom_pracownika=poziom_pracownika.id_poziom_pracownika
where numer_telefonu = '{phoneNumber}'");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Worker> FilterByWorkerStatus(string status)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Worker>(@$"
select `id_pracownika`,`imie`,`nazwisko`,`pesel`,`numer_telefonu`,`adres_email`,pracownicy.id_poziom_pracownika, 
poziom_pracownika.rodzaj_pracownika,poziom_pracownika.status_pracownika,poziom_pracownika.rodzaj_przepustki 
from pracownicy 
join poziom_pracownika on pracownicy.id_poziom_pracownika=poziom_pracownika.id_poziom_pracownika
where status_pracownika = '{status}'");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Worker> FilterByWorkerType(string workerType)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Worker>(@$"
select `id_pracownika`,`imie`,`nazwisko`,`pesel`,`numer_telefonu`,`adres_email`,pracownicy.id_poziom_pracownika, 
poziom_pracownika.rodzaj_pracownika,poziom_pracownika.status_pracownika,poziom_pracownika.rodzaj_przepustki 
from pracownicy 
join poziom_pracownika on pracownicy.id_poziom_pracownika=poziom_pracownika.id_poziom_pracownika
where rodzaj_pracownika = '{workerType}'");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public Worker Get(int id)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                   var result = con.ExecuteQueryResult<Worker>(@$"
select `id_pracownika`,`imie`,`nazwisko`,`pesel`,`numer_telefonu`,`adres_email`,pracownicy.id_poziom_pracownika, 
poziom_pracownika.rodzaj_pracownika,poziom_pracownika.status_pracownika,poziom_pracownika.rodzaj_przepustki 
from pracownicy 
join poziom_pracownika on pracownicy.id_poziom_pracownika=poziom_pracownika.id_poziom_pracownika
where id_pracownika = {id}");
                    return result.Length > 0 ? result[0] : null;
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Worker> GetAll()
        {
            using(MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Worker>(@"
select `id_pracownika`,`imie`,`nazwisko`,`pesel`,`numer_telefonu`,`adres_email`,pracownicy.id_poziom_pracownika, 
poziom_pracownika.rodzaj_pracownika,poziom_pracownika.status_pracownika,poziom_pracownika.rodzaj_przepustki 
from pracownicy join poziom_pracownika on pracownicy.id_poziom_pracownika=poziom_pracownika.id_poziom_pracownika;");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int Post(WorkerDB worker)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($@"
insert into pracownicy values(0,'{worker.Name}','{worker.LastName}','{worker.Pesel}','{worker.PhoneNumber}','{worker.Email}',{worker.WorkerId});
                    ");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdateEmail(int id, string email)
        {
            using(MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($@"
Update pracownicy set adres_email = '{email}' where id_pracownika = {id}
                    ");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdateLastName(int id, string newLastName)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($@"
Update pracownicy set nazwisko = '{newLastName}' where id_pracownika = {id}
                    ");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdateName(int id, string newName)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($@"
Update pracownicy set imie = '{newName}' where id_pracownika = {id}
                    ");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdatePesel(int id, string newPesel)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($@"
Update pracownicy set pesel = '{newPesel}' where id_pracownika = {id}
                    ");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdatePhoneNumber(int id, string newPhoneNumber)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($@"
Update pracownicy set numer_telefonu = '{newPhoneNumber}' where id_pracownika = {id}
                    ");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int UpdateWorkerLevel(int id, int newWorkerLevel)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($@"
Update pracownicy set id_poziom_pracownika = {newWorkerLevel} where id_pracownika = {id}
                    ");
                }
                else
                    throw new ConnectMySqlException();
            }
        }
    }
}
