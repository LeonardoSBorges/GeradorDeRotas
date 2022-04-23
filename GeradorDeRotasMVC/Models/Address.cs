using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeradorDeRotasMVC.Models
{
    public class Address
    {
        public string Id { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Campo Obrigatorio")]

        public string State { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage ="Campo Obrigatorio")]
        public string City { get; set; }

        [NotMapped]
        public string CityState => $"{City} - {State}";
    }
}
