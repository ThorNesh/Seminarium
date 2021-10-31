using Api.App.Core;
using Api.App.Infrastructure.IServices;
using Api.App.Infrastructure.Models.Job;
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
    public class JobController : ControllerBase
    {
        IJobService _service;
        public JobController(IJobService service)
        {
            _service = service;
        }
        [HttpDelete]
        public ActionResult Delete([FromHeader] uint id)
        {
            try
            {
                return _service.Delete(id)>0 ? Ok($"Pomyślnie usunięto") : BadRequest($"Zlecenie id:{id} nie istnieje");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByStatus")]
        public ActionResult FilterByStatus([FromHeader] string status)
        {
            try
            {
                return Ok(_service.FilterByStatus(status));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByCarId")]
        public ActionResult FilterByCarId([FromHeader] uint id)
        {
            try
            {
                return Ok(_service.FilterByCarId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByClientId")]
        public ActionResult FilterByClientId([FromHeader] uint id)
        {
            try
            {
                return Ok(_service.FilterByClientId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByDate")]
        public ActionResult FilterByDate([FromHeader] DateTime start, [FromHeader] DateTime end)
        {
            try
            {
                return Ok(_service.FilterByDate(start, end));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByWorkerId")]
        public ActionResult FilterByWorkerId([FromHeader] uint id)
        {
            try
            {
                return Ok(_service.FilterByWorkerId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByName")]
        public ActionResult SearchByName([FromHeader] string name)
        {
            try
            {
                return Ok(_service.SearchByName(name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Get")]
        public ActionResult Get([FromHeader] uint id)
        {
            try
            {
                Job result = _service.Get(id);
                return result is not null ? Ok(result) : BadRequest($"Brak zecenia id:{id}");
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
        public ActionResult Post([FromBody] JobDB add)
        {
            try
            {
                if (!Helpers.LengthBetween(add.Name, 1, 30)) return BadRequest("Nazwa zlecenia musi zawierać 1-30 znaków");
                if (!Helpers.LengthBetween(add.Describe, 1, 250)) return BadRequest("Opis musi zawierać 1-250 znaków");
                if (!Helpers.LengthBetween(add.Status, 1, 30)) return BadRequest("Status musi zawierać 1-30 znaków");
                
                add.DateOfIssue = DateTime.Now;
                return _service.Post(add)>0 ? Ok("Pomyślnie dodano") : BadRequest("Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateCarId")]
        public ActionResult UpdateCarId([FromHeader]uint id,[FromHeader] uint carId)
        {
            try
            {
                return _service.UpdateCarId(id, carId) > 0 ? Ok("Pomyślnie edytowano") : BadRequest($"Brak zlecenia id:{id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateClientId")]
        public ActionResult UpdateClientId([FromHeader]uint id,[FromHeader] uint clientId)
        {
            try
            {
                return _service.UpdateClientId(id, clientId) > 0 ? Ok("Pomyślnie edytowano") : BadRequest($"Brak zlecenia id:{id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateDescribe")]
        public ActionResult UpdateDescribe([FromHeader] uint id, [FromHeader] string describe)
        {
            try
            {
                if (!Helpers.LengthBetween(describe, 1, 250)) return BadRequest("Opis musi zawierać 1-250 znaków");
                return _service.UpdateJobDescribe(id, describe) > 0 ? Ok("Pomyślnie edytowano") : BadRequest($"Brak zlecenia id:{id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateName")]
        public ActionResult UpdateName([FromHeader] uint id, [FromHeader] string name)
        {
            try
            {
                if (!Helpers.LengthBetween(name, 1, 30)) return BadRequest("Nazwa zlecenia musi zawierać 1-30 znaków");
                return _service.UpdateJobName(id, name) > 0 ? Ok("Pomyślnie edytowano") : BadRequest($"Brak zlecenia id:{id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateStatus")]
        public ActionResult UpdateStatus([FromHeader] uint id, [FromHeader] string status)
        {
            try
            {
                if (!Helpers.LengthBetween(status, 1, 30)) return BadRequest("Status musi zawierać 1-30 znaków");
                return _service.UpdateJobStatus(id,status)>0 ? Ok("Pomyślnie edytowano") : BadRequest($"Brak zlecenia id:{id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateWorkerId")]
        public ActionResult UpdateWorkerId([FromHeader] uint id, [FromHeader] uint workerId)
        {
            try
            {
                return _service.UpdateWorker(id, workerId) > 0 ? Ok("Pomyślnie edytowano") : BadRequest($"Brak zlecenia id:{id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
