using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelShare.Entities
{
    public class Teams
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [BsonRequired]
        [JsonProperty("People")]
        public virtual List<Person> People { get; set; }

        [BsonRequired]
        [JsonProperty("IsAvailable")]
        public bool IsAvailable { get; set; }

        [BsonRequired]
        [JsonProperty("Address")]
        public Address Address { get; set; }

    }
}
