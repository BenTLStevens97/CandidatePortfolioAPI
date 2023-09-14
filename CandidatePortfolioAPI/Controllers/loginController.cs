using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CandidatePortfolioAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CandidatePortfolioAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        //use Cosmos DB Service
        private readonly ICosmosDbService _cosmosDbService;


        private IConfiguration _config;


        //create Database Service Instance
        public loginController(ICosmosDbService cosmosDbService, IConfiguration config)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
            _config = config;
        }



        /*

        // CHECK USERNAME EXISTS IN COSMOS DB
        [HttpGet("Username")]
        public async Task<IActionResult> GetModeratorUsername(string username)
        {
            return Ok(await _cosmosDbService.GetAsync(username));
        }


        //CHECK PASSWORD EXISTS IN COSMOS DB
        [HttpGet("Password")]
        public async Task<IActionResult> GetModeratorPassword(string password)
        {
            return Ok(await _cosmosDbService.GetAsync(password));
        }
        */

        
        //CHECK USER EXISTS IN COSMOS DB
        [HttpGet("Users")]
        public async Task<IActionResult> ListUser([FromQuery] canUser user)
        {
            
            
            return Ok(await _cosmosDbService.GetMultipleAsync("SELECT * FROM c WHERE c.username='" + user.Username + "' AND c.password='" + user.Password + "'") );


           
        }


     



    }
}
