using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeRotasMVC.Models
{
    public class Person
    {
        
        public string Id { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Preencha o campo")]
        [MinLength(4, ErrorMessage = "Insira no minimo 4 caracteres"), MaxLength(50, ErrorMessage = "Pode ser inserido no maximo 50 caracteres")]
        public string Name { get; set; }
        [Display(Name = "Time")]
        public bool HaveTeam { get; set; }

        public Person()
        {

        }
        public Person(string id,string name)
        {
            Id = id;
            Name = name;
        }
        public Person(string name)
        {
            Name = name;
        }
    }
}
