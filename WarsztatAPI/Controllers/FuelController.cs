using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarsztatAPI.Models;
using WarsztatAPI.Tools;

namespace WarsztatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelController : ControllerBase
    {
        [HttpGet("Get")]
        public ActionResult Get([FromHeader] uint id)
        {
            try
            {
                FuelType[] result = MySqlConnector.ExecuteQueryResult<FuelType>($"Select * from fuel_types where Id = {id}");
                return result.Length > 0 ? Ok(result[0]) : BadRequest("Brak typu paliwa");
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
                return Ok(MySqlConnector.ExecuteQueryResult<FuelType>("select * from fuel_types"));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("AddFuel")]
        public ActionResult AddFuel([FromHeader] string authorization, [FromBody] FuelType fuel)
        {
            try
            {
                var token = JwtService.Verify(authorization);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized("Nie posiadasz uprawnień");

                return MySqlConnector.ExecuteNonQueryResult($"insert into fuel_types(Name) values('{fuel}')") > 0 ? Ok("Pomyślnie dodano") : BadRequest("Coś poszło nie tak");
            }
            catch (ArgumentNullException)
            {
                return Unauthorized("Nie jesteś zalogowany");
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized("Przekroczono czas sesji");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("Update")]
        public ActionResult AddFuel([FromHeader] string authorization, [FromHeader] uint id, [FromBody]string fuel)
        {
            try
            {
                var token = JwtService.Verify(authorization);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized("Nie posiadasz uprawnień");

                return MySqlConnector.ExecuteNonQueryResult($"update fuel_types set Name='{fuel}' where Id = {id}") > 0 ? Ok("Pomyślnie edytowano") : BadRequest("Coś poszło nie tak");
            }
            catch (ArgumentNullException)
            {
                return Unauthorized("Nie jesteś zalogowany");
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized("Przekroczono czas sesji");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
