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
        public DbSet<CategoriaTattoo> CategoriasTattoo { get; set; }
        public DbSet<ArtistaJoinCategoriaTattoo> ArtistasJoinCategoriasTatto { get; set; }

    }
}
