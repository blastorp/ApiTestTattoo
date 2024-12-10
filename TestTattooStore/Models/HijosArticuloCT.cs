using System.ComponentModel.DataAnnotations;
namespace TestTattooStore.Models

{
    public class HijosArticuloCT
    {
        [Key]
        public int? IdHijo { get; set; }
        public int? IdArticulo { get; set; }
        public string? Tipo { get; set; }
        public string? Contenido { get; set; }
        public int? Posicion { get; set; }
    }
}
