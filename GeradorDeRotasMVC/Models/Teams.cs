using System.Collections.Generic;

namespace GeradorDeRotasMVC.Models
{
    public class Teams
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual List<Person> People { get; set; }
        public bool IsAvailable { get; set; }
        public string State { get; set; }
        public string City { get; set; }

    }
}
