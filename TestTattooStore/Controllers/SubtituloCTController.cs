using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTattooStore.Data;
using TestTattooStore.Models;

namespace TestTattooStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubtituloCTController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SubtituloCTController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("listarSubtitulos")]
        public dynamic listarSubtitulos()
        {
            try
            {
                IEnumerable<SubtituloCT> objSubtituloLista = _db.SubtitulosCuidadoTattoo.FromSqlRaw<SubtituloCT>("EXEC SP_SubtitulosCT_CRUD 'leer'");
                return objSubtituloLista.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getOneSubtituloById")]
        public dynamic getOneSubtituloById(int idSubtitulo)
        {
            try
            {
                string comando = "EXEC SP_SubtitulosCT_CRUD 'leer', @IdSubtitulo=" + idSubtitulo.ToString();
                IEnumerable<SubtituloCT> objSubtituloLista = _db.SubtitulosCuidadoTattoo.FromSqlRaw<SubtituloCT>(comando);
                return objSubtituloLista.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("guardarSubtitulo")]
        public dynamic guardarSubtitulo(SubtituloCT objSubtitulo)
        {
            try
            {
                var parametros = new[]
                {
                new SqlParameter("@IdSubtitulo", objSubtitulo.IdSubtitulo ?? (object)DBNull.Value),
                new SqlParameter("@IdArticulo", objSubtitulo.IdArticulo ?? (object)DBNull.Value),
                new SqlParameter("@TextoSubtitulo", objSubtitulo.TextoSubtitulo ?? (object)DBNull.Value),
                new SqlParameter("@Posicion", objSubtitulo.Posicion ?? (object)DBNull.Value)
            };

                IEnumerable<SubtituloCT> objSubtituloList = _db.SubtitulosCuidadoTattoo.FromSqlRaw<SubtituloCT>("EXEC SP_SubtitulosCT_CRUD 'crear', @IdSubtitulo, @IdArticulo, @TextoSubtitulo, @Posicion", parametros);
                return Ok(objSubtituloList.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("editarSubtitulo")]
        public dynamic editarSubtitulo(SubtituloCT objSubtitulo)
        {
            try
            {
                var parametros = new[]
                {
                new SqlParameter("@IdSubtitulo", objSubtitulo.IdSubtitulo ?? (object)DBNull.Value),
                new SqlParameter("@IdArticulo", objSubtitulo.IdArticulo ?? (object)DBNull.Value),
                new SqlParameter("@TextoSubtitulo", objSubtitulo.TextoSubtitulo ?? (object)DBNull.Value),
                new SqlParameter("@Posicion", objSubtitulo.Posicion ?? (object)DBNull.Value)
            };

                IEnumerable<SubtituloCT> objSubtituloList = _db.SubtitulosCuidadoTattoo.FromSqlRaw<SubtituloCT>("EXEC SP_SubtitulosCT_CRUD 'actualizar', @IdSubtitulo, @IdArticulo, @TextoSubtitulo, @Posicion", parametros);
                return Ok(objSubtituloList.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteSubtitulo")]
        public dynamic deleteSubtitulo(int idSubtitulo)
        {
            try
            {
                string comando = "EXEC SP_SubtitulosCT_CRUD 'delete', @IdSubtitulo";
                IEnumerable<SubtituloCT> objSubtituloList = _db.SubtitulosCuidadoTattoo.FromSqlRaw<SubtituloCT>(comando, new SqlParameter("@IdSubtitulo", idSubtitulo));
                return objSubtituloList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listarSubtitulosPorArticulo")]
        public dynamic listarSubtitulosPorArticulo(int idArticulo)
        {
            try
            {
                string comando = "EXEC SP_SubtitulosCT_CRUD 'listarXArticulo', @IdArticulo=" + idArticulo.ToString();
                IEnumerable<SubtituloCT> objSubtituloLista = _db.SubtitulosCuidadoTattoo.FromSqlRaw<SubtituloCT>(comando);
                return objSubtituloLista.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
