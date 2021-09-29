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
    public class TipoUsuarioController : Controller
    {
        private msTipoClient _clientMsTipo;
       // private msTransaccionClient _clientMsTransaccion;
        public TipoUsuarioController(msTipoClient clientMsTipo /*, msTransaccionClient clientMsTransaccion*/)
        {

            _clientMsTipo= clientMsTipo;
           // _clientMsTransaccion = clientMsTransaccion;

        }

        [HttpGet("TipoUsuarioGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoUsuarioDto>>> TipoUsuarioGetAll()
        {
            try
            {
                var entidades = await _clientMsTipo.TipoUsuarioGetAllAsync();
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpGet("TipoUsuarioGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoUsuarioDto>>> TipoUsuarioGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsTipo.TipoUsuarioGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("TipoUsuarioGetByIdUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoUsuarioDto>>> TipoUsuarioGetByIdUsuario(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsTipo.TipoPagoGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);

        }

      

        [HttpPost("TipoUsuarioSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoUsuarioDto>>> TipoUsuarioSave(TipoUsuarioDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsTipo.TipoUsuarioSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpPost("TipoUsuarioInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoUsuarioDto>>> TipoUsuarioInsert(TipoUsuarioDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTipo.TipoUsuarioInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("TipoUsuarioUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TipoUsuarioDto>>> TipoUsuarioUpdate(TipoUsuarioDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTipo.TipoUsuarioUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("TipoUsuarioDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> TipoUsuarioDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsTipo.TipoUsuarioInsertAsync(new TipoUsuarioDto { IdTipoUsuario = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
