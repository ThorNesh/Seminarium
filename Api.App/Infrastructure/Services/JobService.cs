using Api.App.Core;
using Api.App.Exceptions;
using Api.App.Infrastructure.IServices;
using Api.App.Infrastructure.Models.Job;
using Api.Infrastructure.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.Services
{
    public class JobService : IJobService
    {
        public int Delete(uint id)
        {
            using(MySqlConnector con = new())
            {
                if (con.Open())
                { return con.ExecuteNonQueryResult($"Delete from zlecenie where id_zlecenia = {id}"); }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Job> FilterByCarId(uint id)
        {
             return FilteredRecords("zlecenie.id_pojazdu",id);
        }

        public IEnumerable<Job> FilterByClientId(uint id)
        {
            return FilteredRecords("zlecenie.id_klienta", id);
        }

        public IEnumerable<Job> FilterByDate(DateTime start, DateTime end)
        {
            return FilteredRecords("czas_wystawienia", Helpers.ConvertDate(start), Helpers.ConvertDate(end));
        }

        public IEnumerable<Job> FilterByStatus(string status)
        {
            return FilteredRecords("status_zlecenia",status);
        }

        public IEnumerable<Job> FilterByWorkerId(uint id)
        {
            return FilteredRecords("zlecenie.id_pracownika", id);
        }

        public Job Get(uint id)
        {
            var results = (Job[])FilteredRecords("id_zlecenia", id);
            return results.Length > 0 ? results[0] : null; 
        }

        IEnumerable<Job> FilteredRecords(string column, object param)
        {
            using(MySqlConnector con =new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Job>($@"
SELECT 
`id_zlecenia`,`nazwa_zlecenia`,`opis_zlecenia`,`status_zlecenia`,`czas_wystawienia`,
	zlecenie.id_pracownika, pracownicy.imie, pracownicy.nazwisko, pesel, pracownicy.numer_telefonu, pracownicy.adres_email,
    	pracownicy.id_poziom_pracownika, rodzaj_pracownika, status_pracownika,rodzaj_przepustki,
    zlecenie.id_klienta, klient.imie, klient.nazwisko,klient.numer_telefonu,klient.adres_email,
    zlecenie.id_pojazdu,numer_vin,marka,model,kolor,rocznik,
    	pojazd.id_rodzaj_silnika, moc,pojemność,rodzaj_paliwa
FROM `zlecenie` 
JOIN klient,pracownicy,poziom_pracownika,pojazd,rodzaj_silnika
{(!(string.IsNullOrEmpty(column)||string.IsNullOrWhiteSpace(column)) ?  $"where {column} ='{param}'" : "")}
GROUP BY id_zlecenia;
");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        IEnumerable<Job> FilteredRecords(string column, object param1,object param2)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Job>($@"
SELECT 
`id_zlecenia`,`nazwa_zlecenia`,`opis_zlecenia`,`status_zlecenia`,`czas_wystawienia`,
	zlecenie.id_pracownika, pracownicy.imie, pracownicy.nazwisko, pesel, pracownicy.numer_telefonu, pracownicy.adres_email,
    	pracownicy.id_poziom_pracownika, rodzaj_pracownika, status_pracownika,rodzaj_przepustki,
    zlecenie.id_klienta, klient.imie, klient.nazwisko,klient.numer_telefonu,klient.adres_email,
    zlecenie.id_pojazdu,numer_vin,marka,model,kolor,rocznik,
    	pojazd.id_rodzaj_silnika, moc,pojemność,rodzaj_paliwa
FROM `zlecenie` 
JOIN klient,pracownicy,poziom_pracownika,pojazd,rodzaj_silnika
{(!(string.IsNullOrEmpty(column) || string.IsNullOrWhiteSpace(column)) ? $"where {column} >'{param1}' and {column}<'{param2}'" : "")}
GROUP BY id_zlecenia;
");
                }
                else
                    throw new ConnectMySqlException();
            }
        }
        public IEnumerable<Job> GetAll()
        {
            return FilteredRecords("", null);
        }

        public int Post(JobDB add)
        {
            using(MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult(@$"
INSERT INTO `zlecenie`(`nazwa_zlecenia`, `opis_zlecenia`, `status_zlecenia`, `czas_wystawienia`, `id_pracownika`, `id_klienta`, `id_pojazdu`) 
VALUES ('{add.Name}','{add.Describe}','{add.Status}','{add.DateOfIssue.Year}-{add.DateOfIssue.Month}-{add.DateOfIssue.Day}','{add.WorkerId}','{add.ClientId}','{add.CarId}')");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Job> SearchByName(string name)
        {
            return FilteredRecords("nazwa_zlecenia",name);
        }


        public int UpdateCarId(uint id, uint carId)
        {
            return UpdateRow(id, "id_pojazdu", carId);
        }

        public int UpdateClientId(uint id, uint clientId)
        {
            return UpdateRow(id, "id_klienta", clientId);
        }

        public int UpdateJobDescribe(uint id, string describe)
        {
            return UpdateRow(id, "opis_zlecenia", describe);
        }

        public int UpdateJobName(uint id, string name)
        {
            return UpdateRow(id, "nazwa_zlecenia", name);
        }


        public int UpdateJobStatus(uint id, string status)
        {
            return UpdateRow(id, "status_zlecenia", status);
        }

        public int UpdateWorker(uint id, uint workerId)
        {
            return UpdateRow(id, "id_pracownika", workerId);
        }

        int UpdateRow(uint id, string column, object param)
        {
            using (MySqlConnector con = new())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"Update zlecenie set {column} = '{param}' where id_zlecenia = {id}");
                }
                else
                    throw new ConnectMySqlException();
            }
        }
    }
}
