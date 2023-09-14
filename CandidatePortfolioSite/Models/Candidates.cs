using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortfolioSite.Models
{

    //create frontend candidates model and assign jsonProperty specified in API
    public class Candidates
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }


        [JsonProperty(PropertyName = "gps_lat")]
        public double gps_lat { get; set; }

        [JsonProperty(PropertyName = "gps_long")]
        public double gps_long { get; set; }

        

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "age")]
        public float Age { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "aboutMe")]
        public string AboutMe { get; set; }

        [JsonProperty(PropertyName = "gitHubLink")]
        public string GitHubLink { get; set; }


        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "currentJob")]
        public string CurrentJob { get; set; }

        [JsonProperty(PropertyName = "specialty")]
        public string Specialty { get; set; }

        [JsonProperty(PropertyName = "currentEmployer")]
        public string CurrentEmployer { get; set; }

        //FOR USER ITEMS

        [JsonProperty(PropertyName = "isUser")]
        public string IsUser { get; set; }

        [JsonProperty(PropertyName = "isAdmin")]
        public string IsAdmin { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }



        //FOR Skill Items

        [JsonProperty(PropertyName = "skillName")]
        public string SkillName { get; set; }

        //upload picture testing

        [Required(ErrorMessage = "Please enter file name")]
        public string FileName { get; set; }


        [Required(ErrorMessage = "Please select file")]
        public IFormFile File { get; set; }

        //FOR EMPLOYMENT ITEMS
        [JsonProperty(PropertyName = "jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty(PropertyName = "companyName")]
        public string CompanyName { get; set; }

        //FOR EDUCATION ITEMS
        [JsonProperty(PropertyName = "institutionName")]
        public string InstitutionName { get; set; }

        [JsonProperty(PropertyName = "studyType")]
        public string StudyType { get; set; }

        [JsonProperty(PropertyName = "courseName")]
        public string CourseName { get; set; }



    }

    

}
