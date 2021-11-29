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
        public ActionResult GetAll([FromHeader] string authorization)
        {
            return ExecuteGet(authorization,()=>Ok(Filter("0",0)));
        }
        [HttpGet("Get")]
        public ActionResult Get([FromHeader] string authorization, [FromHeader] uint id)
        {
            return ExecuteGet(authorization,()=> {
                Chains[] results = Filter("clients_vehicles_chains.Id", id);
                return results.Length > 0 ? Ok(results[0]) : BadRequest("Brak powiązania");
            });
        }

        ActionResult ExecuteGet(string authorization, Func<ActionResult> func)
        {
            try
            {
                if (string.IsNullOrEmpty(authorization)) return func();
                JwtService.Verify(authorization);
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
        public ActionResult Delete([FromHeader] string authorization, [FromHeader] uint id)
        {
            try
            {
                var token = JwtService.Verify(authorization);
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
        public ActionResult UpdateClient([FromHeader] string authorization, [FromHeader] uint id,[FromHeader]uint clientId)
        {
            return ExecuteUpdate(authorization, id, "Client_Id", clientId);
        }
        [HttpPut("Update/Vehicle")]
        public ActionResult UpdateVehicle([FromHeader] string authorization, [FromHeader] uint id, [FromHeader] uint vehicleId)
        {
            return ExecuteUpdate(authorization, id,"Vehicle_Id",vehicleId);
        }
        [HttpPut("Update/Message")]
        public ActionResult UpdateMessage([FromHeader] string authorization, [FromHeader] uint id, [FromHeader] string message)
        {
            return ExecuteUpdate(authorization, id, "Message", message);
        }
        [HttpPut("Update/Service")]
        public ActionResult UpdateService([FromHeader] string authorization,[FromHeader] uint id, [FromHeader] string service)
        {
            return ExecuteUpdate(authorization, id, "Message", service);
        }

        private ActionResult ExecuteUpdate(string authorization, uint id, string col, object var)
        {
            try
            {
                var token = JwtService.Verify(authorization);
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
