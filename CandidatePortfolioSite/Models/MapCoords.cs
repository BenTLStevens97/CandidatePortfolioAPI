using Newtonsoft.Json;

namespace CandidatePortfolioSite.Models
{
    public class MapCoords
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

     



        [JsonProperty(PropertyName = "gps_lat")]
        public double gps_lat { get; set; }

        [JsonProperty(PropertyName = "gps_long")]
        public double gps_long { get; set; }
    }
}
