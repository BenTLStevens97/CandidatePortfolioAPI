using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace UploadFile.Models
{
    public class SingleFileModel : ReponseModel
    {
       
        public string FileName { get; set; }
        [Required(ErrorMessage = "Please select file")]
        public IFormFile File { get; set; }


        //testing edit candidates

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



    }

    public class ReponseModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
    }
}
