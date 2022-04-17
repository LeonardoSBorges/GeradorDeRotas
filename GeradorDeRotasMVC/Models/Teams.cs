using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GeradorDeRotasMVC.Models
{
    public class Teams
    {
        [BsonId]
        [BsonIgnore]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Name { get; set; }
        [BsonRequired]
        public List<Person> Person { get; set; }
        [BsonRequired]
        public string City { get; set; }
    }
}
