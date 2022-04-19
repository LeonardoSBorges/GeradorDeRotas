using Microsoft.AspNetCore.Mvc;
using ModelShare.Entities;
using ModelShare.Services;
using ModelShare.Utils;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeamsGeradorDeRotas.Services
{
    public class TeamsServices
    {
        private readonly IMongoCollection<Teams> _teams;
        public TeamsServices(IConnectionMongoDB settings)
        {
            var service = new MongoClient(settings.ConnectionString);
            var database = service.GetDatabase(settings.DataBaseName);
            _teams = database.GetCollection<Teams>(settings.CollectionName);
        }

        public async Task<IEnumerable<Teams>> GetAll() =>
            await _teams.Find(getTeams => true).ToListAsync();

        public async Task<Teams> GetById(string id) =>
            await _teams.Find(getTeams => getTeams.Id == id).FirstOrDefaultAsync();

        public async Task<int> Create(Teams newTeams)
        {
            var existsTeams = await _teams.Find(verifyExists => verifyExists.Name == newTeams.Name).FirstOrDefaultAsync();
            if (existsTeams == null)
            {
                List<Person> people = new List<Person>();
                foreach (var personOfList in newTeams.People)
                {
                    Person person = await  ConsumeWebAPI.GetPerson(personOfList.Name);
                    if (person != null || person.HaveTeam == false)
                    {
                        person.HaveTeam = true;
                        people.Add(person);
                        await ConsumeWebAPI.UpdateValue(person);
                    }
                }
                newTeams.People = people;
                _teams.InsertOne(newTeams);
                return new OkResult().StatusCode;
            }
            else
                return new NoContentResult().StatusCode;
        }

        public async Task<int> Replace(Teams replaceTeams)
        {
            var countModfyTeams = _teams.ReplaceOne(searchForAtualization => searchForAtualization.Name == replaceTeams.Name, replaceTeams).MatchedCount;

            if (countModfyTeams.Equals(0))
                return new NotFoundResult().StatusCode;
           

            return new OkResult().StatusCode;

        }

        public async Task<int> Delete(string id)
        {
            var countDeleteTeams = _teams.DeleteOne(searchTemasForDelete => searchTemasForDelete.Id == id).DeletedCount;

            if (countDeleteTeams.Equals(0))
                return new NotFoundResult().StatusCode;

            return new OkResult().StatusCode;
        }
    }
}
