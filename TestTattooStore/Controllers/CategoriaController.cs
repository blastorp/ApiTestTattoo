using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTattooStore.Data;
using TestTattooStore.Models;

namespace TestTattooStore.Controllers
{
    [ApiController]
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
        [Route("categoriaXId")]

        public dynamic categoriaXId(int idCategoria)
        {
            try
            {
                string comandoSql = "exec sp_categorias_crud 'leer', @IdCategoria =" + idCategoria;
                IEnumerable<Categoria> objCategoriaLista = _db.Categorias.FromSqlRaw<Categoria>(comandoSql);
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

       
        [HttpPost]
        [Route("guardarCategoria")]
        public dynamic GuardarCategoria(Categoria objCategoria)
        {
            try
            {
                // Configurar valores predeterminados para campos no nulos si es necesario
                objCategoria.EstadoLogico = true;
                objCategoria.Publicado = false;
                objCategoria.Archivado = false;

                // Definir los parámetros SQL
                var parametros = new[]
                {
            new SqlParameter("@IdCategoria", objCategoria.IdCategoria ??  (object)DBNull.Value),
            new SqlParameter("@IdCategoriaPadre", objCategoria.IdCategoriaPadre ?? (object)DBNull.Value),
            new SqlParameter("@Nombre", objCategoria.Nombre ?? (object)DBNull.Value),
            new SqlParameter("@DescripcionCategoria", objCategoria.DescripcionCategoria ?? (object)DBNull.Value),
            new SqlParameter("@IdImagenArticulo", objCategoria.IdImagenArticulo ?? (object)DBNull.Value),
            new SqlParameter("@EstadoLogico", objCategoria.EstadoLogico ?? (object)DBNull.Value),
            new SqlParameter("@Publicado", objCategoria.Publicado ?? (object)DBNull.Value),
            new SqlParameter("@Archivado", objCategoria.Archivado ?? (object)DBNull.Value)
             };

                // Ejecutar procedimiento almacenado
                IEnumerable<Categoria> objCategoriaList = _db.Categorias.FromSqlRaw<Categoria>(
                    "EXEC sp_categorias_crud 'Crear', @IdCategoria, @IdCategoriaPadre, @Nombre, @DescripcionCategoria, @IdImagenArticulo, @EstadoLogico, @Publicado, @Archivado",
                    parametros);

                return objCategoriaList.ToList();
            }
            catch (Exception ex)
            {
                // Registrar el error en la respuesta
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("deleteCategoria")]
        public dynamic DeleteCategoria(int idCategoria)
        {
            try
            {
                string comandoDaBa = $"exec sp_categorias_crud 'Eliminar', @IdCategoria = {idCategoria}";
                IEnumerable<Categoria> objCategoriaList = _db.Categorias.FromSqlRaw<Categoria>(comandoDaBa);
                return objCategoriaList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("archivarCategoria")]
        public dynamic ArchivarCategoria(int idCategoria)
        {
            try
            {
                string comandoDaBa = $"exec sp_categorias_crud 'Archivar', @IdCategoria = {idCategoria}";
                _db.Database.ExecuteSqlRaw(comandoDaBa);
                return Ok(new { message = "Categoría Archivada" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("publicarCategoria")]
        public dynamic PublicarCategoria(int idCategoria)
        {
            try
            {
                string comandoDaBa = $"exec sp_categorias_crud 'Publicar', @IdCategoria = {idCategoria}";
                IEnumerable<Categoria> objCategoriaList = _db.Categorias.FromSqlRaw<Categoria>(comandoDaBa);
                return objCategoriaList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("despublicarCategoria")]
        public dynamic DespublicarCategoria(int idCategoria)
        {
            try
            {
                string comandoDaBa = $"exec sp_categorias_crud 'despublicar', @IdCategoria = {idCategoria}";
                IEnumerable<Categoria> objCategoriaList = _db.Categorias.FromSqlRaw<Categoria>(comandoDaBa);
                return objCategoriaList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        [Route("indicadoresCategoria")]
        public dynamic indicadoresCategoria(int idCategoria)
        {
            try
            {
                string comandoDaBa = $"exec sp_categorias_crud 'indicadores', @IdCategoria = {idCategoria}";
                IEnumerable<IndicadoresCategoria> objCategoriaList = _db.CantidadXCategoria.FromSqlRaw<IndicadoresCategoria>(comandoDaBa);
                return objCategoriaList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("editarCategoria")]
        public dynamic EditarCategoria(Categoria objCategoria)
        {
            try
            {
                // Configurar valores predeterminados para campos no nulos si es necesario
                objCategoria.EstadoLogico = true;
                objCategoria.Publicado = false;
                objCategoria.Archivado = false;

                // Definir los parámetros SQL
                var parametros = new[]
                {
            new SqlParameter("@IdCategoria", objCategoria.IdCategoria ??   (object)DBNull.Value),
            new SqlParameter("@IdCategoriaPadre", objCategoria.IdCategoriaPadre ?? (object)DBNull.Value),
            new SqlParameter("@Nombre", objCategoria.Nombre ?? (object)DBNull.Value),
            new SqlParameter("@DescripcionCategoria", objCategoria.DescripcionCategoria ?? (object)DBNull.Value),
            new SqlParameter("@IdImagenArticulo", objCategoria.IdImagenArticulo ?? (object)DBNull.Value),
            new SqlParameter("@EstadoLogico", objCategoria.EstadoLogico ?? (object)DBNull.Value),
            new SqlParameter("@Publicado", objCategoria.Publicado ?? (object)DBNull.Value),
            new SqlParameter("@Archivado", objCategoria.Archivado ?? (object)DBNull.Value)
        };

                // Ejecutar procedimiento almacenado
                IEnumerable<Categoria> objCategoriaList = _db.Categorias.FromSqlRaw<Categoria>(
                    "EXEC sp_categorias_crud 'Actualizar', @IdCategoria, @IdCategoriaPadre, @Nombre, @DescripcionCategoria, @IdImagenArticulo, @EstadoLogico, @Publicado, @Archivado",
                    parametros);

                return objCategoriaList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
