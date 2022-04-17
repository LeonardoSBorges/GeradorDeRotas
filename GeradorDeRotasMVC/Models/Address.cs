using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GeradorDeRotasMVC.Models
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string District{ get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Complement { get; set; }


        public override string ToString()
        {
            return $@"CEP: {Cep}
LOGRADOURO: {Street}
DISTRITO: {District}
NUMERO: {Number}
CIDADE: {City}
COMPLEMENTO: {Complement}";
                
        }
    }
}
