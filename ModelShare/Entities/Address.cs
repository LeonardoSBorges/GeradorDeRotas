using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelShare.Entities
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        [NotMapped]
        public string CityState => $"{City} - {State}";

        public Address()
        {

        }

        public Address(string id, string state, string city)
        {
            Id = id;
            State = state;
            City = city;
        }
    }
}
