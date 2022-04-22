using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeRotasMVC.Models
{
    public class Teams
    {
        
        public string Id { get; set; }

        [Display(Name = "Nome do Time")]
        [Required(ErrorMessage = "Preencha o campo corretamente")]
        [MinLength(4, ErrorMessage = "Insira no minimo 4 caracteres"), MaxLength(50, ErrorMessage = "Pode ser inserido no maximo 50 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Pessoas")]
        [Required(ErrorMessage = "E necessario ter ao menos 1 membro em um time")]
        public virtual List<Person> People { get; set; }

        [Display(Name = "Time Disponivel")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Preencha o campo corretamente")]
        public string State { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Preencha o campo corretamente")]
        public string City { get; set; }

        public Teams()
        {

        }

        public Teams(string name, string state, string city)
        {
            Name = name;
            State = state;
            City = city;
        }
    }
}
