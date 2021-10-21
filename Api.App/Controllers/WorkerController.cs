using Api.App.Infrastructure.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        IWorkerService _service;
        public WorkerController(IWorkerService service)
        {
            _service = service;
        }

    }
}
