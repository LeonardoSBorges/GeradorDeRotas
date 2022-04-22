using ModelShare.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ModelShare.Services
{
    public class ConsumeWebAPI
    {

        private static readonly string baseUri = "https://localhost:44308/api/";
        public static async Task<Person> GetPerson(string name)
        {
            using (var httpClient = new HttpClient())
            {
                Person people;
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Values/" + name);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    people = JsonConvert.DeserializeObject<Person>(responseBody);
                }
                else
                    people = null;
                
                return new Person(people.Id, people.Name, people.HaveTeam);
            }
        }

        public static async Task<Person> GetById(string id)
        {
            using (var httpClient = new HttpClient())
            {
                Person people = new Person();
                
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Values/GetById/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    people = JsonConvert.DeserializeObject<Person>(responseBody);
                }
                else
                    people = null;

                if (people == null)
                {
                    people = new Person("", "", false);
                }
                
                return new Person(people.Id, people.Name, people.HaveTeam);
            }
        }
        public static async Task UpdateValue(Person person)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);
                var response = await httpClient.PutAsJsonAsync("Values", person);
            }
        }

    }
}
