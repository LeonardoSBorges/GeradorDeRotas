using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MVC.Services
{
    public static class PersonServices
    {
        private static readonly string baseUri = "https://localhost:44308/api/";
        public static async Task<List<Person>> GetAll()
        {
            List<Person> people = null;

            using (var httpClient = new HttpClient())
             {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Values");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    people = JsonConvert.DeserializeObject<List<Person>>(responseBody);
                }
                else
                {
                    people = new List<Person>();
                }
            }

            return people;
        }


        public static async Task Create(Person person)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await httpClient.PostAsJsonAsync("Values", person);
            }
        }

        public static async Task<Person> Details(string id)
        {
            using (var httpClient = new HttpClient())
            {
                Person people;
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Values/GetById/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    people = JsonConvert.DeserializeObject<Person>(responseBody);
                }
                else
                {
                    people = null;
                }
                return people;
            }
        }

        public static async Task Update(Person person)
        {
            
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.PutAsJsonAsync("Values", person);
            }
        }

        public static async Task Delete(string id)
        {
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.DeleteAsync("Values/" + id);
            }
        }
    }
}
