using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTattooStore.Data;
using TestTattooStore.Models;

namespace TestTattooStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParrafoCTController : Controller
    {

        private readonly ApplicationDbContext _db;



        public ParrafoCTController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("listarParrafos")]
        public dynamic listarParrafos()
        {
            try
            {
                IEnumerable<ParrafoCT> objParrafoLista = _db.ParrafosCuidadoTattoo.FromSqlRaw<ParrafoCT>("EXEC SP_ParrafoCT_CRUD 'leer'");
                return objParrafoLista.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getOneParrafoById")]
        public dynamic getOneParrafoById(int idParrafo)
        {
            try
            {
                string comando = "EXEC SP_ParrafoCT_CRUD 'leer', @IdParrafo=" + idParrafo.ToString();
                IEnumerable<ParrafoCT> objParrafoLista = _db.ParrafosCuidadoTattoo.FromSqlRaw<ParrafoCT>(comando);
                return objParrafoLista.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("guardarParrafo")]
        public dynamic guardarParrafo(ParrafoCT objParrafo)
        {
            try
            {
                var parametros = new[]
                {
                new SqlParameter("@IdParrafo", objParrafo.IdParrafo ?? (object)DBNull.Value),
                new SqlParameter("@IdArticulo", objParrafo.IdArticulo ?? (object)DBNull.Value),
                new SqlParameter("@Parrafo", objParrafo.Parrafo ?? (object)DBNull.Value),
                new SqlParameter("@Posicion", objParrafo.Posicion ?? (object)DBNull.Value)
            };

                IEnumerable<ParrafoCT> objParrafoList = _db.ParrafosCuidadoTattoo.FromSqlRaw<ParrafoCT>("EXEC SP_ParrafoCT_CRUD 'crear', @IdParrafo, @IdArticulo, @Parrafo, @Posicion", parametros);
                return Ok(objParrafoList.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("editarParrafo")]
        public dynamic editarParrafo(ParrafoCT objParrafo)
        {
            try
            {
                var parametros = new[]
                {
                new SqlParameter("@IdParrafo", objParrafo.IdParrafo ?? (object)DBNull.Value),
                new SqlParameter("@IdArticulo", objParrafo.IdArticulo ?? (object)DBNull.Value),
                new SqlParameter("@Parrafo", objParrafo.Parrafo ?? (object)DBNull.Value),
                new SqlParameter("@Posicion", objParrafo.Posicion ?? (object)DBNull.Value)
            };

                IEnumerable<ParrafoCT> objParrafoList = _db.ParrafosCuidadoTattoo.FromSqlRaw<ParrafoCT>("EXEC SP_ParrafoCT_CRUD 'actualizar', @IdParrafo, @IdArticulo, @Parrafo, @Posicion", parametros);
                return Ok(objParrafoList.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteParrafo")]
        public dynamic deleteParrafo(int idParrafo)
        {
            try
            {
                string comando = "EXEC SP_ParrafoCT_CRUD 'delete', @IdParrafo";
                IEnumerable<ParrafoCT> objParrafoList = _db.ParrafosCuidadoTattoo.FromSqlRaw<ParrafoCT>(comando, new SqlParameter("@IdParrafo", idParrafo));
                return objParrafoList.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listarParrafosPorArticulo")]
        public dynamic listarParrafosPorArticulo(int idArticulo)
        {
            try
            {
                string comando = "EXEC SP_ParrafoCT_CRUD 'listarXArticulo', @IdArticulo=" + idArticulo.ToString();
                IEnumerable<ParrafoCT> objParrafoLista = _db.ParrafosCuidadoTattoo.FromSqlRaw<ParrafoCT>(comando);
                return objParrafoLista.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
