using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarsztatAPI.Models;
using WarsztatAPI.Tools;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarsztatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet("Get")]
        public ActionResult Get([FromHeader] uint id)
        {
            try
            {
                JwtService.Verify(Request.Cookies["jwt"]);
                Client[] results = MySqlConnector.ExecuteQueryResult<Client>($"select * from clients where Id = {id}");
                return results.Length > 0 ? Ok(results[0]) : BadRequest("Brak klienta w bazie");
            }
            catch (ArgumentNullException) { return Unauthorized(); }
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
                JwtService.Verify(Request.Cookies["jwt"]);
                return Ok(MySqlConnector.ExecuteQueryResult<Client>("select * from clients"));
            }

            catch (ArgumentNullException) { return Unauthorized(); }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Client client)
        {
            try
            {

                return MySqlConnector.ExecuteNonQueryResult($@"
                insert into clients values(0,'{client.Name}','{client.LastName}','{client.PhoneNumber}','{client.Email}')
                ")>0 ? 
                Ok("Pomyślnie dodano klienta") : BadRequest("Coś poszło nie tak") ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public ActionResult Delete([FromHeader] uint id)
        {
            try
            {
                return MySqlConnector.ExecuteNonQueryResult($"delete from clients where Id = {id}")>0 ? 
                    Ok("Pomyślnie usunięto clienta") : BadRequest("Brak klienta w bazie danych");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("Update/Name")]
        public ActionResult UpdateName([FromHeader] uint id,[FromHeader] string name)
        {
            return Update(id,"Name",name);
        }
        [HttpPut("Update/LastName")]
        public ActionResult UpdateLastName([FromHeader]uint id,[FromHeader] string lastName)
        {
            return Update(id, "LastName", lastName);
        }
        [HttpPut("UpdatePhoneNumber")]
        public ActionResult UpdatePhoneNumber([FromHeader] uint id, [FromHeader] string phoneNumber)
        {
            return Update(id, "Phone_Number", phoneNumber);
        }
        [HttpPut("UpdateEmail")]
        public ActionResult UpdateEmail([FromHeader] uint id, [FromHeader] string email)
        {
            return Update(id, "Email", email);
        }
        ActionResult Update(uint id, string col,object value)
        {
            try
            {
                return MySqlConnector.ExecuteNonQueryResult($"update clients set {col}='{value}' where Id = {id}")>0 ? 
                    Ok("Pomyślnie edytowano") : BadRequest("Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
