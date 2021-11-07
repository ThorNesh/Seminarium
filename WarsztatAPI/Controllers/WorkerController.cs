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
using System.IdentityModel.Tokens.Jwt;

namespace WarsztatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
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
    }
}
