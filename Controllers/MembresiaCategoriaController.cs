using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apiSupplier.Interceptor;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiSupplier.Entities;
using ProblemDetails = apiSupplier.Entities.ProblemDetails;
using NotFoundResult = apiSupplier.Entities.NotFoundResult;

namespace apiHome.Controllers
{
    [TypeFilter(typeof(InterceptorLogAttribute))]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class MembresiaCategoriaController : Controller
    {
        private msMembresiaClient _clientMsMembresiaCategoria;
        public MembresiaCategoriaController(msMembresiaClient clientMsMembresiaCategoria)
        {

            _clientMsMembresiaCategoria = clientMsMembresiaCategoria;

        }

        [HttpGet("MembresiaCategoriaGetByIdMembresia/{idMembresia}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaCategoriaDto>>> MembresiaCategoriaGetByIdMembresia(int idMembresia)
        {
            if (idMembresia <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsMembresiaCategoria.MembresiaCategoriaGetByIdMembresiaAsync(idMembresia);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("MembresiaCategoriaGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaCategoriaDto>>> MembresiaCategoriaGetAll()
        {
            var entidades = await _clientMsMembresiaCategoria.MembresiaCategoriaGetAllAsync();
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("MembresiaCategoriaGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaCategoriaDto>>> MembresiaCategoriaGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsMembresiaCategoria.MembresiaCategoriaGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("MembresiaCategoriaSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MembresiaCategoriaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<MembresiaCategoriaDto>> MembresiaCategoriaSave(MembresiaCategoriaDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsMembresiaCategoria.MembresiaCategoriaSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw;
            }
        }

        [HttpPost("MembresiaCategoriaInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaCategoriaDto>>> MembresiaCategoriaInsert(MembresiaCategoriaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresiaCategoria.MembresiaCategoriaInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPut("MembresiaCategoriaUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaCategoriaDto>>> MembresiaCategoriaUpdate(MembresiaCategoriaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresiaCategoria.MembresiaCategoriaUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("MembresiaCategoriaDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> MembresiaCategoriaDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsMembresiaCategoria.MembresiaCategoriaInsertAsync(new MembresiaCategoriaDto { IdMembresiaCategoria = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
