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

namespace CandidatePortfolioAPI.Controllers
{
    public class userLocationController : ControllerBase
    {
        //use Cosmos DB Service
        private readonly ICosmosDbService _cosmosDbService;

        //create Database Service Instance
        public userLocationController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }

        [HttpGet("gps")]
        public async Task<IActionResult> ListGPS()
        {
            return Ok(await _cosmosDbService.GetMultipleAsync("SELECT c.id, c.firstName, c.surname, c.email, c.employmentStatus, c.age, c.aboutMe, c.gps_lat, c.gps_long FROM c WHERE IS_DEFINED(c.firstName)"));
        }
    }
}
