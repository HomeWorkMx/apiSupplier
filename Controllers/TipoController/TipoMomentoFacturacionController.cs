using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apiSupplier.Interceptor;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiSupplier.Entities;
using ProblemDetails = apiSupplier.Entities.ProblemDetails;
using NotFoundResult = apiSupplier.Entities.NotFoundResult;

namespace apiSupplier.Controllers
{
    [TypeFilter(typeof(InterceptorLogAttribute))]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class TipoMomentoFacturacionController : Controller
    {
        private msTipoClient _clientMsTipo;
       // private msTransaccionClient _clientMsTransaccion;
        public TipoMomentoFacturacionController(msTipoClient clientMsTipo /*, msTransaccionClient clientMsTransaccion*/)
        {

            _clientMsTipo= clientMsTipo;
           // _clientMsTransaccion = clientMsTransaccion;

        }

        [HttpGet("TipoMomentoFacturacionGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoMomentoFacturacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoMomentoFacturacionDto>>> TipoMomentoFacturacionGetAll()
        {
            try
            {
                var entidades = await _clientMsTipo.TipoMomentoFacturacionGetAllAsync();
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpGet("TipoMomentoFacturacionGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoMomentoFacturacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoMomentoFacturacionDto>>> TipoMomentoFacturacionGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsTipo.TipoMomentoFacturacionGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("TipoMomentoFacturacionGetByIdUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoMomentoFacturacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoMomentoFacturacionDto>>> TipoMomentoFacturacionGetByIdUsuario(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsTipo.TipoPagoGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);

        }



        [HttpPost("TipoMomentoFacturacionSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoMomentoFacturacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoMomentoFacturacionDto>>> TipoMomentoFacturacionSave(TipoMomentoFacturacionDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsTipo.TipoMomentoFacturacionSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpPost("TipoMomentoFacturacionInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoMomentoFacturacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoMomentoFacturacionDto>>> TipoMomentoFacturacionInsert(TipoMomentoFacturacionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTipo.TipoMomentoFacturacionInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("TipoMomentoFacturacionUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoMomentoFacturacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoMomentoFacturacionDto>>> TipoMomentoFacturacionUpdate(TipoMomentoFacturacionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTipo.TipoMomentoFacturacionUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("TipoMomentoFacturacionDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> TipoMomentoFacturacionDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsTipo.TipoMomentoFacturacionInsertAsync(new TipoMomentoFacturacionDto { IdTipoMomentoFacturacion = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
