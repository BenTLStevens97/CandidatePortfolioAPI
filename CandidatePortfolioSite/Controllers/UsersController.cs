using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using CandidatePortfolioSite.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace CandidatePortfolioSite.Controllers
{
    public class UsersController : Controller
    {
        string baseURL = "http://localhost:30352/api/CandidatesAPIContext/GetUsers";
        public async Task<IActionResult> GetUsers()
        {
            //CALLING web api and populate model Candidates
            IList<Candidates> candidate = new List<Candidates>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("GetUsers");

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


        string baseURL3 = "https://localhost:44336/api/CandidatesAPIContext/:id";
        public async Task<ActionResult<String>> EditUser(Candidates candidate)
        {

            string JWT = HttpContext.Session.GetString("_JWT");

            Candidates obj = new Candidates()
            {

                id = candidate.id,
                Username = candidate.Username,
                Password = candidate.Password,
                IsAdmin = candidate.IsAdmin,
                IsUser = "Yes"
            };

            if (candidate.Password != null)
            {
                using (var client = new HttpClient())
                {

                    var token = JWT;
                    client.BaseAddress = new Uri(baseURL3);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.PutAsJsonAsync("Candidates", obj);



                    if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Candidates", "Candidates");
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web API");
                        return View("../Shared/UnauthorisedMethod");
                    }

                    //ViewData.Model = candidate;
                }
            }

            return View();
        }




        public IActionResult EditUserView(Candidates candidate)
        {
            Candidates obj = new Candidates()
            {

                id = candidate.id,
                Username = candidate.Username,
                Password = candidate.Password,
                IsAdmin = candidate.IsAdmin,
                IsUser = "Yes"
            };


            return View();
        }
    }
}
