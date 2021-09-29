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
    public class TipoCategoriaController : Controller
    {
        private msTipoClient _clientMsTipo;
       // private msTransaccionClient _clientMsTransaccion;
        public TipoCategoriaController(msTipoClient clientMsTipo /*, msTransaccionClient clientMsTransaccion*/)
        {

            _clientMsTipo= clientMsTipo;
           // _clientMsTransaccion = clientMsTransaccion;

        }

        [HttpGet("TipoCategoriaGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCategoriaDto>>> TipoCategoriaGetAll()
        {
            try
            {
                var entidades = await _clientMsTipo.TipoCategoriaGetAllAsync();
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpGet("TipoCategoriaGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCategoriaDto>>> TipoCategoriaGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsTipo.TipoCategoriaGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("TipoCategoriaGetByIdUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCategoriaDto>>> TipoCategoriaGetByIdUsuario(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsTipo.TipoPagoGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);

        }



        [HttpPost("TipoCategoriaSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCategoriaDto>>> TipoCategoriaSave(TipoCategoriaDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsTipo.TipoCategoriaSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpPost("TipoCategoriaInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCategoriaDto>>> TipoCategoriaInsert(TipoCategoriaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTipo.TipoCategoriaInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("TipoCategoriaUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoCategoriaDto>>> TipoCategoriaUpdate(TipoCategoriaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTipo.TipoCategoriaUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("TipoCategoriaDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> TipoCategoriaDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsTipo.TipoCategoriaInsertAsync(new TipoCategoriaDto { IdTipoCategoria = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
