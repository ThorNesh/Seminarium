using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WarsztatAPI.Models;
using WarsztatAPI.Tools;

namespace WarsztatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpDelete]
        public ActionResult Delete([FromHeader] uint id)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized("Nie posiadasz uprawnień");

                return MySqlConnector.ExecuteNonQueryResult($"Delete from users where Id = {id}") > 0 ? Ok("Pomyślnie usunięto") : BadRequest("Brak użytkownika");

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

        
        

        [HttpPost("Register")]
        public ActionResult Register([FromBody] UserDB user)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized("Nie posiadasz uprawnień");

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                var result = MySqlConnector.ExecuteNonQueryResult($@"
                insert into users values(
                0,
                '{user.Login}'
                '{user.Password}',
                {user.Worker_Id},
                {(user.IsSuperUser ? 1 : 0)}
                )
                ");

                return result > 0 ? Created("Succes", "Pomyślnie utworzono użytkownika") : BadRequest("Coś poszło nie tak");
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
        [HttpPost("Login")]
        public ActionResult Login([FromHeader] string login, [FromHeader] string password)
        {
            try
            {
                Models.User[] user = MySqlConnector.ExecuteQueryResult<User>($"select users.Id,users.Login,users.Password,workers.*,users.Is_Super_User from users join workers on users.Worker_Id=workers.Id where Login = '{login}'");


                if (user.Length <= 0) return BadRequest("Błędne dane logowania");
                if (!user[0].Worker_Id.Hired) return Unauthorized("Nie jesteś zatrudniony");
                if (!BCrypt.Net.BCrypt.Verify(password, user[0].Password)) return BadRequest("Błędne dane logowania");



                Claim[] claims = new Claim[] { new Claim("IsSuperUser", user[0].IsSuperUser.ToString()), new Claim("Hired", user[0].Worker_Id.Hired.ToString()) };
                var token = JwtService.Generate(user[0].Id, claims);
                return Ok(new
                {
                    Mess = "Pomyślnie zalogowano",
                    IsSuperUser = user[0].IsSuperUser,
                    Resutl = token
                });
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

        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            if (!string.IsNullOrWhiteSpace(Request.Cookies["jwt"]))
            {
                Response.Cookies.Delete("jwt");
                return Ok("Wylogowano");
            }
            else
                return Unauthorized();
        }
        [HttpPost("Refresh")]
        public ActionResult Refresh()
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);

                Claim[] claims = new Claim[token.Claims.Count()];
                for (int i = 0; i < token.Claims.Count(); i++)
                {
                    claims[i] = token.Claims.ElementAt(i);
                }
                Response.Cookies.Delete("jwt");
                Response.Cookies.Append(
                    "jwt",
                    JwtService.Generate(uint.Parse(token.Issuer), claims),
                    new CookieOptions { HttpOnly = true });
                return Created("Succes", "Refreshed");

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
