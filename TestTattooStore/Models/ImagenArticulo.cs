using System.ComponentModel.DataAnnotations;

namespace TestTattooStore.Models
   
{
    public class ImagenArticulo
    {
        [Key]
        public int? IdImagenArticulo { get; set; }
        public byte[]? Imagen { get; set; }
        public string? ImagenUrl { get; set; }
        public string? DescripcionCorta { get; set; }
    }
}
