using Api.App.Infrastructure.Models.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.IServices
{
    public interface IJobService
    {
        Job Get(uint id);
        IEnumerable<Job> GetAll();
        IEnumerable<Job> SearchByName(string name);
        IEnumerable<Job> FilterByStatus(string status);
        IEnumerable<Job> FilterByDate(DateTime start, DateTime end);
        IEnumerable<Job> FilterByWorkerId(uint id);
        IEnumerable<Job> FilterByClientId(uint id);
        IEnumerable<Job> FilterByCarId(uint id);

        int Post(JobDB add);
        int Delete(uint id);

        int UpdateJobName(uint id, string name);
        int UpdateJobDescribe(uint id, string describe);
        int UpdateJobStatus(uint id, string status);
        int UpdateWorker(uint id, uint workerId);
        int UpdateClientId(uint id, uint clientId);
        int UpdateCarId(uint id, uint carId);
    }
}
