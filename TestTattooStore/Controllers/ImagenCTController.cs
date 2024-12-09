using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTattooStore.Data;
using TestTattooStore.Models;

namespace TestTattooStore.Controllers
{
    public class ImagenCTController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ImagenCTController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("listarImagenes")]
        public dynamic listarImagenes()
        {
            try
            {
                IEnumerable<ImagenCT> objImagenLista = _db.ImagenCuidadoTattoo.FromSqlRaw<ImagenCT>("EXEC SP_ImagenCT_CRUD 'leer'");
                return objImagenLista.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getOneImagenById")]
        public dynamic getOneImagenById(int idImagenCuidadoTatto)
        {
            try
            {
                string comando = "EXEC SP_ImagenCT_CRUD 'leer', @IdImagenCuidadoTatto=" + idImagenCuidadoTatto.ToString();
                IEnumerable<ImagenCT> objImagenLista = _db.ImagenCuidadoTattoo.FromSqlRaw<ImagenCT>(comando);
                return objImagenLista.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("guardarImagen")]
        public dynamic guardarImagen(ImagenCT objImagen)
        {
            try
            {
                var parametros = new[]
                {
                new SqlParameter("@IdImagenCuidadoTatto", objImagen.IdImagenCuidadoTatto ?? (object)DBNull.Value),
                new SqlParameter("@IdArticulo", objImagen.IdArticulo ?? (object)DBNull.Value),
                new SqlParameter("@IdImagenArticulo", objImagen.IdImagenArticulo ?? (object)DBNull.Value),
                new SqlParameter("@Posicion", objImagen.Posicion ?? (object)DBNull.Value)
            };

                IEnumerable<ImagenCT> objImagenList = _db.ImagenCuidadoTattoo.FromSqlRaw<ImagenCT>("EXEC SP_ImagenCT_CRUD 'crear', @IdImagenCuidadoTatto, @IdArticulo, @IdImagenArticulo, @Posicion", parametros);
                return Ok(objImagenList.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("editarImagen")]
        public dynamic editarImagen(ImagenCT objImagen)
        {
            try
            {
                var parametros = new[]
                {
                new SqlParameter("@IdImagenCuidadoTatto", objImagen.IdImagenCuidadoTatto ?? (object)DBNull.Value),
                new SqlParameter("@IdArticulo", objImagen.IdArticulo ?? (object)DBNull.Value),
                new SqlParameter("@IdImagenArticulo", objImagen.IdImagenArticulo ?? (object)DBNull.Value),
                new SqlParameter("@Posicion", objImagen.Posicion ?? (object)DBNull.Value)
            };

                IEnumerable<ImagenCT> objImagenList = _db.ImagenCuidadoTattoo.FromSqlRaw<ImagenCT>("EXEC SP_ImagenCT_CRUD 'actualizar', @IdImagenCuidadoTatto, @IdArticulo, @IdImagenArticulo, @Posicion", parametros);
                return Ok(objImagenList.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteImagen")]
        public dynamic deleteImagen(int idImagenCuidadoTatto)
        {
            try
            {
                string comando = "EXEC SP_ImagenCT_CRUD 'delete', @IdImagenCuidadoTatto";
                IEnumerable<ImagenCT> objImagenList = _db.ImagenCuidadoTattoo.FromSqlRaw<ImagenCT>(comando, new SqlParameter("@IdImagenCuidadoTatto", idImagenCuidadoTatto));
                return objImagenList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listarImagenesPorArticulo")]
        public dynamic listarImagenesPorArticulo(int idArticulo)
        {
            try
            {
                string comando = "EXEC SP_ImagenCT_CRUD 'listarXArticulo', @IdArticulo=" + idArticulo.ToString();
                IEnumerable<ImagenCT> objImagenLista = _db.ImagenCuidadoTattoo.FromSqlRaw<ImagenCT>(comando);
                return objImagenLista.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
