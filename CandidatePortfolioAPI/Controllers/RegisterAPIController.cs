﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using CandidatePortfolioAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace CandidatePortfolioAPI.Controllers
{
    public class RegisterAPIController : Controller
    {
        //use Cosmos DB Service
        private readonly ICosmosDbService _cosmosDbService;

        //create Database Service Instance
        public RegisterAPIController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }


        [HttpGet("RegisterGet")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _cosmosDbService.GetAsync(id));
        }

        [HttpPost("Register")]
        
        public async Task<IActionResult> Create([FromBody]  Candidates Candidate)
        {
            Candidate.id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(Candidate);
            return CreatedAtAction(nameof(Get), new { id = Candidate.id }, Candidate);
        }
    }
}
