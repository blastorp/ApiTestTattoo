using System.ComponentModel.DataAnnotations;

namespace TestTattooStore.Models
{
    public class ParrafoCT
    {
        [Key]
        public int? IdParrafo { get; set; }

        public int? IdArticulo { get; set; }

        public string? Parrafo { get; set; }
        public int? Posicion { get; set; }
    }
}
