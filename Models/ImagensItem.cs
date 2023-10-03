using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aluguel.Models
{
    public class ImagensItem
    {
        [Key]
        public int id { get; set; }
        public string codimagem { get; set; }
        public string format { get; set; }


    }
}
