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
    public class MembresiaDireccionController : Controller
    {
        private msMembresiaClient _clientMsMembresiaDireccion;
        public MembresiaDireccionController(msMembresiaClient clientMsMembresiaDireccion)
        {

            _clientMsMembresiaDireccion = clientMsMembresiaDireccion;

        }

        [HttpGet("MembresiaDireccionGetByIdMembresia/{idMembresia}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDireccionDto>>> MembresiaDireccionGetByIdMembresia(int idMembresia)
        {
            if (idMembresia <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsMembresiaDireccion.MembresiaDireccionGetByIdMembresiaAsync(idMembresia);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("MembresiaDireccionGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDireccionDto>>> MembresiaDireccionGetAll()
        {
            var entidades = await _clientMsMembresiaDireccion.MembresiaDireccionGetAllAsync();
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("MembresiaDireccionGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDireccionDto>>> MembresiaDireccionGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsMembresiaDireccion.MembresiaDireccionGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("MembresiaDireccionSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MembresiaDireccionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<MembresiaDireccionDto>> MembresiaDireccionSave(MembresiaDireccionDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsMembresiaDireccion.MembresiaDireccionSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw;
            }
        }
        [HttpPost("MembresiaDireccionSaveMasive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDireccionDto>>> MembresiaDireccionSaveMasive(List<MembresiaDireccionDto> input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                List<MembresiaDireccionDto> MembresiaDirecciones = new List<MembresiaDireccionDto>();
                foreach (MembresiaDireccionDto MembresiaDireccion in input)
                {
                    MembresiaDireccionDto Membresiadireccion = await _clientMsMembresiaDireccion.MembresiaDireccionSaveAsync(MembresiaDireccion);

                    MembresiaDirecciones.Add(Membresiadireccion);
                }
                return Ok(MembresiaDirecciones);


            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        [HttpPost("MembresiaDireccionInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDireccionDto>>> MembresiaDireccionInsert(MembresiaDireccionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresiaDireccion.MembresiaDireccionInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPut("MembresiaDireccionUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDireccionDto>>> MembresiaDireccionUpdate(MembresiaDireccionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresiaDireccion.MembresiaDireccionUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("MembresiaDireccionDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> MembresiaDireccionDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsMembresiaDireccion.MembresiaDireccionInsertAsync(new MembresiaDireccionDto { IdMembresiaDireccion = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
