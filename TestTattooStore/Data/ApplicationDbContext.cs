﻿using Microsoft.EntityFrameworkCore;
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

    }
}
