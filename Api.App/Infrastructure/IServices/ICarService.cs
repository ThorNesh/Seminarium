using Api.App.Infrastructure.Models.Car;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.IServices
{
    public interface ICarService
    {
        IEnumerable<Car> GetAll();
        Car Get(int id);
        Car SearchVin(string vin);
        IEnumerable<Car> FilterByBrand(string brand);
        IEnumerable<Car> SearchModel(string model);
        IEnumerable<Car> FilterByColor(string color);
        IEnumerable<Car> FilterByVintage(uint vintageMin, uint vintageMax);

        //engine filter
        IEnumerable<Car> FilterByEngineCapacity(float min, float max);
        IEnumerable<Car> FilterByEnginePower(int min, int max);
        IEnumerable<Car> FilterByFuelType(string fuelType);

        int Post(CarDB car);
        int Delete(int id);

        int UpdateVin(int id, string newVin);
        int UpdateBrand(int id, string newBrand);
        int UpdateModel(int id, string newModel);
        int UpdateColor(int id, string newColor);
        int UpdateVintage(int id, uint newVintage);
        int UpdateEngine(int id, int newEngineId);


    }
}
