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
    public class ChainsController : ControllerBase
    {
        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            return ExecuteGet(()=>Ok(Filter("0",0)));
        }
        [HttpGet("Get")]
        public ActionResult Get([FromHeader] uint id)
        {
            return ExecuteGet(()=> {
                Chains[] results = Filter("clients_vehicles_chains.Id", id);
                return results.Length > 0 ? Ok(results[0]) : BadRequest("Brak powiązania");
            });
        }

        ActionResult ExecuteGet(Func<ActionResult> func)
        {
            try
            {
                JwtService.Verify(Request.Cookies["jwt"]);
                return func();
            }
            catch (ArgumentNullException)
            {
                return Unauthorized("Nie jesteś zalogowany");
            }
            catch(SecurityTokenExpiredException)
            {
                return Unauthorized("Przekroczono czas sesji");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        Chains[] Filter(string col, object var)
        {
            return MySqlConnector.ExecuteQueryResult<Chains>($@"
            SELECT clients_vehicles_chains.Id,clients.*,vehicles.*,fuel_types.Name, Message,Service FROM `clients_vehicles_chains` 
join clients on clients.Id = Client_Id
join vehicles on vehicles.Id=Vehicle_Id
JOIN fuel_types on vehicles.Fuel_Id=fuel_types.Id
where {col}='{var}';
            ");
        }

        [HttpDelete]
        public ActionResult Delete([FromHeader] uint id)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized("Nie posiadasz uprawnień");

                return MySqlConnector.ExecuteNonQueryResult($"Delete from clients_vehicles_chains where Id={id}") > 0 ? Ok("Pomyślnie usunięto") : BadRequest("Brak powiązania");

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

        [HttpPut("Update/Client")]
        public ActionResult UpdateClient([FromHeader] uint id,[FromHeader]uint clientId)
        {
            return ExecuteUpdate(id, "Client_Id", clientId);
        }
        [HttpPut("Update/Vehicle")]
        public ActionResult UpdateVehicle([FromHeader] uint id, [FromHeader] uint vehicleId)
        {
            return ExecuteUpdate(id,"Vehicle_Id",vehicleId);
        }
        [HttpPut("Update/Message")]
        public ActionResult UpdateMessage([FromHeader] uint id, [FromHeader] string message)
        {
            return ExecuteUpdate(id, "Message", message);
        }
        [HttpPut("Update/Service")]
        public ActionResult UpdateService([FromHeader] uint id, [FromHeader] string service)
        {
            return ExecuteUpdate(id, "Message", service);
        }

        private ActionResult ExecuteUpdate(uint id, string col, object var)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized("Nie posiadasz uprawnień");

                return MySqlConnector.ExecuteNonQueryResult($@"
                update clients_vehicles_chains set {col}='{var}' where Id={id}
            ") > 0 ? Ok("Pomyślnie edytowano") : BadRequest("Coś poszło nie tak");
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
