using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using CandidatePortfolioSite.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace CandidatePortfolioSite.Controllers
{
    public class InsertController : Controller
    {

        string baseURL = "https://localhost:44336/api/CandidatesAPIContext";
        public async Task<ActionResult<String>> InsertCandidate(Candidates candidate)
        {
            string loggedInUser = HttpContext.Session.GetString("_Username");


            //IF LOGGED IN USER HAS A PROFILE THEY CAN'T CREATE ANOTHER -- RETURN THEM TO THEIR PROFILE
            string ProfileSession = HttpContext.Session.GetString("_ProfileSession");
            if(ProfileSession == "True")
            {
                return RedirectToAction("candidateProfile", "Candidates", new {Username=loggedInUser});
            }


            
            Candidates obj = new Candidates()
            {


                FirstName = candidate.FirstName,
                Surname = candidate.Surname,
                Username = candidate.Username,
                Age = candidate.Age,
                AboutMe = candidate.AboutMe,
                gps_lat = candidate.gps_lat,
                gps_long = candidate.gps_long,
                GitHubLink = candidate.GitHubLink,
                Specialty = candidate.Specialty,
                CurrentJob = candidate.CurrentJob,
                CurrentEmployer = candidate.CurrentEmployer,
                Email = candidate.Email
            };

            //ACCESS TOKEN FOR AUTHORISATION
            string AccessToken = HttpContext.Session.GetString("_JWT");

            

            if (candidate.FirstName != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //AUTHORISATION HEADER FOR JWT
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                    HttpResponseMessage getData = await client.PostAsJsonAsync("CandidatesAPIContext", obj);

                    if (getData.IsSuccessStatusCode)
                    {




                        return RedirectToAction("candidateProfile", "Candidates", new { Username = candidate.Username });
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

        public async Task<ActionResult<String>> InsertSkill(Candidates candidate)
        {
            string loggedInUser = HttpContext.Session.GetString("_Username");

            Candidates obj = new Candidates()
            {

                Username = candidate.Username,
                SkillName = candidate.SkillName
            };
            string AccessToken = HttpContext.Session.GetString("_JWT");



            if (candidate.SkillName != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                    HttpResponseMessage getData = await client.PostAsJsonAsync("CandidatesAPIContext",obj);

                    if (getData.IsSuccessStatusCode)
                    {




                        return RedirectToAction("candidateProfile", "Candidates", new { Username = candidate.Username });
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





        public async Task<ActionResult<String>> InsertJob(Candidates candidate)
        {
            

            Candidates obj = new Candidates()
            {

                Username = candidate.Username,
                JobTitle = candidate.JobTitle,
                CompanyName = candidate.CompanyName
            };
            string AccessToken = HttpContext.Session.GetString("_JWT");



            if (candidate.JobTitle != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                    HttpResponseMessage getData = await client.PostAsJsonAsync("CandidatesAPIContext", obj);

                    if (getData.IsSuccessStatusCode)
                    {




                        return RedirectToAction("candidateProfile", "Candidates", new { Username = candidate.Username });
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



        public async Task<ActionResult<String>> InsertEducation(Candidates candidate)
        {


            Candidates obj = new Candidates()
            {

                Username = candidate.Username,
                InstitutionName = candidate.InstitutionName,
                CourseName = candidate.CourseName,
                StudyType = candidate.StudyType
            };
            string AccessToken = HttpContext.Session.GetString("_JWT");



            if (candidate.InstitutionName != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                    HttpResponseMessage getData = await client.PostAsJsonAsync("CandidatesAPIContext", obj);

                    if (getData.IsSuccessStatusCode)
                    {




                        return RedirectToAction("candidateProfile", "Candidates", new { Username = candidate.Username });
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

        public async Task<ActionResult<String>> InsertUser(Candidates candidate)
        {


            Candidates obj = new Candidates()
            {

                
                Username = candidate.Username,
                Password = candidate.Password,
                IsAdmin = candidate.IsAdmin,
                IsUser = "Yes"
            };
            string AccessToken = HttpContext.Session.GetString("_JWT");



            if (candidate.Username != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                    HttpResponseMessage getData = await client.PostAsJsonAsync("CandidatesAPIContext", obj);

                    if (getData.IsSuccessStatusCode)
                    {




                        return RedirectToAction("GetUsers", "Users");
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
