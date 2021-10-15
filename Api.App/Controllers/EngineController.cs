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
    public class EngineController : ControllerBase
    {
        IEngineService _service;

        public EngineController(IEngineService service)
        {
            _service = service;
        }
        [HttpDelete]
        public ActionResult Delete([FromHeader] int id)
        {
            try
            {
                int result = _service.Delete(id);

                return result > 0 ? 
                    Ok($"Rodzaj silnika id:{id} został usunięty") : 
                    BadRequest($"Rodzaj silnika id:{id} nie istnieje");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByEngineCapacity")]
        public ActionResult<Engine[]> FilterByEngineCapacity([FromHeader]float min,[FromHeader]float max)
        {
            try
            {
                if (min < 0 || max < 0) return BadRequest("Zakres nie może posiadać wartości ujemnych");
                if (min>max) return BadRequest("Wartość minimalna nie może być większa od wartości maksymalnej");

                return Ok(_service.FilterByEngineCapacity(min,max));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByEnginePower")]
        public ActionResult<Engine[]> FilterByEnginePower([FromHeader] int min, [FromHeader] int max)
        {
            try
            {
                if (min < 0 || max < 0) return BadRequest("Zakres nie może posiadać wartości ujemnych");
                if (min > max) return BadRequest("Wartość minimalna nie może być większa od wartości maksymalnej");

                return Ok(_service.FilterByEnginePower(min, max));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByFuelType")]
        public ActionResult<Engine[]> FilterByEnginePower([FromHeader] string fuelType)
        {
            try
            {
                return Ok(_service.FilterByFuelType(fuelType));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetAll")]
        public ActionResult<Engine[]> GetAll()
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
        public ActionResult<Engine> GetId([FromHeader] int id)
        {
            try
            {
                Engine e = _service.GetId(id);
                return e is not null ? Ok(e) : BadRequest($"Brak silnika id:{id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Engine engine)
        {
            try
            {
                if (engine.Power < 0) return BadRequest("Moc silnika nie może być mniejsza od 0");
                if (engine.EngineCapacity < 0) return BadRequest("Pojemność silnika nie może być mniejsza od 0");
                if (string.IsNullOrEmpty(engine.FuelType)) return BadRequest("Pole rodzaj paliwa nie może być puste");
                if (!Helpers.LengthBetween(engine.FuelType, 1, 15)) return BadRequest("Pole rodzaj paliwa powinna zawierać 1-15 znaków");

                return _service.Post(engine)>0 ? Ok("Pomyślnie dodano") : BadRequest("Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateCapacity")]
        public ActionResult UpdateCapacity([FromHeader] int id, [FromHeader] float newCapacity)
        {
            try
            {
                if (newCapacity < 0) return BadRequest("Pojemność silnika nie może być mniejsza od 0");

                return _service.UpdateEngineCapacity(id,newCapacity) > 0 ? Ok("Pomyślnie zaaktualizowano") : BadRequest("Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdatePower")]
        public ActionResult UpdatePower([FromHeader] int id, [FromHeader] int newPower)
        {
            try
            {
                if (newPower < 0) return BadRequest("Moc silnika nie może być mniejsza od 0");

                return _service.UpdateEnginePower(id, newPower) > 0 ? Ok("Pomyślnie zaaktualizowano") : BadRequest("Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("UpdateFuelType")]
        public ActionResult UpdateFuelType([FromHeader] int id, [FromHeader] string newFuelType)
        {
            try
            {
                if (!Helpers.LengthBetween(newFuelType,1,15)) return BadRequest("Długość powinna wynosić 1-15 znaków");

                return _service.UpdateFuelType(id, newFuelType) > 0 ? Ok("Pomyślnie zaaktualizowano") : BadRequest("Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
