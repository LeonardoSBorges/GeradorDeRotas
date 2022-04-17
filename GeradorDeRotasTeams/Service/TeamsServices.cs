using Microsoft.AspNetCore.Mvc;
using ModelShare.Entities;
using ModelShare.Utils;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeradorDeRotasTeams.Service
{
    public class TeamsServices
    {
        public readonly IMongoCollection<Teams> _connection;

        public TeamsServices(IConnectionMongoDB settings)
        {
            var service = new MongoClient(settings.ConnectionString);
            var database = service.GetDatabase(settings.DataBaseName);
            _connection = database.GetCollection<Teams>(settings.CollectionName);
        }


        public async Task<IEnumerable<Teams>> GetPeople() =>
            await _connection.Find(getTeams => true).ToListAsync();

        public async Task<Teams> GetPerson(string id) =>
            await _connection.Find(getTeams => getTeams.Id == id).FirstOrDefaultAsync();

        public async Task<(int, string)> PostNewPerson(Teams teams)
        {
            if (teams.Name.Length <= 4)
                return (400, "Insira um nome com mais de 4 caracteres");

            await _connection.InsertOneAsync(teams);

            return (201, "Um novo registro foi adicionado!");
        }

        public async Task<(int, string)> Update(Teams teams)
        {

            var result = _connection.ReplaceOneAsync(searchTeams => searchTeams.Id == teams.Id, teams).Result;
            if (result.MatchedCount == 0)
                return (400, "Nenhum registro foi encontrado com este id!");

            return (204, "O registro foi atualizado com sucesso!");
        }

        public async Task Delete(string id)
        {
            await _connection.DeleteOneAsync(searchTeams => searchTeams.Id == id);
        }

    }
}
