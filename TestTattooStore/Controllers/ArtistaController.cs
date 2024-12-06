using TestTattooStore.Data;
using TestTattooStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace TestTattooStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ArtistaController(ApplicationDbContext db)
        {
            _db = db;

        }

        [HttpGet]
        [Route("listarPlanes")]

        public dynamic listarArtistas()
        {
            try
            {
                IEnumerable<Artista> objArtistaList = _db.Artistas.FromSqlRaw<Artista>("exec sp_artistas_crud 'Listar'");
                return objArtistaList.ToList();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getOneArtistaById")]

        public dynamic getOneArtistaById(int idArtista)
        {
            try
            {
                string comandoDaBa = " exec sp_artistas_crud 'leer', @IdArtista=" + idArtista.ToString();
                IEnumerable<Artista> objArtistaList = _db.Artistas.FromSqlRaw<Artista>(comandoDaBa);
                return objArtistaList.ToList();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("guardarArtista")]

        public dynamic guardarArtista(Artista objArtista)
        {

            try
            {
                // Setting default values for non-nullable fields if necessary
                objArtista.EstadoLogico = true;
                objArtista.Publicado = false;
                objArtista.Archivado = false;

                // Define SQL parameters

                var parametros = new[]
                {
                new SqlParameter("@IdArtista", objArtista.IdArtista ?? (object)DBNull.Value),
                new SqlParameter("@Nombre", objArtista.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@NombreArt", objArtista.NombreArt ?? (object)DBNull.Value),
                new SqlParameter("@NroIdentificacion", objArtista.NroIdentificacion ?? (object)DBNull.Value),
                new SqlParameter("@DescripcionArt", objArtista.DescripcionArt ?? (object)DBNull.Value),
                new SqlParameter("@IdImagenFotoPerfil", objArtista.IdImagenFotoPerfil ?? (object)DBNull.Value),
                new SqlParameter("@Telefono", objArtista.Telefono ?? (object)DBNull.Value),
                new SqlParameter("@Email", objArtista.Email ?? (object)DBNull.Value),
                new SqlParameter("@EstadoLogico", objArtista.EstadoLogico ?? (object)DBNull.Value),
                new SqlParameter("@Publicado", objArtista.Publicado ?? (object)DBNull.Value),
                new SqlParameter("@Archivado", objArtista.Archivado ?? (object)DBNull.Value)
            };



                // Execute stored procedure
              //  _db.Database.ExecuteSqlRaw("EXEC sp_artistas_crud 'Crear', @IdArtista, @Nombre, @NombreArt, @NroIdentificacion, @DescripcionArt, @IdImagenFotoPerfil, @Telefono, @Email, @EstadoLogico, @Publicado, @Archivado", parametros);
                IEnumerable<Artista> objArtistaList = _db.Artistas.FromSqlRaw<Artista>("EXEC sp_artistas_crud 'Crear', @IdArtista, @Nombre, @NombreArt, @NroIdentificacion, @DescripcionArt, @IdImagenFotoPerfil, @Telefono, @Email, @EstadoLogico, @Publicado, @Archivado", parametros);
                return objArtistaList.ToList();
                
            }
            catch (Exception ex)
            {
                // Log the error in the response

                return BadRequest(ex.Message);
            }



        }

        [HttpDelete]
        [Route("deleteArtista")]

        public dynamic deleteArtista(int idArtista) {
            try
            {
                string comandoDaBa = " exec sp_artistas_crud 'eliminar' , @IdArtista =" + idArtista.ToString();
                IEnumerable<Artista> objArtistaList = _db.Artistas.FromSqlRaw<Artista>(comandoDaBa);
                return objArtistaList.ToList();

            }
            catch (Exception ex )
            {


                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("archivarArtista")]

        public dynamic archivarArtista(int idArtista)
        {
            try
            {
                string comandoDaBa = " exec sp_artistas_crud 'archivar' , @IdArtista = " + idArtista.ToString();
                _db.Database.ExecuteSqlRaw(comandoDaBa);
                return Ok(new { message=( "Artista Archivado") });

            }
            catch (Exception ex)
            {


                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("publicarArtista")]

        public dynamic publicarArtista(int idArtista)
        {
            try
            {
                string comandoDaBa = " exec sp_artistas_crud 'publicar' , @IdArtista =" + idArtista.ToString();
                IEnumerable<Artista> objArtistaList = _db.Artistas.FromSqlRaw<Artista>(comandoDaBa);
                return objArtistaList.ToList();

            }
            catch (Exception ex)
            {


                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("despublicarArtista")]

        public dynamic despublicarArtista(int idArtista)
        {
            try
            {
                string comandoDaBa = " exec sp_artistas_crud 'despublicar' , @IdArtista =" + idArtista.ToString();
                IEnumerable<Artista> objArtistaList = _db.Artistas.FromSqlRaw<Artista>(comandoDaBa);
                return objArtistaList.ToList();

            }
            catch (Exception ex)
            {


                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("editarArtista")]

        public dynamic editarArtista(Artista objArtista)
        {

            try
            {
                // Setting default values for non-nullable fields if necessary
                objArtista.EstadoLogico = true;
                objArtista.Publicado = false;
                objArtista.Archivado = false;

                // Define SQL parameters

                var parametros = new[]
                {
                new SqlParameter("@IdArtista", objArtista.IdArtista ?? (object)DBNull.Value),
                new SqlParameter("@Nombre", objArtista.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@NombreArt", objArtista.NombreArt ?? (object)DBNull.Value),
                new SqlParameter("@NroIdentificacion", objArtista.NroIdentificacion ?? (object)DBNull.Value),
                new SqlParameter("@DescripcionArt", objArtista.DescripcionArt ?? (object)DBNull.Value),
                new SqlParameter("@IdImagenFotoPerfil", objArtista.IdImagenFotoPerfil ?? (object)DBNull.Value),
                new SqlParameter("@Telefono", objArtista.Telefono ?? (object)DBNull.Value),
                new SqlParameter("@Email", objArtista.Email ?? (object)DBNull.Value),
                new SqlParameter("@EstadoLogico", objArtista.EstadoLogico ?? (object)DBNull.Value),
                new SqlParameter("@Publicado", objArtista.Publicado ?? (object)DBNull.Value),
                new SqlParameter("@Archivado", objArtista.Archivado ?? (object)DBNull.Value)
            };



                // Execute stored procedure
                //  _db.Database.ExecuteSqlRaw("EXEC sp_artistas_crud 'Crear', @IdArtista, @Nombre, @NombreArt, @NroIdentificacion, @DescripcionArt, @IdImagenFotoPerfil, @Telefono, @Email, @EstadoLogico, @Publicado, @Archivado", parametros);
                IEnumerable<Artista> objArtistaList = _db.Artistas.FromSqlRaw<Artista>("EXEC sp_artistas_crud 'Actualizar', @IdArtista, @Nombre, @NombreArt, @NroIdentificacion, @DescripcionArt, @IdImagenFotoPerfil, @Telefono, @Email, @EstadoLogico, @Publicado, @Archivado", parametros);
                return objArtistaList.ToList();

            }
            catch (Exception ex)
            {
                // Log the error in the response

                return BadRequest(ex.Message);
            }



        }

    }
}
