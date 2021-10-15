using Api.App.Core;
using Api.App.Infrastructure.IServices;
using Api.App.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService; 
        }

        [HttpGet("FilterBy/Name")]
        public ActionResult SearchName([FromHeader] string name)
        {
            try
            {
                var result = _clientService.FilterByName(name);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterBy/Lastname")]
        public ActionResult SearchLastname([FromHeader] string lastname)
        {
            try
            {
                var result = _clientService.FilterByLastname(lastname);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterBy/ContactNumber")]
        public ActionResult SearchContactNumber([FromHeader] string contactNumber)
        {
            try
            {
                var result = _clientService.FilterByContactNumber(contactNumber);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterBy/ContacEmail")]
        public ActionResult SearchContacEmail([FromHeader] string contactEmail)
        {
            try
            {
                var result = _clientService.FilterByContactMail(contactEmail);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAll")]
        public ActionResult<Client[]> GetAll()
        {
            try
            {
                var result = _clientService.GetAll();
                
                return Ok(result);
                
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Get")]
        public ActionResult<Client> Get([FromHeader] int id)
        {
            try
            {
                var result = _clientService.Get(id);

                return result;
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("Delete")]
        public ActionResult<string> Delete([FromHeader] int id)
        {
            try
            {
                var result = _clientService.Delete(id);

                return $"Usunięto klienta id : {id}";
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Client client)
        {
            if (!client.Valide(out string message))
                return BadRequest(message);
            try
            {
                return _clientService.Post(client) > 0 ? Ok("Pomyślnie dodano klienta") : BadRequest("Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("update/name")]
        public ActionResult UpdateName([FromHeader]int id, [FromHeader] string name)
        {
            if (!Helpers.LengthBetween(name, 1, 30)) return BadRequest("Długość powinna wynosić 1-30 znaków");

            try
            {
                int result = _clientService.UpdateName(id, name);

                if (result > 0) return Ok($"Klient id:{id} zauktualizowany");
                else return BadRequest($"Klient id:{id} nie istnieje");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("update/lastname")]
        public ActionResult UpdateLastname([FromHeader] int id, [FromHeader] string lastname)
        {
            if (!Helpers.LengthBetween(lastname, 1, 30)) return BadRequest("Długość powinna wynosić 1-30 znaków");
            try
            {
                int result = _clientService.UpdateLastname(id, lastname);

                if (result > 0) return Ok($"Klient id:{id} zaktualizowany");
                else return BadRequest($"Klient id:{id} nie istnieje");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update/contactMail")]
        public ActionResult UpdateContactMail([FromHeader] int id, [FromHeader] string ContactMail)
        {
            if (!Helpers.EmailValide(ContactMail)) return BadRequest("Nieprawidłowa forma adresu email");
            try
            {
                int result = _clientService.UpdateContactMail(id, ContactMail);

                if (result > 0) return Ok($"Klient id:{id} zaktualizowany");
                else return BadRequest($"Klient id:{id} nie istnieje");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update/contactNumber")]
        public ActionResult UpdateContactNumber([FromHeader] int id, [FromHeader] string ContactNumber)
        {
            if (!Helpers.PhoneNumber(ContactNumber)) return BadRequest("Nieprawidłowa forma numeru telefonu");
            try
            {
                int result = _clientService.UpdateContactNumber(id, ContactNumber);

                if (result > 0) return Ok($"Klient id:{id} zaktualizowany");
                else return BadRequest($"Klient id:{id} nie istnieje");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
