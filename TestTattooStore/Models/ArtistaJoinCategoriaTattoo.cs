using System.ComponentModel.DataAnnotations;

namespace TestTattooStore.Models
{
    public class ArtistaJoinCategoriaTattoo
    {

        [Key]
        public int IdCategoriaArtista { get; set; }
        public int IdCategoria { get; set; } 
        public int IdArtista { get; set; }


        public ArtistaJoinCategoriaTattoo() { }
    }
}
