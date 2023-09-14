using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Net.Http;
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
using UploadFile.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;


namespace CandidatePortfolioSite.Controllers
{
    public class EditCandidate : Controller
    {
        public IActionResult uploadView()
        {
            SingleFileModel model = new SingleFileModel();
            return View(model);
        }

        

        string baseURL3 = "https://localhost:44336/api/CandidatesAPIContext/:id";
        public async Task<ActionResult<String>> EditCandidates(Candidates candidate)
        {
            SingleFileModel model = new SingleFileModel();
            string JWT = HttpContext.Session.GetString("_JWT");

            Candidates obj = new Candidates()
            {

                id = candidate.id,
                FirstName = candidate.FirstName,
                Surname = candidate.Surname,
                Age = candidate.Age,
                AboutMe = candidate.AboutMe,
                Email = candidate.Email,
                gps_lat = candidate.gps_lat,
                gps_long = candidate.gps_long,
                GitHubLink = candidate.GitHubLink,
                AccessToken = JWT,
                Username = candidate.Username,
                CurrentJob = candidate.CurrentJob,
                CurrentEmployer = candidate.CurrentEmployer,
                Specialty = candidate.Specialty
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
                        return RedirectToAction("candidateProfile", "Candidates", new {Username=candidate.Username});
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

        /*

        [HttpPost]
        public IActionResult Upload(SingleFileModel model)
        {

            SingleFileModel obj = new SingleFileModel()
            {
                FileName = model.FileName
                
            };

            if (ModelState.IsValid)
            {
                model.IsResponse = true;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string fileName = model.id + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);
                System.IO.File.Delete(fileNameWithPath);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {

                    model.File.CopyTo(stream);
                }

                model.IsSuccess = true;
                model.Message = "File upload successfully";




            }
            return View("EditCandidates", model);
        }
        */


        public IActionResult EditCandidateView(Candidates candidate)
        {
            Candidates obj = new Candidates()
            {

                id = candidate.id,
                FirstName = candidate.FirstName,
                Surname = candidate.Surname,
                Age = candidate.Age,
                AboutMe = candidate.AboutMe,
                Email = candidate.Email,
                gps_lat = candidate.gps_lat,
                gps_long = candidate.gps_long,
                GitHubLink = candidate.GitHubLink,
                
                Username = candidate.Username,
                CurrentJob = candidate.CurrentJob,
                CurrentEmployer = candidate.CurrentEmployer,
                Specialty = candidate.Specialty
            };


            return View();
        }
    }
}
