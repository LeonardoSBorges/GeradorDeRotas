
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
    public static class AddressServices
    {
        private static readonly string baseUri = "https://localhost:44377/api/";
        public static async Task<List<Address>> GetAll()
        {
            List<Address> people = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Address");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    people = JsonConvert.DeserializeObject<List<Address>>(responseBody);
                }
                else
                {
                    people = new List<Address>();
                }
            }

            return people;
        }


        public static async Task Create(Address person)
        {
            using (var httpClient = new HttpClient())
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await httpClient.PostAsJsonAsync("Address", person);
            }
        }

        public static async Task<Address> Details(string id)
        {
            using (var httpClient = new HttpClient())
            {
                Address address;
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.GetAsync("Address/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    address = JsonConvert.DeserializeObject<Address>(responseBody);
                }
                else
                {
                    address = null;
                }
                return address;
            }
        }

        public static async Task Update(Address address)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.PutAsJsonAsync("Address", address);
            }
        }

        public static async Task Delete(string id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);

                var response = await httpClient.DeleteAsync("Address/" + id);
            }
        }
    }
}
