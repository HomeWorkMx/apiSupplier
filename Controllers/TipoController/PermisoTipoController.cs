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
    public class PermisoTipoController : Controller
    {
        private msTipoClient _clientMsTipo;
       // private msTransaccionClient _clientMsTransaccion;
        public PermisoTipoController(msTipoClient clientMsTipo /*, msTransaccionClient clientMsTransaccion*/)
        {

            _clientMsTipo= clientMsTipo;
           // _clientMsTransaccion = clientMsTransaccion;

        }

        [HttpGet("PermisoTipoGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PermisoTipoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PermisoTipoDto>>> PermisoTipoGetAll()
        {
            try
            {
                var entidades = await _clientMsTipo.PermisoTipoGetAllAsync();
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpGet("PermisoTipoGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PermisoTipoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PermisoTipoDto>>> PermisoTipoGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsTipo.PermisoTipoGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("PermisoTipoGetByIdUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PermisoTipoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PermisoTipoDto>>> PermisoTipoGetByIdUsuario(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsTipo.TipoPagoGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);

        }

      

        [HttpPost("PermisoTipoSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PermisoTipoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PermisoTipoDto>>> PermisoTipoSave(PermisoTipoDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsTipo.PermisoTipoSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpPost("PermisoTipoInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PermisoTipoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PermisoTipoDto>>> PermisoTipoInsert(PermisoTipoDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTipo.PermisoTipoInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("PermisoTipoUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PermisoTipoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PermisoTipoDto>>> PermisoTipoUpdate(PermisoTipoDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTipo.PermisoTipoUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("PermisoTipoDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> PermisoTipoDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsTipo.PermisoTipoInsertAsync(new PermisoTipoDto { IdPermisoTipo = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
