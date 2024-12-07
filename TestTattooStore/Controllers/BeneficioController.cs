using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTattooStore.Data;
using TestTattooStore.Models;

namespace TestTattooStore.Controllers
{
    [ApiController]
        [Route("[controller]")]

    public class BeneficioController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BeneficioController(ApplicationDbContext db)
        {
            _db = db;

        }

        [HttpGet]
        [Route("listarBeneficios")]

        public dynamic listarBeneficios()
        {
            try
            {
                IEnumerable<Beneficio> objBeneficioLista = _db.Beneficios.FromSqlRaw<Beneficio>("exec SP_Beneficios 'Listar'");
                return objBeneficioLista.ToList();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getOneBeneficioById")]
        
        public dynamic getOneBeneficioById(int idBeneficio)
        {
            try
            {
                string comandoDaBa = " exec SP_Beneficios 'leer', @IdBeneficio=" + idBeneficio.ToString();
                IEnumerable<Beneficio> objBeneficioLista = _db.Beneficios.FromSqlRaw<Beneficio>(comandoDaBa);
                return objBeneficioLista.ToList();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }



        [HttpPost]
        [Route("guardarBeneficio")]

        public dynamic guardarBeneficio(Beneficio objBeneficio)
        {

            try
            {
                // Setting default values for non-nullable fields if necessary
                objBeneficio.EstadoLogico = true;
                objBeneficio.Publicado = false;
                objBeneficio.Archivado = false;

                // Define SQL parameters

                var parametros = new[]
                {
                new SqlParameter("@IdBeneficio", objBeneficio.IdBeneficio ?? (object)DBNull.Value),
                new SqlParameter("@Nombre", objBeneficio.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@Subtitulo", objBeneficio.Subtitulo ?? (object)DBNull.Value),
                new SqlParameter("@Descripcion", objBeneficio.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@IdImagenArticulo", objBeneficio.IdImagenArticulo ?? (object)DBNull.Value),
                new SqlParameter("@CantVisitas", objBeneficio.CantVisitas ?? (object)DBNull.Value),
                new SqlParameter("@EstadoLogico", objBeneficio.EstadoLogico ?? (object)DBNull.Value),
                new SqlParameter("@Publicado", objBeneficio.Publicado ?? (object)DBNull.Value),
                new SqlParameter("@Archivado", objBeneficio.Archivado ?? (object)DBNull.Value)
            };

                // Execute stored procedure
                //devuelve un objeto tipo beneficio
                //  _db.Database.ExecuteSqlRaw("EXEC sp_artistas_crud 'Crear', @IdArtista, @Nombre, @NombreArt, @NroIdentificacion, @DescripcionArt, @IdImagenFotoPerfil, @Telefono, @Email, @EstadoLogico, @Publicado, @Archivado", parametros);
                IEnumerable<Beneficio> objBeneficioList = _db.Beneficios.FromSqlRaw<Beneficio>("EXEC SP_Beneficios 'Crear', @IdBeneficio, @Nombre, @Subtitulo, @Descripcion, @IdImagenArticulo, @CantVisitas, @EstadoLogico, @Publicado, @Archivado", parametros);
                return Ok(objBeneficioList.ToList());

            }
            catch (Exception ex)
            {
                // Log the error in the response

                return BadRequest(ex.Message);
            }

        }
        
        [HttpPost]
        [Route("editarBeneficio")]

        public dynamic editarBeneficio(Beneficio objBeneficio)
        {

            try
            {
                // Setting default values for non-nullable fields if necessary
                objBeneficio.EstadoLogico = true;
                objBeneficio.Publicado = false;
                objBeneficio.Archivado = false;

                // Define SQL parameters

                var parametros = new[]
                {
                new SqlParameter("@IdBeneficio", objBeneficio.IdBeneficio ?? (object)DBNull.Value),
                new SqlParameter("@Nombre", objBeneficio.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@Subtitulo", objBeneficio.Subtitulo ?? (object)DBNull.Value),
                new SqlParameter("@Descripcion", objBeneficio.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@IdImagenArticulo", objBeneficio.IdImagenArticulo ?? (object)DBNull.Value),
                new SqlParameter("@CantVisitas", objBeneficio.CantVisitas ?? (object)DBNull.Value),
                new SqlParameter("@EstadoLogico", objBeneficio.EstadoLogico ?? (object)DBNull.Value),
                new SqlParameter("@Publicado", objBeneficio.Publicado ?? (object)DBNull.Value),
                new SqlParameter("@Archivado", objBeneficio.Archivado ?? (object)DBNull.Value)
            };

                // Execute stored procedure
                //devuelve un objeto tipo beneficio
                //  _db.Database.ExecuteSqlRaw("EXEC sp_artistas_crud 'Crear', @IdArtista, @Nombre, @NombreArt, @NroIdentificacion, @DescripcionArt, @IdImagenFotoPerfil, @Telefono, @Email, @EstadoLogico, @Publicado, @Archivado", parametros);
                IEnumerable<Beneficio> objBeneficioList = _db.Beneficios.FromSqlRaw<Beneficio>("EXEC SP_Beneficios 'actualizar', @IdBeneficio, @Nombre, @Subtitulo, @Descripcion, @IdImagenArticulo, @CantVisitas, @EstadoLogico, @Publicado, @Archivado", parametros);
                return Ok(objBeneficioList.ToList());

            }
            catch (Exception ex)
            {
                // Log the error in the response

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("deleteBeneficio")]
        public dynamic deleteBeneficio(int idBeneficio)
        {
            try
            {
                string comando = "exec SP_Beneficios 'eliminar', @idBeneficio";
                IEnumerable<Beneficio> objBeneficioList = _db.Beneficios.FromSqlRaw<Beneficio>(comando, new SqlParameter("@idBeneficio", idBeneficio));
                return objBeneficioList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("archivarBeneficio")]
        public dynamic archivarBeneficio(int idBeneficio)
        {
            try
            {
                string comando = "exec SP_Beneficios 'archivar', @idBeneficio";
                _db.Database.ExecuteSqlRaw(comando, new SqlParameter("@idBeneficio", idBeneficio));
                return Ok(new { message = ("Beneficio Archivado") });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("publicarBeneficio")]
        public dynamic PublicarBeneficio(int idBeneficio)
        {
            try
            {
                string comandoDaBa = "exec SP_Beneficios 'publicar', @IdBeneficio = " + idBeneficio.ToString();
                IEnumerable<Beneficio> objBeneficioList = _db.Beneficios.FromSqlRaw<Beneficio>(comandoDaBa);
                return objBeneficioList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("despublicarBeneficio")]
        public dynamic DespublicarBeneficio(int idBeneficio)
        {
            try
            {
                string comandoDaBa = "exec SP_Beneficios 'despublicar', @IdBeneficio = " + idBeneficio.ToString();
                IEnumerable<Beneficio> objBeneficioList = _db.Beneficios.FromSqlRaw<Beneficio>(comandoDaBa);
                return objBeneficioList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
