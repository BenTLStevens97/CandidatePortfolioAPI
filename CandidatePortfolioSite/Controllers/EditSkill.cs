using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using CandidatePortfolioSite.Models;

namespace CandidatePortfolioSite.Controllers
{
    public class EditSkill : Controller
    {

        string baseURL3 = "https://localhost:44336/api/CandidatesAPIContext/:id";
        public async Task<ActionResult<String>> EditSkills(Candidates candidate)
        {
            
            string JWT = HttpContext.Session.GetString("_JWT");

            Candidates obj = new Candidates()
            {

                id = candidate.id,
                Username = candidate.Username,
                SkillName = candidate.SkillName
            };


            if (candidate.Username != null)
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
                        return RedirectToAction("candidateProfile", "Candidates", new { Username = candidate.Username });
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


        public IActionResult EditSkillView(Candidates candidate)
        {
            Candidates obj = new Candidates()
            {

                id = candidate.id,
                Username = candidate.Username,
                SkillName = candidate.SkillName
            };


            return View();
        }
    }
}
