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
                IEnumerable<Artista> objArtistaList = _db.Artistas.FromSqlRaw<Artista>("exec sp_Artistas_CRUD 'Listar'");
                return objArtistaList.ToList();

            }
            catch (Exception e)
            {

                return e.Message;
            }

        }

        [HttpGet]
        [Route("getOneArtistaById")]

        public dynamic getOneArtistaById(int idArtista)
        {
            try
            {
                string comandoDaBa = " exec sp_Artistas_CRUD 'leer', @IdArtista=" + idArtista.ToString();
                IEnumerable<Artista> objArtistaList = _db.Artistas.FromSqlRaw<Artista>(comandoDaBa);
                return objArtistaList.ToList();

            }
            catch (Exception e)
            {

                return e.Message;
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
                _db.Database.ExecuteSqlRaw("EXEC sp_Artistas_CRUD 'Crear', @IdArtista, @Nombre, @NombreArt, @NroIdentificacion, @DescripcionArt, @IdImagenFotoPerfil, @Telefono, @Email, @EstadoLogico, @Publicado, @Archivado", parametros);

                return "Exito";
            }
            catch (Exception e)
            {
                // Log the error in the response

                return e.Message;
            }



        }

        [HttpDelete]
        [Route("deleteArtista")]

        public dynamic deleteArtista(int idArtista) {
            try
            {
                string comandoDaBa = " exec sp_Artistas_CRUD 'eliminar'" + idArtista.ToString();
                IEnumerable<Artista> objArtistaList = _db.Artistas.FromSqlRaw<Artista>(comandoDaBa);
                return objArtistaList.ToList();

            }
            catch (Exception e )
            {


                return e.Message;
            }
        }

    }
}
