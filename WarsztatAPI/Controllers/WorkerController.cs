using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WarsztatAPI.Models;
using WarsztatAPI.Tools;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WarsztatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        [HttpGet("Get")]
        public ActionResult Get([FromHeader] string authorization, [FromHeader] uint id)
        {
            try
            {
                var token = JwtService.Verify(authorization);
                if (uint.Parse(token.Issuer) != id)
                    if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized();

                var results = MySqlConnector.ExecuteQueryResult<Worker>($"Select * from workers where Id = {id}");
                return results.Length > 0 ? Ok(results[0]) : BadRequest("Brak pracownika");
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
        [HttpGet("GetAll")]
        public ActionResult GetAll([FromHeader] string authorization)
        {
            try
            {
                var token = JwtService.Verify(authorization);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized();
                return Ok(MySqlConnector.ExecuteQueryResult<Worker>("Select * from workers"));
            }
            catch (ArgumentNullException)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("FilterByName")]
        public ActionResult FilterByName([FromHeader] string authorization, [FromHeader] string name)
        {
            return Filter(authorization, "Name",name);
        }
        [HttpGet("FilterByLastname")]
        public ActionResult FilterByLastname([FromHeader] string authorization, [FromHeader] string lastname)
        {
            return Filter(authorization, "LastName", lastname);
        }

        private ActionResult Filter(string authorization, string col,object var)
        {
            try
            {
                var token = JwtService.Verify(authorization);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized();

                return Ok(MySqlConnector.ExecuteQueryResult<Worker>($@"
                    select * from workers where {col}='{var}';
                "));

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

        [HttpPost]
        public ActionResult Post([FromHeader] string authorization, [FromBody] Worker worker)
        {
            try
            {
                var token = JwtService.Verify(authorization);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized();

                return MySqlConnector.ExecuteNonQueryResult(@$"
                    insert into workers values(
                    0,
                    '{worker.Name}',
                    '{worker.LastName}',
                    '{worker.PhoneNumber}',
                    '{worker.Email}',
                    true
                    )
                    ") > 0 ?
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

        [HttpPut("UpdateName")]
        public ActionResult UpdateName([FromHeader] string authorization, [FromHeader] uint id, [FromHeader] string name)
        {
            return Update(authorization, id, "Name", name);
        }
        [HttpPut("UpdateLastname")]
        public ActionResult UpdateLastname([FromHeader] string authorization, [FromHeader] uint id, [FromHeader] string lastname)
        {
            return Update(authorization, id, "LastName", lastname);
        }
        [HttpPut("UpdatePhoneNumber")]
        public ActionResult UpdatePhoneNumber([FromHeader] string authorization, [FromHeader] uint id, [FromHeader] string phoneNumber)
        {
            return Update(authorization, id, "Phone_Number", phoneNumber);
        }
        [HttpPut("UpdateEmail")]
        public ActionResult UpdateEmail([FromHeader] string authorization, [FromHeader] uint id, [FromHeader] string email)
        {
            return Update(authorization, id, "Email", email);
        }
        [HttpPut("UpdateHired")]
        public ActionResult UpdateHired([FromHeader] string authorization, [FromHeader] uint id, [FromHeader] string hired)
        {
            return Update(authorization, id, "Hired", hired);
        }

        private ActionResult Update(string authorization,uint id, string col, string var)
        {
            try
            {
                var token = JwtService.Verify(authorization);
                if (uint.Parse(token.Issuer) != id)
                    if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized();
                return MySqlConnector.ExecuteNonQueryResult(@$"
start transaction;
{(var=="0" ? $"delete from users where Worker_Id={id};":"")}
Update workers set {col}='{var}' where Id = {id};
commit"
) > 0 ?
Ok("Pomyślnie edytowano") : BadRequest("Coś poszło nie tak");
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
