using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTattooStore.Data;
using TestTattooStore.Models;

namespace TestTattooStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticuloCTController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ArticuloCTController(ApplicationDbContext db)
        {
            _db = db;

        }
       

            [HttpGet]
            [Route("listarArticulos")]
            public dynamic ListarArticulos()
            {
                try
                {
                    IEnumerable<ArticuloCT> articulos = _db.ArticuloCuidadoTattoo.FromSqlRaw<ArticuloCT>("exec sp_articulocuidatattos_crud 'Listar'");
                    return articulos.ToList();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet]
            [Route("getOneArticuloById")]
            public dynamic GetOneArticuloById(int idArticulo)
            {
                try
                {
                    string query = "exec sp_articulocuidatattos_crud 'Leer', @IdArticulo=" + idArticulo;
                    IEnumerable<ArticuloCT> articulos = _db.ArticuloCuidadoTattoo.FromSqlRaw<ArticuloCT>(query);
                    return articulos.ToList();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPost]
            [Route("guardarArticulo")]
            public dynamic GuardarArticulo(ArticuloCT articulo)
            {
                try
                {
                    articulo.EstadoLogico = true;
                    articulo.Publicado = false;
                    articulo.Archivado = false;

                    var parametros = new[]
                    {
                new SqlParameter("@IdArticulo", articulo.IdArticulo ?? (object)DBNull.Value),
                new SqlParameter("@TituloPrincipal", articulo.TituloPrincipal ?? (object)DBNull.Value),
                new SqlParameter("@TituloCorto", articulo.TituloCorto ?? (object)DBNull.Value),
                new SqlParameter("@IdImagenArticulo", articulo.IdImagenArticulo ?? (object)DBNull.Value),
                new SqlParameter("@DescripcionIntro", articulo.DescripcionIntro ?? (object)DBNull.Value),
                new SqlParameter("@EstadoLogico", articulo.EstadoLogico ?? (object)DBNull.Value),
                new SqlParameter("@Publicado", articulo.Publicado ?? (object)DBNull.Value),
                new SqlParameter("@Archivado", articulo.Archivado ?? (object)DBNull.Value)
            };

                    IEnumerable<ArticuloCT> articulos = _db.ArticuloCuidadoTattoo.FromSqlRaw<ArticuloCT>("EXEC sp_articulocuidatattos_crud 'Crear', @IdArticulo, @TituloPrincipal, @TituloCorto, @IdImagenArticulo, @DescripcionIntro, @EstadoLogico, @Publicado, @Archivado", parametros);
                    return Ok(articulos.ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPost]
            [Route("editarArticulo")]
            public dynamic EditarArticulo(ArticuloCT articulo)
            {
                try
                {
                    articulo.EstadoLogico = true;
                    articulo.Publicado = false;
                    articulo.Archivado = false;

                    var parametros = new[]
                    {
                new SqlParameter("@IdArticulo", articulo.IdArticulo ?? (object)DBNull.Value),
                new SqlParameter("@TituloPrincipal", articulo.TituloPrincipal ?? (object)DBNull.Value),
                new SqlParameter("@TituloCorto", articulo.TituloCorto ?? (object)DBNull.Value),
                new SqlParameter("@IdImagenArticulo", articulo.IdImagenArticulo ?? (object)DBNull.Value),
                new SqlParameter("@DescripcionIntro", articulo.DescripcionIntro ?? (object)DBNull.Value),
                new SqlParameter("@EstadoLogico", articulo.EstadoLogico ?? (object)DBNull.Value),
                new SqlParameter("@Publicado", articulo.Publicado ?? (object)DBNull.Value),
                new SqlParameter("@Archivado", articulo.Archivado ?? (object)DBNull.Value)
            };

                    IEnumerable<ArticuloCT> articulos = _db.ArticuloCuidadoTattoo.FromSqlRaw<ArticuloCT>("EXEC sp_articulocuidatattos_crud 'Actualizar', @IdArticulo, @TituloPrincipal, @TituloCorto, @IdImagenArticulo, @DescripcionIntro, @EstadoLogico, @Publicado, @Archivado", parametros);
                    return Ok(articulos.ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet]
            [Route("deleteArticulo")]
            public dynamic DeleteArticulo(int idArticulo)
            {
                try
                {
                    string query = "exec sp_articulocuidatattos_crud 'Eliminar', @IdArticulo";
                    IEnumerable<ArticuloCT> articulos = _db.ArticuloCuidadoTattoo.FromSqlRaw<ArticuloCT>(query, new SqlParameter("@IdArticulo", idArticulo));
                    return articulos.ToList();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet]
            [Route("archivarArticulo")]
            public dynamic ArchivarArticulo(int idArticulo)
            {
                try
                {
                    string query = "exec sp_articulocuidatattos_crud 'Archivar', @IdArticulo";
                    _db.Database.ExecuteSqlRaw(query, new SqlParameter("@IdArticulo", idArticulo));
                    return Ok(new { message = "Artículo Archivado" });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet]
            [Route("publicarArticulo")]
            public dynamic PublicarArticulo(int idArticulo)
            {
                try
                {
                    string query = "exec sp_articulocuidatattos_crud 'Publicar', @IdArticulo=" + idArticulo;
                    IEnumerable<ArticuloCT> articulos = _db.ArticuloCuidadoTattoo.FromSqlRaw<ArticuloCT>(query);
                    return articulos.ToList();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet]
            [Route("despublicarArticulo")]
            public dynamic DespublicarArticulo(int idArticulo)
            {
                try
                {
                    string query = "exec sp_articulocuidatattos_crud 'Despublicar', @IdArticulo=" + idArticulo;
                    IEnumerable<ArticuloCT> articulos = _db.ArticuloCuidadoTattoo.FromSqlRaw<ArticuloCT>(query);
                    return articulos.ToList();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


        [HttpGet]
        [Route("likeArticulo")]
        public dynamic likeArticulo(int idArticulo)
        {
            try
            {
                string query = "exec sp_articulocuidatattos_crud 'like', @IdArticulo=" + idArticulo;
                IEnumerable<ArticuloCT> articulos = _db.ArticuloCuidadoTattoo.FromSqlRaw<ArticuloCT>(query);
                return articulos.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("dislikeArticulo")]
        public dynamic dislikeArticulo(int idArticulo)
        {
            try
            {
                string query = "exec sp_articulocuidatattos_crud 'unlike', @IdArticulo=" + idArticulo;
                IEnumerable<ArticuloCT> articulos = _db.ArticuloCuidadoTattoo.FromSqlRaw<ArticuloCT>(query);
                return articulos.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

