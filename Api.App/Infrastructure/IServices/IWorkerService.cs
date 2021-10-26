using Api.App.Infrastructure.Models.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.IServices
{
    public interface IWorkerService
    {
        IEnumerable<Worker> GetAll();
        Worker Get(int id);

        IEnumerable<Worker> FilterByName(string name);
        IEnumerable<Worker> FilterByLastName(string lastName);
        IEnumerable<Worker> FilterByPesel(string pesel);
        IEnumerable<Worker> FilterByPhoneNumber(string phoneNumber);
        IEnumerable<Worker> FilterByEmail(string email);
        IEnumerable<Worker> FilterByWorkerType(string workerType);
        IEnumerable<Worker> FilterByWorkerStatus(string status);
        IEnumerable<Worker> FilterByPermitType(string permitType);

        int Delete(int id);
        int Post(WorkerDB worker);
        int UpdateName(int id, string newName);
        int UpdateLastName(int id, string newLastName);
        int UpdatePesel(int id, string newPesel);
        int UpdatePhoneNumber(int id, string newPhoneNumber);
        int UpdateEmail(int id, string email);
        int UpdateWorkerLevel(int id, int newWorkerLevel);
    }
}
