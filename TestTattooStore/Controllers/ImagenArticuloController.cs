using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTattooStore.Data;
using TestTattooStore.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestTattooStore.Controllers
{
    [ApiController]
        [Route("[controller]")]
    public class ImagenArticuloController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        public ImagenArticuloController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("buscarUrlXID")]
        public dynamic buscarUrlXID(int idImagenArticulo)
        {
            try
            {
                IEnumerable<ImagenArticulo> objImagenList = _db.ImagenesArticulos.FromSqlRaw<ImagenArticulo>("EXEC SP_ImagenesArticulos_CRUD 'leer', " + idImagenArticulo);
                return Ok(objImagenList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            
        }

        [HttpPost]
        [Route("guardarImagen")]
        public dynamic guardarImagen(ImagenArticulo objImagen)
        {
            try
            {

                var parametros = new[]
                {
            new SqlParameter("@ImagenUrl", objImagen.ImagenUrl),
            new SqlParameter("@Descripcion", objImagen.DescripcionCorta),
           
        };

                // Ejecuta el procedimiento almacenado
                string comando = "exec [SP_ImagenesArticulos_CRUD] 'crear', @ImagenUrl  = '"  + objImagen.ImagenUrl.ToString() +"'";
                IEnumerable<ImagenArticulo> objImagenList = _db.ImagenesArticulos.FromSqlRaw<ImagenArticulo>(comando);

                return Ok(objImagenList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
