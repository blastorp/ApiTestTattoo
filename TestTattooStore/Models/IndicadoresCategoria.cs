using System.ComponentModel.DataAnnotations;

namespace TestTattooStore.Models
{
    public class IndicadoresCategoria
    {
        [Key]
        public int? IdCategoria { get; set; }

        public int? CantidadArtistas { get; set; }

        public int? CantidadTattosGaleria { get; set; }


    }
}
