using CandidatePortfolioSite.Models;

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
using HtmlAgilityPack;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CandidatePortfolioSite.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ILogger<CandidatesController> _logger;

        string baseURL = "http://localhost:30352/api/CandidatesAPIContext/AllCandidates";
        string baseURL2 = "http://localhost:30352/findProfile";


        //GET CANDIDATE PROFILE ITEMS
        public async Task<IActionResult> Candidates()
        {
            //CALLING web api and populate model Candidates
            IList<Candidates> candidate = new List<Candidates>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("AllCandidates");

                if (getData.IsSuccessStatusCode)
                {
                    //HttpContext.Session.GetString("_Username");
                    string results = getData.Content.ReadAsStringAsync().Result;
                    candidate = JsonConvert.DeserializeObject<List<Candidates>>(results);
                }
                else
                {
                    Console.WriteLine("Error Calling Web API");
                }

                ViewData.Model = candidate;
            }


            return View();
        }



        
        public async Task<IActionResult> CandidatesRO()
        {
            //CALLING web api and populate model Candidates
            IList<Candidates> candidate = new List<Candidates>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("AllCandidates");

                if (getData.IsSuccessStatusCode)
                {
                    //HttpContext.Session.GetString("_Username");
                    string results = getData.Content.ReadAsStringAsync().Result;
                    candidate = JsonConvert.DeserializeObject<List<Candidates>>(results);
                }
                else
                {
                    Console.WriteLine("Error Calling Web API");
                }

                ViewData.Model = candidate;
            }


            return View();
        }


        //GET ALL ITEMS FOR USER SELECTED
        public async Task<IActionResult> CandidateProfile(Candidates Profile)
        {
            Candidates obj = new Candidates()
            {

                FirstName = Profile.FirstName,
                Surname = Profile.Surname,
                Email = Profile.Email,
                Age = Profile.Age,
                AboutMe = Profile.AboutMe,
                gps_lat = Profile.gps_lat,
                gps_long = Profile.gps_long,
                GitHubLink = Profile.GitHubLink
            };

            IList<Candidates> profile = new List<Candidates>();

            if (Profile.Username != null)
            {
                
                
                using (var client = new HttpClient())
                {
                    

                    client.BaseAddress = new Uri(baseURL2);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.GetAsync("findProfile?Username=" + Profile.Username);



                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                       profile = JsonConvert.DeserializeObject<List<Candidates>>(results);




                    }


                    else
                    {
                        Console.WriteLine("Error Calling Web API");
                    }


                    ViewData.Model = profile;

                }
            }
            return View();

        }

















        public async Task<ActionResult<String>> SpecCandidate(Candidates specCandidates)
        {

            //CALLING web api and populating the data in view using Games Class
            IList<Candidates> SpecCandidatesl = new List<Candidates>();
            Candidates obj = new Candidates()
            {
                FirstName = specCandidates.FirstName,
                Surname = specCandidates.Surname,
                Email = specCandidates.Email,
                Age = specCandidates.Age,
                AboutMe = specCandidates.AboutMe,
                gps_lat = specCandidates.gps_lat,
                gps_long = specCandidates.gps_long
            };

            if (specCandidates.id != null)
            {

                using (var client = new HttpClient())
                {


                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.GetAsync("SpecCandidate?id=" + specCandidates.id);

                    string results = getData.Content.ReadAsStringAsync().Result;
                    SpecCandidatesl = JsonConvert.DeserializeObject<List<Candidates>>(results);

                    if (SpecCandidatesl.Count > 0)
                    {


                        return RedirectToAction("Candidates", "Candidates");
                    }


                    else
                    {
                        Console.WriteLine("Error Calling Web API");
                    }


                    ViewData.Model = specCandidates;

                }
            }
            return View();
        }


    }
}
