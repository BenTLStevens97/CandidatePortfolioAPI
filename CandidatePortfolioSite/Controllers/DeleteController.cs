using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using CandidatePortfolioSite.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace CandidatePortfolioSite.Controllers
{
    public class DeleteController : Controller
    {
        string baseURL3 = "https://localhost:44336/api/CandidatesAPIContext/:id";

        //DELETE ITEM MATCHING THE ID
        public async Task<ActionResult<String>> DeleteCandidate(Candidates candidate)
        {

            string JWT = HttpContext.Session.GetString("_JWT");

            Candidates obj = new Candidates()
            {

                id = candidate.id
                

            };
            if (candidate.id != null)
            {



                using (var client = new HttpClient())
                {
                    var token = JWT;
                    client.BaseAddress = new Uri(baseURL3);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.DeleteAsync(candidate.id);

                    if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Candidates", "Candidates");
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web API");
                    }

                    
                }
            }return View();
        }
    }
}
