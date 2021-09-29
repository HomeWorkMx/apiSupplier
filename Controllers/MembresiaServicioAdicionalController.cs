using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apiSupplier.Interceptor;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiSupplier.Entities;
using ProblemDetails = apiSupplier.Entities.ProblemDetails;
using NotFoundResult = apiSupplier.Entities.NotFoundResult;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace apiSupplier.Controllers
{
    [TypeFilter(typeof(InterceptorLogAttribute))]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class MembresiaServicioAdicionalController : Controller
    {
        private msMembresiaClient _clientMsMembresia;
        private readonly IMemoryCache _memoryCache;
        public MembresiaServicioAdicionalController(msMembresiaClient clientMsMembresia, IMemoryCache memoryCache)
        {
            _clientMsMembresia = clientMsMembresia;
            _memoryCache = memoryCache;
        }

        [HttpGet("MembresiaServicioAdicionalGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalDto>>> MembresiaServicioAdicionalGetAll()
        {
            //var entidades = await _clientMsMembresia.MembresiaServicioAdicionalGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("MembresiaServicioAdicionalGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsMembresia.MembresiaServicioAdicionalGetAllAsync();
               });

            if (entidades == null) return NotFound();
            return Ok(entidades);
        }
        [HttpGet("MembresiaServicioAdicionalGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalDto>>> MembresiaServicioAdicionalGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsMembresia.MembresiaServicioAdicionalGetAsync(id);
            var entidad = await
               _memoryCache.GetOrCreateAsync("MembresiaServicioAdicionalGetAsync"+id.ToString(), entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsMembresia.MembresiaServicioAdicionalGetAsync(id);
               });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPost("MembresiaServicioAdicionalSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalDto>>> MembresiaServicioAdicional(MembresiaServicioAdicionalDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioAdicionalSaveAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);

        }

        [HttpPost("MembresiaServicioAdicionalSaveMasive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalDto>>> MembresiaServicioAdicionalMasive(List<MembresiaServicioAdicionalDto> input)
        {
            if (input == null) return BadRequest(input);
            List<MembresiaServicioAdicionalDto> serviciosAdicionales = new List<MembresiaServicioAdicionalDto>();
            foreach (MembresiaServicioAdicionalDto membresiaServicioAdicional in input)
            {
                MembresiaServicioAdicionalDto servicioAdicional = await _clientMsMembresia.MembresiaServicioAdicionalSaveAsync(membresiaServicioAdicional);
                serviciosAdicionales.Add(servicioAdicional);
            }
            return Ok(serviciosAdicionales);
  

        }


        [HttpPost("MembresiaServicioAdicionalInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalDto>>> MembresiaServicioAdicionalInsert(MembresiaServicioAdicionalDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioAdicionalInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("MembresiaServicioAdicionalUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalDto>>> MembresiaServicioAdicionalUpdate(MembresiaServicioAdicionalDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioAdicionalUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpDelete("MembresiaServicioAdicionalDelete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> MembresiaServicioAdicionalDelete(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            await _clientMsMembresia.MembresiaServicioAdicionalDeleteAsync(id);
            return NoContent();

        }
    }
}
