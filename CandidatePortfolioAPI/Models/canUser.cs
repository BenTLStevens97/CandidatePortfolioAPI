using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CandidatePortfolioAPI.Models
{
    //Create Users model and define JSON identifcations for logins
    public class canUser
    {
        
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }


        [JsonProperty(PropertyName ="accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "isAdmin")]
        public string IsAdmin { get; set; }



    }
}
