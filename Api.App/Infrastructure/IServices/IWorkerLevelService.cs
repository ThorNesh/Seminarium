using Api.App.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.IServices
{
    public interface IWorkerLevelService
    {
        int Delete(int id);
        int Post(WorkerLevel workerLevel);

        IEnumerable<WorkerLevel> GetAll();
        WorkerLevel Get(int id);
        IEnumerable<WorkerLevel> FilterByWorkerType(string workerType);
        IEnumerable<WorkerLevel> FilterByWorkerStatus(string workerStatus);
        IEnumerable<WorkerLevel> FilterByWorkerPermitType(string permitType);

        int UpdateWorkerType(int id, string workerType);
        int UpdateWorkerStatus(int id, string workerStatus);
        int UpdateWorkerPermit(int id, string workerPermit);

    }
}
