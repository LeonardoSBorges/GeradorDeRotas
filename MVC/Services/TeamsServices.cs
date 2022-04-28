
using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class TeamsServices
    {
        private static  readonly string baseUri = "https://localhost:44376/api/";

        public static async Task<List<Teams>> GetAll()
        {
            List<Teams> teams = new List<Teams>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Teams");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    teams = JsonConvert.DeserializeObject<List<Teams>>(responseBody);
                }
                else
                {
                    teams = new List<Teams>();
                }
            }
            return teams;
        }


        public static async Task Create(Teams teams)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) 
                    httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await httpClient.PostAsJsonAsync("Teams/Post", teams);
            }
        }

        public static async Task<Teams> Details(string id)
        {
            using (var httpClient = new HttpClient())
            {
                Teams teams;
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Teams/GetById/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    teams = JsonConvert.DeserializeObject<Teams>(responseBody);
                }
                else
                {
                    teams = null;
                }
                return teams;
            }
        }

        public static async Task<IEnumerable<Teams>> GetTeamsByCity(string idCityOfTeams)
        {
            IEnumerable<Teams> teams = new List<Teams>();
            ICollection<Teams> teamFromCity = new List<Teams>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Teams");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    teams = JsonConvert.DeserializeObject<IEnumerable<Teams>>(responseBody);
                }
                else
                {
                    teams = new List<Teams>();
                }
            }
            foreach (var team in teams)
            {
                if (team.Address.Id == idCityOfTeams && team.IsAvailable != true)
                    teamFromCity.Add(team);
            }
            return teamFromCity;
        }

        public static async Task Update(Teams teams)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.PutAsJsonAsync("Teams", teams);
            }
        }

        public static async Task Delete(string id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.DeleteAsync("Teams/" + id);
            }
        }

        public static async Task UpdatePersonInTeams(string id, Person person)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var result = await httpClient.PutAsJsonAsync("Teams/UpdatePersonInTeams/" + id, person);
            }
        }

        public static async Task AddPersonInTeams(string id, Person person)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);
                var result = await httpClient.PutAsJsonAsync("Teams/AddPersonInTeams/" + id, person);
            }
        }
    }
}
