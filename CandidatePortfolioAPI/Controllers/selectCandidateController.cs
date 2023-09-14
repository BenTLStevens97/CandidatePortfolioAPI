
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Xml;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using CandidatePortfolioAPI.Models;

namespace CandidatePortfolioSite.Controllers
{
    public class SelectCandidateController : Controller
    {

        //use Cosmos DB Service
        private readonly ICosmosDbService _cosmosDbService;

        //create Database Service Instance
        public SelectCandidateController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }

        [HttpGet("SpecCandidate")]
        public async Task<IActionResult> ListCandidate([FromQuery] selectCandidate Candidate)
        {
            return Ok(await _cosmosDbService.GetMultipleAsync("SELECT c.id, c.firstName, c.surname, c.email, c.employmentStatus, c.age, c.aboutMe, c.gps_lat, c.gps_long, c.gitHubLink, c.accessToken FROM c WHERE c.id ='" + Candidate.id + "'"));
        }

        
    }
}
