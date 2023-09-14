using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using CandidatePortfolioSite.Models;

namespace CandidatePortfolioSite.Controllers
{
    public class MapController : Controller
    {
        string baseURL = "http://localhost:30352/api/CandidatesAPIContext/AllCandidates";

        public async Task<IActionResult> MapCoords()
        {

            IList<Candidates> mapCoords = new List<Candidates>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("AllCandidates");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    mapCoords = JsonConvert.DeserializeObject<List<Candidates>>(results);
                }
                else
                {
                    Console.WriteLine("Error Calling Web API");
                }

                ViewData.Model = mapCoords;
                return Json(mapCoords);
               

            }
        }

        public async Task<IActionResult> Map()
        {

            IList<Map> map = new List<Map>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("gps");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    map = JsonConvert.DeserializeObject<List<Map>>(results);
                }
                else
                {
                    Console.WriteLine("Error Calling Web API");
                }

                ViewData.Model = map;
                
                return View();

            }
        }
    }
}
