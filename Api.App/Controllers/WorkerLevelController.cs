using Api.App.Core;
using Api.App.Infrastructure.IServices;
using Api.App.Infrastructure.Models;
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
    public class WorkerLevelController : ControllerBase
    {
        IWorkerLevelService _service;

        public WorkerLevelController(IWorkerLevelService service)
        {
            _service = service;
        }

        [HttpDelete]
        public ActionResult Delete([FromHeader] int id)
        {
            try
            {
                return _service.Delete(id) > 0 ? Ok($"Pomyślnie usunięto") : BadRequest($"Poziom pracownika z id:{id} nie istnieje");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("FilterByWorkerPermitType")]
        public ActionResult FilterByWorkerPermitType([FromHeader] string permitType)
        {
            try
            {
                return Ok(_service.FilterByWorkerPermitType(permitType));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("FilterByWorkerStatus")]
        public ActionResult FilterByWorkerStatus([FromHeader] string workerStatus)
        {
            try
            {
                return Ok(_service.FilterByWorkerStatus(workerStatus));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("FilterByWorkerType")]
        public ActionResult FilterByWorkerType([FromHeader] string workerType)
        {
            try
            {
                return Ok(_service.FilterByWorkerType(workerType));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("Get")]
        public ActionResult Get([FromHeader] int id)
        {
            try
            {
                var result = _service.Get(id);
                return result is not null ? Ok(result) : BadRequest($"Brak poziomu pracownika id:{id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] WorkerLevel workerLevel)
        {
            try
            {
                if (!Helpers.LengthBetween(workerLevel.WorkerType, 1, 30)) return BadRequest("Pole rodzaj pracownika powinno zawierać 1-30 znaków");
                if (!Helpers.LengthBetween(workerLevel.WorkerStatus, 1, 30)) return BadRequest("Pole status pracownika powinno zawierać 1-30 znaków");
                if (!Helpers.LengthBetween(workerLevel.PermitType, 1, 30)) return BadRequest("Pole rodzaj przepustki powinno zawierać 1-30 znaków");

                return _service.Post(workerLevel) > 0 ? Ok("Pomyślnie dodano") : BadRequest("Coś poszło nie tak");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateWorkerStatus")]
        public ActionResult UpdateWorkerStatus([FromHeader]int id,[FromHeader] string workerStatus)
        {
            try
            {
                if (!Helpers.LengthBetween(workerStatus, 1, 30)) return BadRequest("Pole status pracownika powinno zawierać 1-30 znaków");

                return _service.UpdateWorkerStatus(id,workerStatus) > 0 ? Ok("Pomyślnie zedytowano") : BadRequest($"Brak poziomu pracownika id:{id}");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("UpdateWorkerType")]
        public ActionResult UpdateWorkerType([FromHeader]int id,[FromHeader] string workerType)
        {
            try
            {
                if (!Helpers.LengthBetween(workerType, 1, 30)) return BadRequest("Pole rodzaj pracownika powinno zawierać 1-30 znaków");

                return _service.UpdateWorkerType(id,workerType) > 0 ? Ok("Pomyślnie zedytowano") : BadRequest($"Brak poziomu pracownika id:{id}");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateWorkerPermitType")]
        public ActionResult UpdateWorkerPermitType([FromHeader]int id,[FromHeader] string workerPermitType)
        {
            try
            {
                if (!Helpers.LengthBetween(workerPermitType, 1, 30)) return BadRequest("Pole rodzaj przepustki powinno zawierać 1-30 znaków");

                return _service.UpdateWorkerPermit(id,workerPermitType) > 0 ? Ok("Pomyślnie zedytowano") : BadRequest($"Brak poziomu pracownika id:{id}");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
