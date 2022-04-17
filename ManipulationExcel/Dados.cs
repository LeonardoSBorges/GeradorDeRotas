using ModelShare.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManipulationExcel
{
    public class Dados
    {
        public int OS { get; set; }
        public string Base { get; set; }
        public string Service { get; set; }
        public Address Address { get; set; }


        public override string ToString()
        {
            return $"{OS}{Base}{Service}{Address.ToString()}";
        }

    }
}
