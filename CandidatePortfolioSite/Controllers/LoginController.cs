using CandidatePortfolioSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;




namespace CandidatePortfolioSite.Controllers
{
    public class LoginController : Controller
    {
        const string SessionUsername = "_Username";
        const string SessionIsAdmin = "_IsAdmin";
        const string SessionHasProfile = "_HasProfile";
        const string SessionJWTtoken = "_JWT";
        const string SessionLoginID = "_ID";
        const string SessionPassword = "_Password";
        const string SessionProfile = "_ProfileSession";

        public async Task<ActionResult<String>> Login(Login login)
        {

            
            string baseURL = "http://localhost:30352/api/login/";
            string baseURL2 = "https://localhost:44336/api/CandidatesAPIContext/";
            string baseURL3 = "http://localhost:30352/api/CandidatesAPIContext/JWT";

            
            IList<Login> logins = new List<Login>();
            IList<Login> loginAgain = new List<Login>();
            Login obj = new Login()
            {
                Username = login.Username,
                Password = login.Password,
                AccessToken = login.AccessToken
                
            };

            if (login.sendUsername != null)
            {

                using (var client = new HttpClient())
                {
                   
                    //CHECK USERNAME AND PASSWORD MATCH

                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.GetAsync("Users?Username=" + login.sendUsername + "&Password=" + login.sendPassword);

                    string results = getData.Content.ReadAsStringAsync().Result;
                    logins = JsonConvert.DeserializeObject<List<Login>>(results);

                    if (logins.Count > 0)
                    {


                        Login PutObj = new Login()
                        {
                            id = logins[0].id,
                            Username = logins[0].Username,
                            Password = logins[0].Password,
                            AccessToken = logins[0].AccessToken,
                            IsAdmin = logins[0].IsAdmin,
                            HasProfile = logins[0].HasProfile,
                            IsUser = "Yes"
                        };


                        HttpContext.Session.SetString(SessionLoginID, logins[0].id);
                        HttpContext.Session.SetString(SessionUsername, logins[0].Username);
                        HttpContext.Session.SetString(SessionPassword, logins[0].Password);

                        using (var clientUpdate = new HttpClient())
                        {

                            clientUpdate.BaseAddress = new Uri(baseURL3);
                            clientUpdate.DefaultRequestHeaders.Accept.Clear();
                            clientUpdate.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            
                            //GENERATE JWT
                            HttpResponseMessage getUpdatedData = await clientUpdate.PutAsJsonAsync("JWT", PutObj);
                            
                            //RETRIEVE JWT
                            return RedirectToAction("GetToken");
                        }
                                

                            

                      
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web API");
                    }

                    ViewData.Model = login;
                }

                return View("../Shared/LoginFail");
            }



            else
            {
                Console.WriteLine("Error Calling Web API");
            }

                    return View();

                }




        string baseURL4 = "https://localhost:44336/api/CandidatesAPIContext/";
        public async Task<ActionResult<String>> GetToken(Login login)
        {
            string baseURL = "http://localhost:30352/api/login/";
            string baseURL2 = "https://localhost:44336/api/CandidatesAPIContext/";
            string baseURL3 = "http://localhost:30352/api/CandidatesAPIContext/JWT";

            //CALLING web api and populating the data in view using Games Class
            IList<Login> logins = new List<Login>();
            IList<Login> loginAgain = new List<Login>();
            Login obj = new Login()
            {
                Username = login.Username,
                Password = login.Password,
                AccessToken = login.AccessToken

            };

            string loginUsername = HttpContext.Session.GetString("_Username");
            string loginPassword = HttpContext.Session.GetString("_Password");




            if (loginUsername != null)
            {

                using (var client = new HttpClient())
                {

                    //GET TOKEN USERNAME AND IS ADMIN 

                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.GetAsync("Users?Username=" + loginUsername + "&Password=" + loginPassword);

                    string results = getData.Content.ReadAsStringAsync().Result;
                    logins = JsonConvert.DeserializeObject<List<Login>>(results);

                    if (logins.Count > 0)
                    {
                        var AccessToken2 = logins[0].AccessToken;

                        HttpContext.Session.SetString(SessionUsername, logins[0].Username);
                        HttpContext.Session.SetString(SessionIsAdmin, logins[0].IsAdmin);

                        if (AccessToken2 != null)
                        {
                            HttpContext.Session.SetString(SessionJWTtoken, AccessToken2);

                        }

                        return RedirectToAction("Candidates", "Candidates");
                       
                    }


                

                else
                {
                    Console.WriteLine("Error Calling Web API");
                }

                ViewData.Model = login;
            }

        }
            
            return View("../Shared/LoginFail");
        }

        public async Task<ActionResult> SignOut()
        {
            HttpContext.Session.SetString(SessionUsername, "");
            HttpContext.Session.SetString(SessionIsAdmin, "");
            HttpContext.Session.SetString(SessionJWTtoken, "");
            HttpContext.Session.SetString(SessionLoginID, ""); 
            HttpContext.Session.SetString(SessionPassword, "");
            HttpContext.Session.SetString(SessionProfile, "");

            return RedirectToAction("Login", "Login");

        }


    }
}










