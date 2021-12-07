using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

    public class Variable<T>
    {
        public T Var { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CommisionController : ControllerBase
    {

        [HttpPost]
        public ActionResult Post([FromBody] CommisionDTO commision)
        {
            return ExecuteApi("", jwt =>
            {
                Variable<uint>[] clientId = MySqlConnector.ExecuteQueryResult<Variable<uint>>($"Select Id from clients where Phone_Number = '{commision.client.PhoneNumber}'");

                string insertClients = clientId.Length > 0 ?
                @$"
                update clients set 
                Name='{commision.client.Name}',
                LastName='{commision.client.LastName}',
                Email='{commision.client.Email}'
                where Id={clientId[0].Var};
                set @clientId = {clientId[0].Var};"
                :
                 @$"insert into clients values(
                    '0',
                    '{commision.client.Name}',
                    '{commision.client.LastName}',
                    '{commision.client.PhoneNumber}',
                    '{commision.client.Email}'
                    );
                    set @clientId = last_insert_id();";

                Variable<uint>[] vehicleId = MySqlConnector.ExecuteQueryResult<Variable<uint>>($"select Id from vehicles where Vin='{commision.vehicle.Vin}';");

                Console.WriteLine($"Client:{(clientId.Length > 0 ? clientId[0].Var : -1)} | Vehicle:{(vehicleId.Length > 0 ? vehicleId[0].Var : -1)}");
                string insertVehicle = vehicleId.Length > 0 ?
                $@"update vehicles set
                `Registration_Number`='{commision.vehicle.RegistrationNumber}',
                `Engine_Power`='{commision.vehicle.Engine_Power}',
                `Engine_Capacity`='{commision.vehicle.Engine_Capacity}',
                `Fuel_Id`='{commision.vehicle.FuelId}'
                where Id = {vehicleId[0].Var};
                set @vehicleId = {vehicleId[0].Var};"
                :
                $@"insert into vehicles values(0,
                    '{commision.vehicle.Brand}',
                    '{commision.vehicle.Model}',
                    '{commision.vehicle.ProductionYear}',
                    '{commision.vehicle.Vin}',
                    '{commision.vehicle.RegistrationNumber}',
                    '{commision.vehicle.Engine_Power}',
                    '{commision.vehicle.Engine_Capacity}',
                    '{commision.vehicle.FuelId}'
                    );
                set @vehicleId = last_insert_id();";

                string code = GenerateCode();
                var result = MySqlConnector.ExecuteNonQueryResult($@"
                    start transaction;
                    {insertClients}
                    {insertVehicle}
                    
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
            return ExecuteApi("", jwt =>
            {
                Commision[] results = MySqlConnector.ExecuteQueryResult<Commision>($@"
               select commisions.Id,
clients_vehicles_chains.Id,
clients.*,
vehicles.*,
fuel_types.Name,
clients_vehicles_chains.Message,
clients_vehicles_chains.Service,
Code,
Date_Of_Start,
Hour_Of_Start,
statuses.*,
workers.*
from commisions
join clients_vehicles_chains on commisions.Chain_Id = clients_vehicles_chains.Id
join clients on clients_vehicles_chains.Client_Id = clients.Id
join vehicles on clients_vehicles_chains.Vehicle_Id = vehicles.Id
join fuel_types on vehicles.Fuel_Id = fuel_types.Id
join statuses on commisions.Status_Id = statuses.Id
join workers on commisions.Worker_Id = workers.Id

where code = '{code}';
                ");

                return results.Length > 0 ? Ok(results[0]) : BadRequest("Błędny kod");
            });
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll([FromHeader] string authorization)
        {
            return ExecuteApi(authorization, token =>
            {
                Commision[] results = MySqlConnector.ExecuteQueryResult<Commision>($@"
               select commisions.Id,
clients_vehicles_chains.Id,
clients.*,
vehicles.*,
fuel_types.Name,
clients_vehicles_chains.Message,
clients_vehicles_chains.Service,
Code,
Date_Of_Start,
Hour_Of_Start,
statuses.*,
workers.*
from commisions
join clients_vehicles_chains on commisions.Chain_Id = clients_vehicles_chains.Id
join clients on clients_vehicles_chains.Client_Id = clients.Id
join vehicles on clients_vehicles_chains.Vehicle_Id = vehicles.Id
join fuel_types on vehicles.Fuel_Id = fuel_types.Id
join statuses on commisions.Status_Id = statuses.Id
join workers on commisions.Worker_Id = workers.Id

                ");
                return Ok(results);
            });
        }

        public class CommisionStatusDTO
        {
            public uint Id { get; set; }
            public uint Status { get; set; }
        }

        [HttpPut("UpdateStatus")]
        public ActionResult UpdateStatus([FromHeader] string authorization, [FromBody] CommisionStatusDTO status)
        {
            return ExecuteApi(authorization, token =>
            {
                if (token.Claims.First(x =>
                        (x.Type == "Hired" && x.Value == true.ToString())) is null) return BadRequest("Nie jesteś zatrudniony");

                Func<ActionResult> func = () => MySqlConnector.ExecuteNonQueryResult(@$"update commisions set Status_Id={status.Status} where Id={status.Id}") > 0 ?
                    Ok("Pomyślnie edytowano") : BadRequest("Coś poszło nie tak");


                Variable<uint>[] WorkerAccountId = MySqlConnector.ExecuteQueryResult<Variable<uint>>($"Select Worker_Id from commisions where Id = {status.Id}");
                if (WorkerAccountId.Length < 1) return BadRequest("Brak takiego zlecenia");

                Variable<uint>[] UserId = MySqlConnector.ExecuteQueryResult<Variable<uint>>($"select users.Id from commisions join users on commisions.Worker_Id=users.Worker_Id where users.Worker_Id = {WorkerAccountId[0].Var}");

                if (uint.Parse(token.Issuer) == UserId[0].Var) return func();

                try
                {
                    token.Claims.First(x =>
                    (x.Type == "IsSuperUser" && x.Value == true.ToString()));
                }
                catch (Exception) { return Unauthorized("Nie posiadasz uprawnień"); }

                return func();


            });
        }

        public class CommisionWorkerDTO
        {
            public uint Id { get; set; }
            public uint WorkerId { get; set; }
        }
        [HttpPut("UpdateWorker")]
        public ActionResult UpdateWorker([FromHeader] string authorization, [FromBody] CommisionWorkerDTO Worker)
        {
            return ExecuteApi(authorization, token =>
            {
                if (token.Claims.First(x =>
                        (x.Type == "Hired" && x.Value == true.ToString())) is null) return BadRequest("Nie jesteś zatrudniony");

                Variable<uint>[] WorkerId = MySqlConnector.ExecuteQueryResult<Variable<uint>>($"select Worker_Id from commisions where Id = {Worker.Id}");

                if (WorkerId.Length < 1) return BadRequest("Brak zlecenia");

                Func<ActionResult> func = () => MySqlConnector.ExecuteNonQueryResult($"Update commisions set Worker_Id = {Worker.WorkerId} where Id = {Worker.Id}") > 0 ?
                Ok("Pomyślnie edytowano") : BadRequest("Coś poszło nie tak");

                if (WorkerId[0].Var == 0) return func();

                try
                {
                    token.Claims.First(x =>
                    (x.Type == "IsAdmin" && x.Value == true.ToString())
                    );
                }
                catch (Exception) { return Unauthorized("Nie posiadasz uprawnień"); }

                return func();
            }
            );
        }

        public class CommisionDateDTO
        {
            public string Code { get; set; }
            public string DateTime { get; set; }
        }
        [HttpPut("UpdateDate")]
        public ActionResult UpdateDate([FromBody] CommisionDateDTO date)
        {
            return ExecuteApi(null, token => {
                return MySqlConnector.ExecuteNonQueryResult($"update commisions set Date_Of_Start ='{date.DateTime}' where Code = '{date.Code}';") > 0 ? Ok("Pomyślnie edytowano") : BadRequest("Błędny kod");
            });
        }
        [HttpPut("UpdateTime")]
        public ActionResult UpdateTime([FromBody] CommisionDateDTO time)
        {
            return ExecuteApi(null, token => {
                return MySqlConnector.ExecuteNonQueryResult($"update commisions set Hour_Of_Start ='{time.DateTime}' where Code = '{time.Code}'") > 0 ? Ok("Pomyślnie edytowano") : BadRequest("Błędny kod");
            });
        }

        ActionResult ExecuteApi(string authorization, Func<JwtSecurityToken, ActionResult> func)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authorization)) return func(null);
                return func(JwtService.Verify(authorization));
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
