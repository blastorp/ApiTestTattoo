using System.ComponentModel.DataAnnotations;

namespace TestTattooStore.Models
{
    public class SubtituloCT
    {
        [Key]
        public int? IdSubtitulo { get; set; }

        public int? IdArticulo { get; set; }

        public string? TextoSubtitulo { get; set; }

        public int? Posicion { get; set; }

    }
}
