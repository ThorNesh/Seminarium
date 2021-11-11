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
    public class StatusController : ControllerBase
    {
        [HttpGet("Get")]
        public ActionResult Get([FromHeader] uint id)
        {
            try
            {
                Status[] results = MySqlConnector.ExecuteQueryResult<Status>($"Select * from statuses where Id = {id}");
                return results.Length>0 ?  Ok(results[0]) : BadRequest("Brak statusu");
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
                return Ok(MySqlConnector.ExecuteQueryResult<Status>($"Select * from statuses"));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Status status)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString().ToLower()) is null) return Unauthorized("Brak uprawnień");

                return MySqlConnector.ExecuteNonQueryResult($"insert into statuses values(0,'{status.Name}')") > 0 ?
                    Ok("Pomyślnie dodano") : BadRequest("Coś poszło nie tak");
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
        [HttpPut]
        public ActionResult Update([FromHeader] uint id,[FromHeader] string name)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString().ToLower()) is null) return Unauthorized("Brak uprawnień");

                return MySqlConnector.ExecuteNonQueryResult($"Update statuses set Name='{name}' where Id = {id}") > 0 ?
                    Ok("Pomyślnie dodano") : BadRequest("Coś poszło nie tak");
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
