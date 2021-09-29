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
    public class MembresiaImagenController : Controller
    {
        private msMembresiaClient _clientMsMembresiaImagen;
        public MembresiaImagenController(msMembresiaClient clientMsMembresiaImagen)
        {

            _clientMsMembresiaImagen = clientMsMembresiaImagen;

        }

        [HttpGet("MembresiaImagenGetByIdMembresia/{idMembresia}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaImagenDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaImagenDto>>> MembresiaImagenGetByIdMembresia(int idMembresia)
        {
            if (idMembresia <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsMembresiaImagen.MembresiaImagenGetByIdMembresiaAsync(idMembresia);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("MembresiaImagenGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaImagenDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaImagenDto>>> MembresiaImagenGetAll()
        {
            var entidades = await _clientMsMembresiaImagen.MembresiaImagenGetAllAsync();
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("MembresiaImagenGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaImagenDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaImagenDto>>> MembresiaImagenGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsMembresiaImagen.MembresiaImagenGetAsync(id);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("MembresiaImagenSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaImagenDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaImagenDto>>> MembresiaImagenSave(MembresiaImagenDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsMembresiaImagen.MembresiaImagenSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw;
            }
        }

        [HttpPost("MembresiaImagenInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaImagenDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaImagenDto>>> MembresiaImagenInsert(MembresiaImagenDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresiaImagen.MembresiaImagenInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPut("MembresiaImagenUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaImagenDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaImagenDto>>> MembresiaImagenUpdate(MembresiaImagenDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresiaImagen.MembresiaImagenUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("MembresiaImagenDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> MembresiaImagenDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsMembresiaImagen.MembresiaImagenInsertAsync(new MembresiaImagenDto { IdMembresiaImagen = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
