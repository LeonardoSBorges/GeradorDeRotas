using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GeradorDeRotasMVC.Models
{
    public class Teams
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public virtual List<Person> People { get; set; }

        [BsonRequired]
        public bool IsAvailable { get; set; }

        [BsonRequired]
        public string State { get; set; }

        [BsonRequired]
        public string City { get; set; }

    }
}
