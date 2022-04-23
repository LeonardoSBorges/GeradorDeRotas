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
        
        public async Task<List<Teams>> GetAll()
        {
            //await UpdateTeamsData();
            var result = await _teams.Find(getTeams => true).ToListAsync();
          
            return result;
        }
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
                    Person person = await ConsumeWebAPI.GetById(personOfList.Id);
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
            var team = await _teams.Find(searchTeam => searchTeam.Id == id).FirstOrDefaultAsync();
            if (team.People != null)
            {
                foreach (var item in team.People)
                {
                    item.HaveTeam = false;
                    await ConsumeWebAPI.UpdateValue(item);
                }
            }

            var countDeleteTeams = _teams.DeleteOne(searchTemasForDelete => searchTemasForDelete.Id == id).DeletedCount;

            if (countDeleteTeams.Equals(0))
                return new NotFoundResult().StatusCode;

            return new OkResult().StatusCode;
        }
        public async Task<bool> UpdateTeamsData()
        {
            var flafFinished = false;
            ICollection<Teams> teams= await _teams.Find(getTeams => true).ToListAsync();
            ICollection<Teams> updateTeam = new List<Teams>();
            foreach (var team in teams)
            {
                foreach(var person in team.People)
                {
                    Person resultSearchIfExistPerson = await ConsumeWebAPI.GetById(person.Id);

                    if(resultSearchIfExistPerson.Name != person.Name)
                    {
                        team.People.Remove(person);
                    }

                }
                var address = await ConsumeWebAPI.GetAddress(team.Address.Id);

                if(address.CityState != team.Address.CityState)
                {
                    team.Address = address;
                }


                updateTeam.Add(team);
            }
            foreach (var team in updateTeam)
            {
                await Replace(team);
            }

            return flafFinished;
        }
    }
}
