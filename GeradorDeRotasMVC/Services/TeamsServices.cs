using GeradorDeRotasMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GeradorDeRotasMVC.Services
{
    public class TeamsServices
    {
        private static  readonly string baseUri = "https://localhost:44376/api/";

        public static async Task<List<Teams>> GetAll()
        {
            List<Teams> people = new List<Teams>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Teams");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    people = JsonConvert.DeserializeObject<List<Teams>>(responseBody);
                }
                else
                {
                    people = new List<Teams>();
                }
            }

            return people;
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
    }
}
