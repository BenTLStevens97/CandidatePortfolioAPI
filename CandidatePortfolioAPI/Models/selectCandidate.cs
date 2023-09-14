using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CandidatePortfolioAPI.Models
{
    //Create Candidate model and define JSON identifcations
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
    }
}
