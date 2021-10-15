using Api.App.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.IServices
{
    public interface IEngineService
    {
        IEnumerable<Engine> GetAll();
        IEnumerable<Engine> FilterByEnginePower(int min, int max);
        IEnumerable<Engine> FilterByEngineCapacity(float min, float max);
        IEnumerable<Engine> FilterByFuelType(string fuelType);
        Engine GetId(int id);

        int Delete(int id);

        int Post(Engine engine);

        int UpdateEnginePower(int id, int newValue);
        int UpdateEngineCapacity(int id, float newValue);
        int UpdateFuelType(int id, string newValue);

        
    }
}
