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
    public class MembresiaFrecuenciaCobroController : Controller
    {
        private msMembresiaClient _clientMsMembresiaFrecuenciaCobro;
        public MembresiaFrecuenciaCobroController(msMembresiaClient clientMsMembresiaFrecuenciaCobro)
        {

            _clientMsMembresiaFrecuenciaCobro = clientMsMembresiaFrecuenciaCobro;

        }

        [HttpGet("MembresiaFrecuenciaCobroGetByIdMembresia/{idMembresia}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaFrecuenciaCobroDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaFrecuenciaCobroDto>>> MembresiaFrecuenciaCobroGetByIdMembresia(int idMembresia)
        {
            if (idMembresia <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsMembresiaFrecuenciaCobro.MembresiaFrecuenciaCobroGetByIdMembresiaAsync(idMembresia);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("MembresiaFrecuenciaCobroGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaFrecuenciaCobroDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaFrecuenciaCobroDto>>> MembresiaFrecuenciaCobroGetAll()
        {
            var entidades = await _clientMsMembresiaFrecuenciaCobro.MembresiaFrecuenciaCobroGetAllAsync();
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("MembresiaFrecuenciaCobroGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaFrecuenciaCobroDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaFrecuenciaCobroDto>>> MembresiaFrecuenciaCobroGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsMembresiaFrecuenciaCobro.MembresiaFrecuenciaCobroGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("MembresiaFrecuenciaCobroSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MembresiaFrecuenciaCobroDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<MembresiaFrecuenciaCobroDto>> MembresiaFrecuenciaCobroSave(MembresiaFrecuenciaCobroDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsMembresiaFrecuenciaCobro.MembresiaFrecuenciaCobroSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw;
            }
        }

        [HttpPost("MembresiaFrecuenciaCobroInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaFrecuenciaCobroDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaFrecuenciaCobroDto>>> MembresiaFrecuenciaCobroInsert(MembresiaFrecuenciaCobroDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresiaFrecuenciaCobro.MembresiaFrecuenciaCobroInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPut("MembresiaFrecuenciaCobroUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaFrecuenciaCobroDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaFrecuenciaCobroDto>>> MembresiaFrecuenciaCobroUpdate(MembresiaFrecuenciaCobroDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresiaFrecuenciaCobro.MembresiaFrecuenciaCobroUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("MembresiaFrecuenciaCobroDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> MembresiaFrecuenciaCobroDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsMembresiaFrecuenciaCobro.MembresiaFrecuenciaCobroInsertAsync(new MembresiaFrecuenciaCobroDto { IdMembresiaFrecuenciaCobro = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
