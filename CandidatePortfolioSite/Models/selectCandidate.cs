using Newtonsoft.Json;

namespace CandidatePortfolioSite.Models
{
    public class selectCandidate
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }


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

        [JsonProperty(PropertyName = "gps_lat")]
        public double gps_lat { get; set; }

        [JsonProperty(PropertyName = "gps_long")]
        public double gps_long { get; set; }

        [JsonProperty(PropertyName = "gitHubLink")]
        public string GitHubLink { get; set; }

        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken { get; set; }
    }
}
