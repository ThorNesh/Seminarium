using Api.App.Core;
using Api.App.Infrastructure.IServices;
using Api.App.Infrastructure.Models.Worker;
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
        [HttpGet("Get")]
        public ActionResult Get([FromHeader] int id)
        {
            try
            {
                var result = _service.Get(id);
                return result is not null ? Ok(result) : BadRequest($"Pracownik id:{id} nie istnieje");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("FilterByLastName")]
        public ActionResult FilterByLastName([FromHeader] string lastName)
        {
            try
            {
                return Ok(_service.FilterByLastName(lastName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByEmail")]
        public ActionResult FilterByEmail([FromHeader]string email)
        {
            try
            {
                return Ok(_service.FilterByEmail(email));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByName")]
        public ActionResult FilterByName([FromHeader]string name)
        {
            try
            {
                return Ok(_service.FilterByName(name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByPermitType")]
        public ActionResult FilterByPermitType([FromHeader]string permitType)
        {
            try
            {
                return Ok(_service.FilterByPermitType(permitType));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByPesel")]
        public ActionResult FilterByPesel([FromHeader]string pesel)
        {
            try
            {
                return Ok(_service.FilterByPesel(pesel));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByPhoneNumber")]
        public ActionResult FilterByPhoneNumber([FromHeader] string phoneNumber)
        {
            try
            {
                return Ok(_service.FilterByPhoneNumber(phoneNumber));
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
                return Ok(_service.FilterByWorkerStatus(status));
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


        [HttpDelete]
        public ActionResult Delete([FromHeader] int id)
        {
            try
            {
                return _service.Delete(id) > 0 ? Ok($"Pomyślnie usunięto") : BadRequest($"Pracownik id:{id} nie istnieje");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] WorkerDB worker)
        {
            try
            {
                if (!Helpers.LengthBetween(worker.Name, 1, 30)) return BadRequest("Imię powinno zawierać 1-30 znaków");
                if (!Helpers.LengthBetween(worker.LastName, 1, 30)) return BadRequest("Nazwisko powinno zawierać 1-30 znaków");
                if (!Helpers.PeselValidate(worker.Pesel)) return BadRequest("Nieprawidłowy format peselu");
                if (!Helpers.EmailValide(worker.Email)) return BadRequest("Nieprawidłowy format adresu email");
                if (!Helpers.PhoneNumber(worker.PhoneNumber)) return BadRequest("Nieprawidłowy format numeru kontaktowego");

                return _service.Post(worker) > 0 ? Ok($"Pomyślnie dodano") : BadRequest($"Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateEmail")]
        public ActionResult UpdateEmail([FromHeader] int id, [FromHeader]string email)
        {
            try
            {
                if (!Helpers.EmailValide(email)) return BadRequest("Nieprawidłowy format adresu email");

                return _service.UpdateEmail(id, email) > 0 ? Ok($"Pomyślnie edytowano") : BadRequest($"Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateLastName")]
        public ActionResult UpdateLastName([FromHeader] int id, [FromHeader] string lastName)
        {
            try
            {
                if (!Helpers.LengthBetween(lastName, 1, 30)) return BadRequest("Nazwisko powinno zawierać 1-30 znaków");

                return _service.UpdateLastName(id, lastName) > 0 ? Ok($"Pomyślnie edytowano") : BadRequest($"Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateName")]
        public ActionResult UpdateName([FromHeader] int id, [FromHeader] string name)
        {
            try
            {
                if (!Helpers.LengthBetween(name, 1, 30)) return BadRequest("Imie powinno zawierać 1-30 znaków");

                return _service.UpdateName(id, name) > 0 ? Ok($"Pomyślnie edytowano") : BadRequest($"Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdatePesel")]
        public ActionResult UpdatePesel([FromHeader] int id, [FromHeader] string pesel)
        {
            try
            {
                if (!Helpers.PeselValidate(pesel)) return BadRequest("Nieprawidłowy format peselu");

                return _service.UpdatePesel(id, pesel) > 0 ? Ok($"Pomyślnie edytowano") : BadRequest($"Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdatePhoneNumber")]
        public ActionResult UpdatePhoneNumber([FromHeader] int id, [FromHeader] string phoneNumber)
        {
            try
            {
                if (!Helpers.PhoneNumber(phoneNumber)) return BadRequest("Nieprawidłowy format numeru kontaktowego");

                return _service.UpdatePhoneNumber(id, phoneNumber) > 0 ? Ok($"Pomyślnie edytowano") : BadRequest($"Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
