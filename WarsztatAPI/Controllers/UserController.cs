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


        [HttpGet("GetAll")]
        public ActionResult GetAll([FromHeader] string authorization)
        {
            return ExecuteApi(authorization, token =>
                 {
                     if (token is null) return Unauthorized("Nie jesteś zalogowany");
                     if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized("Nie posiadasz uprawnień");

                     return Ok(MySqlConnector.ExecuteQueryResult<User>("select users.Id,users.Login,users.Password,workers.*,users.Is_Super_User from users join workers on users.Worker_Id=workers.Id"));
                 });
        }

        [HttpDelete]
        public ActionResult Delete([FromHeader] string authorization, [FromHeader] uint id)
        {
            return ExecuteApi(authorization, token =>
                 {
                     if (token is null) return Unauthorized("Nie jesteś zalogowany");
                     if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized("Nie posiadasz uprawnień");

                     return MySqlConnector.ExecuteNonQueryResult($"Delete from users where Id = {id}") > 0 ? Ok("Pomyślnie usunięto") : BadRequest("Brak użytkownika");

                 });
        }



        public class UserDTO
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public uint Worker_Id { get; set; } 
        }
        [HttpPost("Register")]
        public ActionResult Register([FromHeader] string authorization, [FromBody] UserDTO user)
        {
            return ExecuteApi(authorization, token =>
                {

                    if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized("Nie posiadasz uprawnień");

                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                    var result = MySqlConnector.ExecuteNonQueryResult($@"
                insert into users values(
                0,
                '{user.Login}',
                '{user.Password}',
                {user.Worker_Id},
                0,
                0
                )
                ");

                    return result > 0 ? Created("Succes", "Pomyślnie utworzono użytkownika") : BadRequest("Coś poszło nie tak");
                });
        }

        [HttpPost("Login")]
        public ActionResult Login([FromHeader] string login, [FromHeader] string password)
        {
            return ExecuteApi(null, token =>
                {

                    Models.User[] user = MySqlConnector.ExecuteQueryResult<User>($"select users.Id,users.Login,users.Password,workers.*,users.Is_Super_User from users join workers on users.Worker_Id=workers.Id where Login = '{login}'");

                    if (user.Length <= 0) return BadRequest("Błędne dane logowania");
                    if (!user[0].Worker_Id.Hired) return Unauthorized("Nie jesteś zatrudniony");
                    if (string.IsNullOrEmpty(password)) return BadRequest("Błędne dane logowania");
                    if (!BCrypt.Net.BCrypt.Verify(password, user[0].Password)) return BadRequest("Błędne dane logowania");



                    Claim[] claims = new Claim[] {
                        new Claim("IsSuperUser", user[0].IsSuperUser.ToString()),
                        new Claim("Hired", user[0].Worker_Id.Hired.ToString()),
                        new Claim("WorkerId",user[0].Worker_Id.Id.ToString())};
                    var tokenString = JwtService.Generate(user[0].Id, claims);

                    return Ok(new
                    {
                        Mess = "Pomyślnie zalogowano",
                        IsSuperUser = user[0].IsSuperUser,
                        Id=user[0].Id,
                        WorkerId=user[0].Worker_Id,
                        Resutl = tokenString
                    });
                });
        }

        [HttpPut("EditLogin")]
        public ActionResult EditLogin([FromHeader] string authorization, [FromBody] string login)
        {
            return ExecuteApi(authorization, token =>
             {
                 UserDB[] user = MySqlConnector.ExecuteQueryResult<UserDB>($"select * from users where Id = {token.Issuer}");

                 if (user.Length < 1) return Unauthorized("Nie jesteś zalogowany");

                
                 return MySqlConnector.ExecuteNonQueryResult($"Update users set login='{login}' where Id={token.Issuer}") > 0 ? Ok("Pomyślnie edytowano") : BadRequest("Coś poszło nie tak");
             });
        }

        [HttpPut("EditPassword")]
        public ActionResult EditPassword([FromHeader] string authorization, [FromHeader] string password)
        {
            return ExecuteApi(authorization, token =>
            {
                UserDB[] user = MySqlConnector.ExecuteQueryResult<UserDB>($"select * from users where Id = {token.Issuer}");

                if (user.Length < 1) return Unauthorized("Nie jesteś zalogowany");


                return MySqlConnector.ExecuteNonQueryResult($"Update users set password='{BCrypt.Net.BCrypt.HashPassword(password)}' where Id={token.Issuer}") > 0 ? Ok("Pomyślnie edytowano") : BadRequest("Coś poszło nie tak");
            });
        }

        public class UserSUDTO
        {
            public uint Id { get; set; }
            public bool IsSuperUser { get; set; }
        }

        [HttpPut("EditSuperUser")]
        public ActionResult EditSuperUser([FromHeader] string authorization, [FromBody] UserSUDTO user)
        {
            return ExecuteApi(authorization,token =>
            {
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized("Nie posiadasz uprawnień");

                return MySqlConnector.ExecuteNonQueryResult($"update users set Is_Super_User={(user.IsSuperUser?1:0)} where Id={user.Id}")>0 ? Ok("Pomyślnie edytowano") : BadRequest("Coś poszło nie tak");
            });
        }


        [HttpPost("Refresh")]
        public ActionResult Refresh([FromHeader] string authorization)
        {
            return ExecuteApi(authorization, token => Created("Succes", JwtService.Generate(uint.Parse(token.Issuer), (Claim[])token.Claims)));
        }



        ActionResult ExecuteApi(string authorization, Func<JwtSecurityToken, ActionResult> func)
        {
            try
            {
                if (string.IsNullOrEmpty(authorization))
                {
                    return func(null);
                }
                Console.WriteLine(1);
                var token = JwtService.Verify(authorization);

                return func(token);

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
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
