using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTattooStore.Data;
using TestTattooStore.Models;

namespace TestTattooStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HijosArticuloCTController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public HijosArticuloCTController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("listarHijosXIdArticuloCT")]
        public dynamic listarHijosXIdArticuloCT(int idArticulo)
        {
            try
            {
                string comandoDB = "EXEC sp_HijosArticuloCT_Crud 'ListarXArticulo', @IdArticulo =" + idArticulo;
                IEnumerable<HijosArticuloCT> hijosArticuloList = _db.HijosArticuloCT.FromSqlRaw<HijosArticuloCT>(comandoDB);
                return hijosArticuloList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("guardarHijosArticuloCT")]
        public IActionResult GuardarHijosArticuloCT([FromBody] List<HijosArticuloCT> hijosArticulos)
        {
            try
            {
                foreach (var hijoArticulo in hijosArticulos)
                {
                    var parametros = new[]
                    {
               
                new SqlParameter("@IdArticulo", hijoArticulo.IdArticulo ?? (object)DBNull.Value),
                new SqlParameter("@Tipo", hijoArticulo.Tipo ?? (object)DBNull.Value),
                new SqlParameter("@Contenido", hijoArticulo.Contenido ?? (object)DBNull.Value),
                new SqlParameter("@Posicion", hijoArticulo.Posicion ?? (object)DBNull.Value)
            };

                    _db.Database.ExecuteSqlRaw(
                        "EXEC sp_HijosArticuloCT_Crud 'Crear',null, @IdArticulo, @Tipo, @Contenido, @Posicion",
                        parametros);
                }

                return Ok(new { message = "Hijos guardados correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al guardar los hijos: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("eliminarOneHijoArticuloCT")]
        public dynamic eliminarOneHijoArticuloCT(int idHijo)
        {
            try
            {
                var comando = $"EXEC sp_HijosArticuloCT_Crud 'EliminarOne', @IdHijo = {idHijo}";
                _db.Database.ExecuteSqlRaw(comando);
                return Ok(new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("eliminarHijosArticuloCT")]
        public IActionResult EliminarHijosArticuloCT(int idArticulo)
        {
            try
            {

                var comando=$"EXEC sp_HijosArticuloCT_Crud 'EliminarAll', @IdArticulo = {idArticulo}";
                _db.Database.ExecuteSqlRaw(comando);

                return Ok(new { message = "Todos los hijos asociados al artículo fueron eliminados correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al eliminar los hijos: " + ex.Message });
            }
        }

    }
}
