using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aluguel.ViewModel
{
    public class ItemViewModel
    {
        [NotMapped]
        public int id { get; set; }
        public string nmitem { get; set; }
        public string dsitem { get; set; }
        public DateTime dtcriacao { get; set; }
        public string snativo { get; set; }
        public IFormFile imagem { get; set; }

        //AspNetUsers
        public string iduser { get; set; }
    }
}
