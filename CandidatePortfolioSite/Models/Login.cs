using Newtonsoft.Json;

namespace CandidatePortfolioSite.Models
{
    public class Login
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        public string sendUsername { get; set; }
        public string sendPassword { get; set; }

        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "isAdmin")]
        public string IsAdmin { get; set; }

        [JsonProperty(PropertyName = "hasProfile")]
        public string HasProfile { get; set; }

        [JsonProperty(PropertyName = "isUser")]
        public string IsUser { get; set; }




    }
}
