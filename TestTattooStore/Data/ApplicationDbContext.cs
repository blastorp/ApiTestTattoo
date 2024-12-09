using Microsoft.EntityFrameworkCore;
using TestTattooStore.Models;

namespace TestTattooStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Beneficio> Beneficios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ArtistaJoinCategoriaTattoo> ArtistasJoinCategoriasTatto { get; set; }
        public DbSet<ImagenArticulo> ImagenesArticulos { get; set; }
        public DbSet<ArticuloCT> ArticuloCuidadoTattoo { get; set; }
        public DbSet<ParrafoCT> ParrafosCuidadoTattoo { get; set; }
        public DbSet<ImagenCT> ImagenCuidadoTattoo { get; set; }
        public DbSet<SubtituloCT> SubtitulosCuidadoTattoo { get; set; }
        public DbSet<IndicadoresCategoria> CantidadXCategoria { get; set; }

    }
}
