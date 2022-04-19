using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ModelShare.Entities
{
    public class Person
    {
        public Person()
        {

        }
        public Person(string id,string name, bool haveTeam)
        {
            Id = id;
            Name = name;
            HaveTeam = haveTeam;
        }


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public bool HaveTeam { get; set; }
       
    }
}
