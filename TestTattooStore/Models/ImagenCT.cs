using System.ComponentModel.DataAnnotations;

namespace TestTattooStore.Models
{
    public class ImagenCT
    {
        [Key]
        public int IdImagenCuidadoTatto { get; set; }

        public int? IdArticulo { get; set; }

        public int? IdImagenArticulo { get; set; }
    }
}
