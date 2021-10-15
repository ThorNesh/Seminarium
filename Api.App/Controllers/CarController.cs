using Api.App.Infrastructure.IServices;
using Api.App.Infrastructure.Models.Car;
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
    public class CarController : ControllerBase
    {
        ICarService _service;

        public CarController(ICarService service)
        {
            _service = service;
        }

        [HttpDelete]
        public ActionResult Delete([FromHeader]int id)
        {
            try
            {
                return _service.Delete(id) > 0 ? Ok($"Pomyślnie usunięto pojazd id:{id}") : BadRequest($"Pojazd id:{id} nie istnieje");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByBrand")]
        public ActionResult FilterBrand([FromHeader] string brand)
        {
            try
            {
                return Ok(_service.FilterByBrand(brand));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByColor")]
        public ActionResult FilterColor([FromHeader] string color)
        {
            try
            {
                return Ok(_service.FilterByColor(color));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByEngineCapacity")]
        public ActionResult FilterEngineCapacity([FromHeader] float min,[FromHeader] float max)
        {
            try
            {
                return Ok(_service.FilterByEngineCapacity(min,max));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByEnginePower")]
        public ActionResult FilterEnginePower([FromHeader] int min, [FromHeader] int max)
        {
            try
            {
                if (min < 0) return BadRequest("Wartość minimalna nie może być mniejsza od 0");
                if (max < 0) return BadRequest("Wartość maksymalna nie może być mniejsza od 0");
                if (min > max) return BadRequest("Wartość minimalna nie może być większa od wartości maksymalnej");
                return Ok(_service.FilterByEnginePower(min, max));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("FilterByEngineFuelType")]
        public ActionResult FilterEngineFuelType([FromHeader] string fuelType)
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
        [HttpGet("FilterByVintage")]
        public ActionResult FilterEngineVintage([FromHeader] int vintageMin, [FromHeader] int vintageMax)
        {
            try
            {
                if (vintageMin < 0) return BadRequest("Wartość minimalna nie może być mniejsza od 0");
                if (vintageMax < 0) return BadRequest("Wartość maksymalna nie może być mniejsza od 0");
                if (vintageMin > vintageMax) return BadRequest("Wartość minimalna nie może być większa od wartości maksymalnej");
                return Ok(_service.FilterByVintage((uint)vintageMin,(uint)vintageMax));
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
                Car car = _service.Get(id);
                return car is not null ? Ok(car) : BadRequest($"Brak pojazdu id:{id}");
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
        public ActionResult Post([FromBody] CarDB car)
        {
            try
            {
                //ify vin 17znaków
                return _service.Post(car)>0 ? Ok("Pomyślnie dodano") : BadRequest("Coś poszło nietak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
