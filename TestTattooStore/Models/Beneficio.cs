using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestTattooStore.Models
{
    public class Beneficio
    {

        [Key]
        
        public int ? IdBeneficio { get; set; }

        
        public string? Nombre { get; set; }

        
        public string? Subtitulo { get; set; }

        
        public string? Descripcion { get; set; }

        public int? IdImagenArticulo { get; set; }
        
        

        public int? CantVisitas { get; set; }

        public bool? EstadoLogico { get; set; }
        public bool? Publicado { get; set; }
        public bool? Archivado { get; set; }

    }
}
