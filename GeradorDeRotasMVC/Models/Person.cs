using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace GeradorDeRotasMVC.Models
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Name { get; set; }

        public Person()
        {

        }
        public Person(string name)
        {
            Name = name;
        }
    }
}
