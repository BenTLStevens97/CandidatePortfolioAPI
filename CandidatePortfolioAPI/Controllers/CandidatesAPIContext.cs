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
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Runtime.Caching;

using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace CandidatePortfolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesAPIContext : ControllerBase
    {
        //use Cosmos DB Service
        private readonly ICosmosDbService _cosmosDbService;

        private IConfiguration _config;

        private readonly IMemoryCache _memoryCache;








        //create Database Service Instance
        public CandidatesAPIContext(ICosmosDbService cosmosDbService, IConfiguration config, IMemoryCache memoryCache)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));

            _config = config;

            _memoryCache = memoryCache;
        }

        // GET using REST/HTTP, CosmosDB RUN SELECT QUERY TO COSMOS DB




        //RESPONSE CACHE IN ENDPOINT HEADER

        [AllowAnonymous]
        
        [HttpGet("AllCandidates")]
        [ResponseCache(Duration = 120)]
        public async Task<IActionResult> List()
        {
            

                return Ok(await _cosmosDbService.GetMultipleAsync("SELECT c.id, c.firstName, c.surname, c.email, c.currentJob, c.currentEmployer, c.specialty, c.age, c.aboutMe, c.gps_lat, c.gps_long, c.username, c.skillName  FROM c WHERE NOT IS_NULL(c.firstName) AND IS_DEFINED(c.firstName)"));
        }


        //RESPONSE CACHE IN ENDPOINT HEADER

        [AllowAnonymous]

        [HttpGet("GetUsers")]
        [ResponseCache(Duration = 5)]
        public async Task<IActionResult> ListUsers()
        {


            return Ok(await _cosmosDbService.GetMultipleAsync("SELECT c.username, c.password, c.isAdmin, c.isUser, c.id FROM c WHERE IS_DEFINED(c.username) AND IS_DEFINED(c.password) and IS_NULL(c.firstName) AND c.isUser ='Yes'"));
        }








        
        [HttpGet("{id}")]
        
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _cosmosDbService.GetAsync(id));
        }

        // POST Candidates to AzureCosmos DB
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Candidates Candidate)
        {
            Candidate.id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(Candidate);
            return CreatedAtAction(nameof(Get), new { id = Candidate.id }, Candidate);
        }


        


        // PUT(UPDATE) BASED ON ID
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Edit([FromBody] Candidates Candidate)
        {



            await _cosmosDbService.UpdateAsync(Candidate.id, Candidate);
            return NoContent();
        }






        // PUT(UPDATE) BASED ON Username
        [HttpPut("JWT")]
        public async Task<IActionResult> AddJWT([FromBody] Candidates Candidate)
        {
            IActionResult response = Unauthorized();

           
                var tokenString = GenerateJSONWebToken(Candidate);
                Candidate.AccessToken = tokenString;
            
            await _cosmosDbService.UpdateAsync(Candidate.id, Candidate);
            return NoContent();
        }



        //WRITE JSON WEB TOKEN AND ENCRYPT 

        private string GenerateJSONWebToken(Candidates user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        private canUser AuthenticateUser(Candidates login)
        {
            canUser user = null;




            //Validate the User Credentials
            //Testing Purpose
            if (login.Username == "benStevens97")
            {
                user = new canUser { Username = "benStevens97", Password = "password" };
            }
            return user;
        }











        // DELETE BASED ON ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cosmosDbService.DeleteAsync(id);
            return NoContent();
        }
    }
}
