using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTattooStore.Data;
using TestTattooStore.Models;

namespace TestTattooStore.Controllers
{[ApiController]
        [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        
        private readonly ApplicationDbContext _db;
        public CategoriaController(ApplicationDbContext db)
        {
            _db = db;

        }

        [HttpGet]
        [Route("listarCategorias")]

        public dynamic listarCategorias()
        {
            try
            {
                IEnumerable<Categoria> objCategoriaLista = _db.Categorias.FromSqlRaw<Categoria>("exec sp_categorias_crud 'listar'");
                return objCategoriaLista.ToList();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("listarCategoriasXArtista")]

        public dynamic listarCategoriasXArtista(int idArtista)
        {
            try
            {
                IEnumerable<Categoria> objCategoriaLista = _db.Categorias.FromSqlRaw<Categoria>("exec sp_ArtistasJoinCategoriasTatto_CRUD 'ListarCategoriaXArtista' , null , null , " + idArtista);
                return objCategoriaLista.ToList();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("listarArtistasXCategoria")]

        public dynamic listarArtistasXCategoria(int idCategoria)
        {
            try
            {
                IEnumerable<Artista> objArtistaLista = _db.Artistas.FromSqlRaw<Artista>("exec sp_ArtistasJoinCategoriasTatto_CRUD 'ListarArtistasXCategoria' , null ," + idCategoria );
                return objArtistaLista.ToList();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("asignarCategoriaArtista")]

        public dynamic asignarCategoriaArtista([FromBody] CategoriaArtista[] categoriasArtista)
        {
            try
            {
                foreach (var categoriaArtista in categoriasArtista)
                {
                    var parametros = new[]
               {
                new SqlParameter("@IdArtista", categoriaArtista.IdArtista ?? (object)DBNull.Value),
                new SqlParameter("@IdCategoria", categoriaArtista.IdCategoria ?? (object)DBNull.Value)

                    };
                    _db.Database.ExecuteSqlRaw("exec sp_ArtistasJoinCategoriasTatto_CRUD 'AsignarCategoriaXArtista' , null, @IdCategoria, @IdArtista", parametros);

                }

                _db.Database.ExecuteSqlRaw("exec sp_ArtistasJoinCategoriasTatto_CRUD 'ResetXArtista' , null, null," + categoriasArtista[0].IdArtista);

                foreach (var categoriaArtista in categoriasArtista)
                {
                    var parametros = new[]
               {
                new SqlParameter("@IdArtista", categoriaArtista.IdArtista ?? (object)DBNull.Value),
                new SqlParameter("@IdCategoria", categoriaArtista.IdCategoria ?? (object)DBNull.Value)

                    };
                    _db.Database.ExecuteSqlRaw("exec sp_ArtistasJoinCategoriasTatto_CRUD 'AsignarCategoriaXArtista' , null, @IdCategoria, @IdArtista", parametros);
                }
                return Ok(new { message = "Categorias asignadas correctamente" });

            }
            catch (Exception e)
            {

                return BadRequest(new { message = "Error al asignar categorias: " + e.Message });
            }

        }
    }
}
