using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CandidatePortfolioAPI.Models
{
    //Create Candidates model and define JSON identifcations
    public class Candidates
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

        [JsonProperty(PropertyName = "specialty")]
        public string Specialty { get; set; }

        [JsonProperty(PropertyName = "currentJob")]
        public string CurrentJob { get; set; }

        [JsonProperty(PropertyName = "currentEmployer")]
        public string CurrentEmployer { get; set; }


        //USERS ITEMS



        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "isUser")]
        public string IsUser { get; set; }


        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken { get; set; }


        [JsonProperty(PropertyName = "isAdmin")]
        public string IsAdmin { get; set; }

        [JsonProperty(PropertyName = "hasProfile")]
        public string HasProfile { get; set; }

        //FOR SKILLS ITEMS

        [JsonProperty(PropertyName = "skillName")]
        public string SkillName { get; set; }

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

    //Define Cosmos DB operations for Candidates Model to Azure Cosmos DB
    public interface ICosmosDbService
    {
        Task<IEnumerable<Candidates>> GetMultipleAsync(string query);

        Task<Candidates> GetAsync(string id);
        Task AddAsync(Candidates Game);
        Task UpdateAsync(string id, Candidates Candidate);
        Task DeleteAsync(string id);
    }

    //Create CosmosDB class to define Azure Cosmos DB operations configurations to setup  in Startup.cs
    public class CosmosDbService : ICosmosDbService
    {

        //Cosmos Connection Setup
        private Container _container;
        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }

        //Input into Cosmos DB Op
        public async Task AddAsync(Candidates Candidate)
        {
            await _container.CreateItemAsync(Candidate, new PartitionKey(Candidate.id));
        }


        //Delete from Cosmos DB Op
        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Candidates>(id, new PartitionKey(id));
        }

        //GET From Cosmos DB Op
        public async Task<Candidates> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Candidates>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<Candidates>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Candidates>(new QueryDefinition(queryString));
            var results = new List<Candidates>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateAsync(string id, Candidates item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
    } 
}
