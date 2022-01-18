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
    public class MembresiaServicioDisponibleController : Controller
    {
        private msMembresiaClient _clientMsMembresia;
        private readonly IMemoryCache _memoryCache;
        public MembresiaServicioDisponibleController(msMembresiaClient clientMsMembresia, IMemoryCache memoryCache)
        {
            _clientMsMembresia = clientMsMembresia;
            _memoryCache = memoryCache;
        }

        [HttpGet("MembresiaServicioDisponibleGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleDto>>> MembresiaServicioDisponibleGetAll()
        {
            //var entidades = await _clientMsMembresia.MembresiaServicioDisponibleGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("MembresiaServicioDisponibleGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsMembresia.MembresiaServicioDisponibleGetAllAsync();
               });

            if (entidades == null) return NotFound();
            return Ok(entidades);
        }
        [HttpGet("MembresiaServicioDisponibleGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleDto>>> MembresiaServicioDisponibleGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsMembresia.MembresiaServicioDisponibleGetAsync(id);
            var entidad = await
               _memoryCache.GetOrCreateAsync("MembresiaServicioDisponibleGetAsync"+id.ToString(), entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsMembresia.MembresiaServicioDisponibleGetAsync(id);
               });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPost("MembresiaServicioDisponibleSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleDto>>> MembresiaServicioDisponible(MembresiaServicioDisponibleDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioDisponibleSaveAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);

        }

        [HttpPost("MembresiaServicioDisponibleSaveMasive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleDto>>> MembresiaServicioDisponibleMasive(List<MembresiaServicioDisponibleDto> input)
        {
            if (input == null) return BadRequest(input);
            List<MembresiaServicioDisponibleDto> serviciosDisponiblees = new List<MembresiaServicioDisponibleDto>();
            foreach (MembresiaServicioDisponibleDto membresiaServicioDisponible in input)
            {
                MembresiaServicioDisponibleDto servicioDisponible = await _clientMsMembresia.MembresiaServicioDisponibleSaveAsync(membresiaServicioDisponible);
                serviciosDisponiblees.Add(servicioDisponible);
            }
            return Ok(serviciosDisponiblees);
  

        }


        [HttpPost("MembresiaServicioDisponibleInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleDto>>> MembresiaServicioDisponibleInsert(MembresiaServicioDisponibleDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioDisponibleInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("MembresiaServicioDisponibleUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleDto>>> MembresiaServicioDisponibleUpdate(MembresiaServicioDisponibleDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioDisponibleUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpDelete("MembresiaServicioDisponibleDelete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> MembresiaServicioDisponibleDelete(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            await _clientMsMembresia.MembresiaServicioDisponibleDeleteAsync(id);
            return NoContent();

        }
    }
}
