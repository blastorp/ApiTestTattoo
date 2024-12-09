using System.ComponentModel.DataAnnotations;

namespace TestTattooStore.Models
{
    public class Categoria
    {
        [Key]
        public int? IdCategoria { get; set; } 
        public int? IdCategoriaPadre { get; set; }
        public string? Nombre { get; set; }
        public string? DescripcionCategoria { get; set; }
        public int? IdImagenArticulo { get; set; }
        public bool? EstadoLogico { get; set; }
        public bool? Publicado { get; set; }
        public bool? Archivado { get; set; }

        public Categoria() { }
    }
}
