using Microsoft.AspNetCore.Mvc;
using ModelShare.Entities;
using ModelShare.Utils;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeradorDeRotasPerson.Service
{
    public class PersonService
    {
        private readonly IMongoCollection<Person> _connection;

        public PersonService(IConnectionMongoDB settings)
        {
            var service = new MongoClient(settings.ConnectionString);
            var database = service.GetDatabase(settings.DataBaseName);
            _connection = database.GetCollection<Person>(settings.CollectionName);
        }


        public async Task<IEnumerable<Person>> GetPeople()
        {
            var result = await _connection.Find(getPeople => true).ToListAsync();
            
            return result;
        }

        public async Task<Person> GetById(string id)
        {
            var result = await _connection.Find(getPerson => getPerson.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Person> GetPerson(string name)
        {
            var result = await _connection.Find(getPerson => getPerson.Name == name).FirstOrDefaultAsync();
            return result;
        }

        public async Task<(int, string)> PostNewPerson(Person person)
        {
            if (person.Name.Length <= 4)
                return (400, "Insira um nome com mais de 4 caracteres");

            await _connection.InsertOneAsync(person);

            return (201, "Um novo registro foi adicionado!");
        }

        public async Task<(int, string)> Update(Person person)
        {

            var result = _connection.ReplaceOneAsync(searchPerson => searchPerson.Id == person.Id, person).Result;
            if (result.MatchedCount == 0)
                return (400, "Nenhum registro foi encontrado com este id!");

            return (204, "O registro foi atualizado com sucesso!");
        }

        public async Task Delete(string id)
        {
            await _connection.DeleteOneAsync(searchPerson => searchPerson.Id == id);
        }

    }
}
