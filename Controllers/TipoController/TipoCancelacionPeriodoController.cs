using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apiSupplier.Interceptor;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiSupplier.Entities;
using ProblemDetails = apiSupplier.Entities.ProblemDetails;
using NotFoundResult = apiSupplier.Entities.NotFoundResult;
using System;

namespace apiSupplier.Controllers
{
    [TypeFilter(typeof(InterceptorLogAttribute))]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class TipoCancelacionPeriodoController : Controller
    {
        private msTipoClient _clientMsTipo;
       // private msTransaccionClient _clientMsTransaccion;
        public TipoCancelacionPeriodoController(msTipoClient clientMsTipo /*, msTransaccionClient clientMsTransaccion*/)
        {

            _clientMsTipo= clientMsTipo;
           // _clientMsTransaccion = clientMsTransaccion;

        }

        [HttpGet("TipoCancelacionPeriodoGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCancelacionPeriodoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCancelacionPeriodoDto>>> TipoCancelacionPeriodoGetAll()
        {
            try
            {
                var entidades = await _clientMsTipo.TipoCancelacionPeriodoGetAllAsync();
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpGet("TipoCancelacionPeriodoGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCancelacionPeriodoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCancelacionPeriodoDto>>> TipoCancelacionPeriodoGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsTipo.TipoCancelacionPeriodoGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("TipoCancelacionPeriodoGetByIdUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCancelacionPeriodoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCancelacionPeriodoDto>>> TipoCancelacionPeriodoGetByIdUsuario(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsTipo.TipoPagoGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);

        }



        [HttpPost("TipoCancelacionPeriodoSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCancelacionPeriodoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCancelacionPeriodoDto>>> TipoCancelacionPeriodoSave(TipoCancelacionPeriodoDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsTipo.TipoCancelacionPeriodoSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpPost("TipoCancelacionPeriodoInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCancelacionPeriodoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCancelacionPeriodoDto>>> TipoCancelacionPeriodoInsert(TipoCancelacionPeriodoDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTipo.TipoCancelacionPeriodoInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("TipoCancelacionPeriodoUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCancelacionPeriodoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCancelacionPeriodoDto>>> TipoCancelacionPeriodoUpdate(TipoCancelacionPeriodoDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTipo.TipoCancelacionPeriodoUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("TipoCancelacionPeriodoDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> TipoCancelacionPeriodoDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsTipo.TipoCancelacionPeriodoInsertAsync(new TipoCancelacionPeriodoDto { IdTipoCancelacionPeriodo = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
