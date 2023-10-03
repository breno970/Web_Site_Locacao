using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aluguel.Models
{
    public class Proposta
    {
        [Key]
        public int id { get; set; }
        public int multiplicador { get; set; }
        public float vltotal { get; set; }
        public string snAceitoProposta { get; set; }

        //AspNetUsers
        public string idlocatario { get; set; }
        public string idlocador { get; set; }
    }
        
}
