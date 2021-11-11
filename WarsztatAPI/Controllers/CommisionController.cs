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
    public class CommisionDTO
    {
        public Client client { get; set; }
        public VehicleDB vehicle { get; set; }
        public string message { get; set; }
        public string service { get; set; }
        public string DateOfStart { get; set; }
        public string HourOfStart { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CommisionController : ControllerBase
    {

        [HttpPost]
        public ActionResult Post([FromBody] CommisionDTO commision)
        {
            return ExecuteApi(jwt =>
            {
                string code = GenerateCode();
                var result = MySqlConnector.ExecuteNonQueryResult($@"
                    start transaction;
                    insert into clients values(
                    '0',
                    '{commision.client.Name}',
                    '{commision.client.LastName}',
                    '{commision.client.PhoneNumber}',
                    '{commision.client.Email}'
                    );
                    set @clientId = last_insert_id();
                    insert into vehicles values(0,
                    '{commision.vehicle.Brand}',
                    '{commision.vehicle.Model}',
                    '{commision.vehicle.ProductionYear}',
                    '{commision.vehicle.Vin}',
                    '{commision.vehicle.RegistrationNumber}',
                    '{commision.vehicle.Engine_Power}',
                    '{commision.vehicle.Engine_Capacity}',
                    '{commision.vehicle.FuelId}'
                    );
                    set @vehicleId=last_insert_id();
                    insert into clients_vehicles_chains values(0,
                    @clientId,
                    @vehicleId,
                    '{commision.message}',
                    '{commision.service}'
                    );
                    set @chainId = last_insert_id();
                    insert into commisions values(0,
                    @chainId,
                    '{code}',
                    '{commision.DateOfStart}',
                    '{commision.HourOfStart}',
                    '1',
                    '0'
                    );
                    commit;
                    ");
                return result > 0 ? Created("succes", code) : BadRequest("Błąd");


            }
            );
        }

        private string GenerateCode()
        {
            string result = string.Empty;
            List<char> charList = new();

            for (int i = 48; i < 58; i++)
            {
                charList.Add((char)i);
            }
            for (int i = 65; i < 91; i++)
            {
                charList.Add((char)i);
                charList.Add((char)(i + 32));
            }
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 8; i++)
            {
                result += charList[rand.Next(0, charList.Count)];
            }
            return result;
        }

        [HttpGet("Get")]
        public ActionResult Get([FromHeader] string code)
        {
            return ExecuteApi(jwt =>
            {
                Commision[] results = MySqlConnector.ExecuteQueryResult<Commision>($@"
                select commision.Id,
clients_vehicles_chains.Id,
clients.*,
vehicle.*,
fuel_types.Name,
clients_vehicles_chains.Message,
clients_vehicles_chains.Service,
Code,
Date_Of_Start,
Hour_Of_Start,
statuses.*,
workers.* from clients_vehicles_chains 
join clients_vehicles_chains on clients_vehicles_chains.Id=Chain_Id
join clients on clients.Id = clients_vehicles_chains.Client_Id
join vehicles on vehicles.Id=clients_vehicles_chains.Vehicle_Id
join fuel_types on vehicles.Fuel_Id = fuel_types.Id
join statuses on commisions.Status_Id=statuses.Id
join workers on commisions.Worker_Id=workers.Id

where code = '{code}';
                ");

                return results.Length > 0 ? Ok(results[0]) : BadRequest("Błędny kod");
            });
        }



        ActionResult ExecuteApi(Func<string,ActionResult> func, bool authorite = true)
        {
            try
            {
                string jwt = "";
                if (authorite) jwt = Request.Cookies["jwt"];
                return func(jwt);
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
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


    }
}
