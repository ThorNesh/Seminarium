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

namespace WarsztatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        readonly JwtService _jwtService;
        public WorkerController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet("Get")]
        public ActionResult Get([FromHeader] uint id)
        {
            try
            {
                var results = MySqlConnector.ExecuteQueryResult<Worker>($"Select * from workers where Id = {id}");
                return results.Length > 0 ? Ok(results[0]) : BadRequest("Brak pracownika");
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
                return Ok(MySqlConnector.ExecuteQueryResult<Worker>("Select * from workers"));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Register")]
        public ActionResult Register([FromBody] WorkerDto worker)
        {
            try
            {
                worker.Password = BCrypt.Net.BCrypt.HashPassword(worker.Password);

                var result = MySqlConnector.ExecuteNonQueryResult
                    ($@"insert into workers values(
                    0,
                    '{worker.Name}',
                    '{worker.LastName}',
                    '{worker.PhoneNumber},
                    '{worker.Email}',
                    '{worker.Password}',
                    '{true}')"
                    );

                return result > 0 ? Created("Succes", "Pracownik dodany") : BadRequest("Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("Login")]
        public ActionResult Login([FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                Worker worker = MySqlConnector.ExecuteQueryResult<Worker>($"select * from workers where Email = '{email}'")[0];
                if (!worker.Hired) return Unauthorized("Nie jesteś zatrudniony/a!!!");

                if (!BCrypt.Net.BCrypt.Verify(password, worker.Password)) return BadRequest("Błędne dane logowania");

                var claims = new Claim[] {
                    new Claim("Name",worker.Name),
                    new Claim("LastName", worker.LastName),
                    new Claim("Phone_Number",worker.PhoneNumber),
                    new Claim("Email",worker.Email)
                };
                var jwt = _jwtService.Generate(worker.Id,claims);
                Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

                return Ok("Pomyślnie zalogowano");

            }
            catch (NullReferenceException)
            {
                return BadRequest("Błędne dane logowania");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("User")]
        public ActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                if (token is null) return Unauthorized();
                uint userId = uint.Parse(token.Issuer);

                Worker worker = MySqlConnector.ExecuteQueryResult<Worker>($"Select * from workers where Id = {userId}")[0];
                return Ok(worker);
            }
            catch (MySqlException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return Unauthorized();
            }

        }
        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok("Wylogowano");
        }
    }
}
