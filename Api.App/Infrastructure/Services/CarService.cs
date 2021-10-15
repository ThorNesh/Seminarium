﻿using Api.App.Core;
using Api.App.Exceptions;
using Api.App.Infrastructure.IServices;
using Api.App.Infrastructure.Models.Car;
using Api.Infrastructure.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.Services
{
    public class CarService : ICarService
    {
        public int Delete(int id)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"Delete from pojazd where id = {id}");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Car> FilterByBrand(string brand)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Car>(@$"
SELECT `id_pojazdu`,`numer_vin`,`marka`,`model`,`kolor`,`rocznik`,rodzaj_silnika.id_rodzaj_silnika,rodzaj_silnika.moc,rodzaj_silnika.pojemność,rodzaj_silnika.rodzaj_paliwa 
FROM pojazd join rodzaj_silnika on pojazd.id_rodzaj_silnika=rodzaj_silnika.id_rodzaj_silnika
where marka = '{brand}'
");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Car> FilterByColor(string color)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Car>(@$"
SELECT `id_pojazdu`,`numer_vin`,`marka`,`model`,`kolor`,`rocznik`,rodzaj_silnika.id_rodzaj_silnika,rodzaj_silnika.moc,rodzaj_silnika.pojemność,rodzaj_silnika.rodzaj_paliwa 
FROM pojazd join rodzaj_silnika on pojazd.id_rodzaj_silnika=rodzaj_silnika.id_rodzaj_silnika
where kolor = '{color}'
");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Car> FilterByEngineCapacity(float min, float max)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Car>(@$"
SELECT `id_pojazdu`,`numer_vin`,`marka`,`model`,`kolor`,`rocznik`,rodzaj_silnika.id_rodzaj_silnika,rodzaj_silnika.moc,rodzaj_silnika.pojemność,rodzaj_silnika.rodzaj_paliwa 
FROM pojazd join rodzaj_silnika on pojazd.id_rodzaj_silnika=rodzaj_silnika.id_rodzaj_silnika
where rodzaj_silnika.pojemność >= {Helpers.FloatFormater(min)} && rodzaj_silnika.pojemność <={Helpers.FloatFormater(max)}
");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Car> FilterByEnginePower(int min, int max)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Car>(@$"
SELECT `id_pojazdu`,`numer_vin`,`marka`,`model`,`kolor`,`rocznik`,rodzaj_silnika.id_rodzaj_silnika,rodzaj_silnika.moc,rodzaj_silnika.pojemność,rodzaj_silnika.rodzaj_paliwa 
FROM pojazd join rodzaj_silnika on pojazd.id_rodzaj_silnika=rodzaj_silnika.id_rodzaj_silnika
where rodzaj_silnika.moc >= {min} && rodzaj_silnika.moc <={max}
");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Car> FilterByFuelType(string fuelType)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Car>(@$"
SELECT `id_pojazdu`,`numer_vin`,`marka`,`model`,`kolor`,`rocznik`,rodzaj_silnika.id_rodzaj_silnika,rodzaj_silnika.moc,rodzaj_silnika.pojemność,rodzaj_silnika.rodzaj_paliwa 
FROM pojazd join rodzaj_silnika on pojazd.id_rodzaj_silnika=rodzaj_silnika.id_rodzaj_silnika
where rodzaj_silnika.rodzaj_paliwa = '{fuelType}'
");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Car> FilterByVintage(uint vintageMin, uint vintageMax)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Car>(@$"
SELECT `id_pojazdu`,`numer_vin`,`marka`,`model`,`kolor`,`rocznik`,rodzaj_silnika.id_rodzaj_silnika,rodzaj_silnika.moc,rodzaj_silnika.pojemność,rodzaj_silnika.rodzaj_paliwa 
FROM pojazd join rodzaj_silnika on pojazd.id_rodzaj_silnika=rodzaj_silnika.id_rodzaj_silnika
where rocznik >= {vintageMin} && rocznik <= {vintageMax}
");
                }
                else
                    throw new ConnectMySqlException();
            }
        }


        public Car Get(int id)
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    Car[] result = con.ExecuteQueryResult<Car>(@$"
SELECT `id_pojazdu`,`numer_vin`,`marka`,`model`,`kolor`,`rocznik`,rodzaj_silnika.id_rodzaj_silnika,rodzaj_silnika.moc,rodzaj_silnika.pojemność,rodzaj_silnika.rodzaj_paliwa 
FROM pojazd join rodzaj_silnika on pojazd.id_rodzaj_silnika=rodzaj_silnika.id_rodzaj_silnika
where id_pojazdu = {id}
");
                    return result.Length > 0 ? result[0] : null;
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Car> GetAll()
        {
            using (MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteQueryResult<Car>(@"
                    SELECT `id_pojazdu`,`numer_vin`,`marka`,`model`,`kolor`,`rocznik`,rodzaj_silnika.id_rodzaj_silnika,rodzaj_silnika.moc,rodzaj_silnika.pojemność,rodzaj_silnika.rodzaj_paliwa 
                    FROM pojazd join rodzaj_silnika on pojazd.id_rodzaj_silnika=rodzaj_silnika.id_rodzaj_silnika
                    ");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public int Post(CarDB car)
        {
            using(MySqlConnector con = new MySqlConnector())
            {
                if (con.Open())
                {
                    return con.ExecuteNonQueryResult($"insert into pojazd values(0,'{car.Vin}','{car.Brand}','{car.Model}','{car.Color}',{car.Vintage}, {car.Engine})");
                }
                else
                    throw new ConnectMySqlException();
            }
        }

        public IEnumerable<Car> SearchModel(string model)
        {
            throw new NotImplementedException();
        }

        public Car SearchVin(string vin)
        {
            throw new NotImplementedException();
        }

        public int UpdateBrand(int id, string newBrand)
        {
            throw new NotImplementedException();
        }

        public int UpdateColor(int id, string newColor)
        {
            throw new NotImplementedException();
        }

        public int UpdateEngine(int id, int newEngineId)
        {
            throw new NotImplementedException();
        }

        public int UpdateModel(int id, string newModel)
        {
            throw new NotImplementedException();
        }

        public int UpdateVin(int id, string newVin)
        {
            throw new NotImplementedException();
        }

        public int UpdateVintage(int id, uint newVintage)
        {
            throw new NotImplementedException();
        }
    }
}
