using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using CandidatePortfolioSite.Models;
using System.Net.Http.Headers;
using System.Net.Http;

namespace CandidatePortfolioSite.Controllers
{
    public class Register : Controller
    {


        string baseURL = "https://localhost:44336/Register";
        public async Task<ActionResult<String>> RegisterCandidate(Login candidate)
        {

            Login obj = new Login()
            {


                Username = candidate.Username,
                Password = candidate.Password,
                IsAdmin = "No",
                IsUser = "Yes"
            };
            if (candidate.Username != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    

                    HttpResponseMessage getData = await client.PostAsJsonAsync("Register", obj);

                    if (getData.IsSuccessStatusCode)
                    {




                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web API");
                    }



                    ViewData.Model = candidate;
                }
            }

            return View();




        }
    }
}
