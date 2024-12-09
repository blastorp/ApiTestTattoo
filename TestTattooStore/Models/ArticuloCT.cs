using System.ComponentModel.DataAnnotations;

namespace TestTattooStore.Models
{
    public class ArticuloCT
    {
        [Key]
        public int? IdArticulo { get; set; }

        public string? TituloPrincipal { get; set; }

        public string? TituloCorto { get; set; }

        public int? IdImagenArticulo { get; set; }

        public string? DescripcionIntro { get; set; }

        public bool? EstadoLogico { get; set; }

        public bool? Publicado { get; set; }

        public bool? Archivado { get; set; }
        public int? Likes { get; set; }
    }
}


