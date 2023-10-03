using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using aluguel.ViewModel;

namespace aluguel.Models
{
    public class Item
    {
        [Key]
        public int id { get; set; }
        public string nmitem { get; set; }
        public string dsitem { get; set; }
        public DateTime dtcriacao { get; set; }
        public string snativo { get; set; }
        public string imagem { get; set; }

        //AspNetUsers
        public string iduser { get; set; }

        [NotMapped]
        public ItemViewModel ?itemViewModel { get; set; }

        private List<Item> GetItems()
        {
            List<Item> items = new List<Item>();
            return items;
        }
    }

}
