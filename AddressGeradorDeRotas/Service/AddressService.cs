using ModelShare.Entities;
using ModelShare.Utils;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressGeradorDeRotas.Service
{
    public class AddressService
    {
        public readonly IMongoCollection<Address> _connection;

        public AddressService(IConnectionMongoDB settings)
        {
            var service = new MongoClient(settings.ConnectionString);
            var database = service.GetDatabase(settings.DataBaseName);
            _connection = database.GetCollection<Address>(settings.CollectionName);
        }


        public async Task<IEnumerable<Address>> GetAllAddress() =>
            await _connection.Find(getAddress => true).ToListAsync();

        public async Task<Address> GetAddress(string id) =>
            await _connection.Find(getAddress => getAddress.Id == id).FirstOrDefaultAsync();

        public async Task<(int, string)> PostNewPerson(Address address)
        {
            await _connection.InsertOneAsync(address);

            return (201, "Um novo registro foi adicionado!");
        }

        public async Task<(int, string)> Update(Address address)
        {
            var result = _connection.ReplaceOneAsync(getAddress => getAddress.Id == address.Id, address).Result;
            if (result.MatchedCount == 0)
                return (400, "Nenhum registro foi encontrado com este id!");

            return (204, "O registro foi atualizado com sucesso!");
        }

        public async Task Delete(string id)
        {
            await _connection.DeleteOneAsync(searchAddress => searchAddress.Id == id);
        }

    }
}
