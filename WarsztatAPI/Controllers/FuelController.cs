using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class FuelController : ControllerBase
    {
        [HttpGet("Get")]
        public ActionResult Get([FromHeader] uint id)
        {
            try
            {
                FuelType[] result = MySqlConnector.ExecuteQueryResult<FuelType>($"Select * from fuel_types where Id = {id}");
                return result.Length > 0 ? Ok(result[0]) : BadRequest("Brak typu paliwa");
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
                return Ok(MySqlConnector.ExecuteQueryResult<FuelType>("select * from fuel_types"));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
