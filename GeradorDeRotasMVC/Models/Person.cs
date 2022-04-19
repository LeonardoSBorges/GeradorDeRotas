using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeRotasMVC.Models
{
    public class Person
    {
        
        public string Id { get; set; }
        [Required(ErrorMessage ="Campo obrigadotorio")]
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
