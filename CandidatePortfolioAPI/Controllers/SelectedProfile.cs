using CandidatePortfolioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CandidatePortfolioAPI.Controllers
{
    public class SelectedProfile : Controller
    {

        //use Cosmos DB Service
        private readonly ICosmosDbService _cosmosDbService;


        private IConfiguration _config;


        //create Database Service Instance
        public SelectedProfile(ICosmosDbService cosmosDbService, IConfiguration config)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
            _config = config;
        }



        //Find SelectedProfile
        [HttpGet("findProfile")]
        public async Task<IActionResult> FindProfile([FromQuery] Candidates profile)
        {


            return Ok(await _cosmosDbService.GetMultipleAsync("SELECT c.id, c.firstName, c.surname, c.email, " +
                "c.employmentStatus, c.age, c.aboutMe, c.gps_lat, c.gps_long, c.username, c.skillName, c.jobTitle, c.companyName, c.institutionName, " +
                "c.studyType, c.courseName, c.gitHubLink, c.currentJob, c.currentEmployer, c.specialty FROM c WHERE IS_NULL(c.password) AND c.username ='" + profile.Username +"' "));



        }
    }
}
