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
    public class VehicleController : ControllerBase
    {
        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {

            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                Vehicle[] results = MySqlConnector.ExecuteQueryResult<Vehicle>($"Select vehicles.*,fuel_types.Name from vehicles join fuel_types on vehicles.Fuel_Id=fuel_types.Id group by Vehicles.Id;");
                return Ok(results);
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
        [HttpGet("Get")]
        public ActionResult Get([FromHeader] uint id)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                Vehicle[] results = Filter("vehicles.Id", id);
                return results.Length > 0 ? Ok(results[0]) : BadRequest("Brak pojazdu");
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

        [HttpGet("Filter/Brand")]
        public ActionResult FilterBrand([FromHeader] string brand)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                return Ok(Filter("Brand", brand));

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
        [HttpGet("Filter/Model")]
        public ActionResult FilterModel([FromHeader] string model)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                return Ok(Filter("Model", model));
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
        [HttpGet("Filter/ProductionYear")]
        public ActionResult FilterProductionYear([FromHeader] uint min, [FromHeader] uint max)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                return Ok(FilterRange("Production_Year", min, max));
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
        [HttpGet("Search/Vin")]
        public ActionResult SearchVin([FromHeader] string vin)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                Vehicle[] results = Filter("Vin", vin);
                return results.Length > 0 ? Ok(results[0]) : BadRequest("Brak pojazdu");
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

        [HttpGet("Filter/EnginePower")]
        public ActionResult FilterEnginePower([FromHeader] uint min, [FromHeader] uint max)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                return Ok(FilterRange("Engine_Power", min, max));
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
        [HttpGet("Filter/EngineCapacity")]
        public ActionResult FilterEngineCapacity([FromHeader] uint min, [FromHeader] uint max)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                return Ok(FilterRange("Engine_Capacity", min, max));
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
        [HttpGet("Filter/FuelType")]
        public ActionResult FilterFuelType([FromHeader] uint fuelType)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                return Ok(Filter("Fuel_Id", fuelType));
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
        private static Vehicle[] Filter(string col, object var)
        {
            return MySqlConnector.ExecuteQueryResult<Vehicle>($"Select vehicles.*,fuel_types.Name from vehicles join fuel_types on vehicles.Fuel_Id=fuel_types.Id where {col} = '{var}' group by Vehicles.Id;");
        }
        private static Vehicle[] FilterRange(string col, object min, object max)
        {
            return MySqlConnector.ExecuteQueryResult<Vehicle>($"Select vehicles.*,fuel_types.Name from vehicles join fuel_types on vehicles.Fuel_Id=fuel_types.Id where {col} >= '{min}' and {col}<={max} group by Vehicles.Id;");
        }

        [HttpPost]
        public ActionResult Post([FromBody] VehicleDB vehicle)
        {
            try
            {
                return MySqlConnector.ExecuteNonQueryResult($"insert into vehicles values(0,'{vehicle.Brand}','{vehicle.Model}','{vehicle.ProductionYear}','{vehicle.Vin}','{vehicle.RegistrationNumber}','{vehicle.Engine_Power}','{vehicle.Engine_Capacity}','{vehicle.FuelId}')")
                    > 0 ? Ok("Pomyślnie dodano") : BadRequest("Coś poszło nie tak");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("Update/Brand")]
        public ActionResult UpdateBrand([FromHeader] uint id, [FromHeader] string brand)
        {
            try
            {
                return Update(id, "Brand", brand);
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
        [HttpPut("Update/Model")]
        public ActionResult UpdateModel([FromHeader] uint id, [FromHeader] string model)
        {
            try
            {
                return Update(id, "Model", model);
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
        [HttpPut("Update/ProductionYear")]
        public ActionResult UpdateProductionYear([FromHeader] uint id, [FromHeader] string productionYear)
        {
            try
            {
                return Update(id, "Production_Year", productionYear);
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
        [HttpPut("Update/Vin")]
        public ActionResult UpdateVin([FromHeader] uint id, [FromHeader] string vin)
        {
            try
            {
                return Update(id, "Vin", vin);
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
        [HttpPut("Update/RegistrationNumber")]
        public ActionResult UpdateRegistrationNumber([FromHeader] uint id, [FromHeader] string registrationNumber)
        {
            try
            {
                return Update(id, "Registration_Number", registrationNumber);
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
        [HttpPut("Update/EnginePower")]
        public ActionResult UpdateEnginePower([FromHeader] uint id, [FromHeader] uint enginePower)
        {
            try
            {
                return Update(id, "Engine_Power", enginePower);
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
        [HttpPut("Update/EngineCapacity")]
        public ActionResult UpdateEngineCapacity([FromHeader] uint id, [FromHeader] float engineCapacity)
        {
            try
            {
                return Update(id, "Engine_Capacity", engineCapacity);
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
        [HttpPut("Update/FuelType")]
        public ActionResult UpdateFuelType([FromHeader] uint id, [FromHeader] uint fuelType)
        {
            try
            {
                return Update(id, "Fuel_Id", fuelType);
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


        ActionResult Update(uint id, string col, object var)
        {
            var token = JwtService.Verify(Request.Cookies["jwt"]);
            if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized();
            return MySqlConnector.ExecuteNonQueryResult($"update vehicles set {col}='{var}' where Id = {id}")
                > 0 ? Ok("Pomyślnie edytowano") : BadRequest("Coś poszło nie tak");
        }

        [HttpDelete]
        public ActionResult Delete([FromHeader] uint id)
        {
            try
            {
                var token = JwtService.Verify(Request.Cookies["jwt"]);
                if (token.Claims.First(x => x.Type == "IsSuperUser" && x.Value == true.ToString()) is null) return Unauthorized();
                return MySqlConnector.ExecuteNonQueryResult($"delete from vehicles where Id = {id}")
                    > 0 ? Ok("Pomyślnie usunięto") : BadRequest("Brak pojazdu");
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
