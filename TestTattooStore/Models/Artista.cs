
using System.ComponentModel.DataAnnotations;
namespace TestTattooStore.Models

{
    public class Artista
    {
        [Key]
        public int? IdArtista { get; set; }
        public string? Nombre { get; set; }
        public string? NombreArt { get; set; }
        public string? NroIdentificacion { get; set; }
        public string? DescripcionArt { get; set; }
        public int? IdImagenFotoPerfil { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool? EstadoLogico { get; set; }
        public bool? Publicado { get; set; }
        public bool? Archivado { get; set; }

        public Artista() { }
    }
}