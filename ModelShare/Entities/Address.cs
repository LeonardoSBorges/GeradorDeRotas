using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
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

        public override string ToString()
        {
            return $@"{City.ToUpper()} - {State.ToUpper()} ";
                
        }
    }
}
