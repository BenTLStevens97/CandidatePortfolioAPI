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
    public class SingleCandidateController : Controller
    {
        //private readonly ILogger<SingleCandidateController> _logger;

        string baseURL = "http://localhost:30352/specCandidate";
        string baseURL3 = "http://localhost:30352/api/CandidatesAPIContext/JWT";


        public async Task<IActionResult> SingleCandidate(selectCandidate singCandidate, Candidates candidates)
        {
           
            
            selectCandidate obj = new selectCandidate()
            {
                
                FirstName = singCandidate.FirstName,
                Surname = singCandidate.Surname,
                Email = singCandidate.Email,
                Age = singCandidate.Age,
                AboutMe = singCandidate.AboutMe,
                gps_lat = singCandidate.gps_lat,
                gps_long = singCandidate.gps_long,
                GitHubLink = singCandidate.GitHubLink
            };
            
            IList<selectCandidate> SpecCand = new List<selectCandidate>();

            if (candidates.id != null)
            {

                using (var client = new HttpClient())
                {


                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.GetAsync("SpecCandidate?id=" + candidates.id);

                    

                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        SpecCand = JsonConvert.DeserializeObject<List<selectCandidate>>(results);

                        

                        
                    }


                    else
                    {
                        Console.WriteLine("Error Calling Web API");
                    }


                    ViewData.Model = SpecCand;

                }
            }
            return View();

        }








        public async Task<IActionResult> SingleCandidateRO(selectCandidate singCandidate, Candidates candidates)
        {


            selectCandidate obj = new selectCandidate()
            {

                FirstName = singCandidate.FirstName,
                Surname = singCandidate.Surname,
                Email = singCandidate.Email,
                Age = singCandidate.Age,
                AboutMe = singCandidate.AboutMe,
                gps_lat = singCandidate.gps_lat,
                gps_long = singCandidate.gps_long,
                GitHubLink = singCandidate.GitHubLink
            };

            IList<selectCandidate> SpecCand = new List<selectCandidate>();

            if (candidates.id != null)
            {

                using (var client = new HttpClient())
                {


                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.GetAsync("SpecCandidate?id=" + candidates.id);



                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        SpecCand = JsonConvert.DeserializeObject<List<selectCandidate>>(results);




                    }


                    else
                    {
                        Console.WriteLine("Error Calling Web API");
                    }


                    ViewData.Model = SpecCand;

                }
            }
            return View();

        }












        public async Task<IActionResult> GenerateToken(selectCandidate singCandidate)
        {
            selectCandidate obj = new selectCandidate()
            {
                id = singCandidate.id,
                FirstName = singCandidate.FirstName,
                Surname = singCandidate.Surname,
                Email = singCandidate.Email,
                Age = singCandidate.Age,
                AboutMe = singCandidate.AboutMe,
                gps_lat = singCandidate.gps_lat,
                gps_long = singCandidate.gps_long,
                GitHubLink = singCandidate.GitHubLink
            };

            if (singCandidate.FirstName != null)
            {

                using (var clientUpdate = new HttpClient())
                {







                    clientUpdate.BaseAddress = new Uri(baseURL3);
                    clientUpdate.DefaultRequestHeaders.Accept.Clear();
                    clientUpdate.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getUpdatedData = await clientUpdate.PutAsJsonAsync("JWT", obj);



                    if (getUpdatedData.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Candidates", "Candidates");
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web API");
                    }

                    ViewData.Model = singCandidate;
                }
                
            }
            return View();
        }

    }
}
