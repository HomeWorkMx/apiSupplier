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
    public class SalaServicioDisponibleController : Controller
    {
        private msSalaClient _clientMsSala;
        private readonly IMemoryCache _memoryCache;
        public SalaServicioDisponibleController(msSalaClient clientMsSala, IMemoryCache memoryCache)
        {
            _clientMsSala = clientMsSala;
            _memoryCache = memoryCache;
        }

        [HttpGet("SalaServicioDisponibleGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleDto>>> SalaServicioDisponibleGetAll()
        {
            //var entidades = await _clientMsSala.SalaServicioDisponibleGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("SalaServicioDisponibleGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSala.SalaServicioDisponibleGetAllAsync();
               });

            if (entidades == null) return NotFound();
            return Ok(entidades);
        }
        [HttpGet("SalaServicioDisponibleGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleDto>>> SalaServicioDisponibleGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsSala.SalaServicioDisponibleGetAsync(id);
            var entidad = await
               _memoryCache.GetOrCreateAsync("SalaServicioDisponibleGetAsync"+id.ToString(), entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSala.SalaServicioDisponibleGetAsync(id);
               });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPost("SalaServicioDisponibleSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleDto>>> SalaServicioDisponible(SalaServicioDisponibleDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaServicioDisponibleSaveAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);

        }

        [HttpPost("SalaServicioDisponibleSaveMasive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleDto>>> SalaServicioDisponibleMasive(List<SalaServicioDisponibleDto> input)
        {
            if (input == null) return BadRequest(input);
            List<SalaServicioDisponibleDto> serviciosDisponiblees = new List<SalaServicioDisponibleDto>();
            foreach (SalaServicioDisponibleDto SalaServicioDisponible in input)
            {
                SalaServicioDisponibleDto servicioDisponible = await _clientMsSala.SalaServicioDisponibleSaveAsync(SalaServicioDisponible);
                serviciosDisponiblees.Add(servicioDisponible);
            }
            return Ok(serviciosDisponiblees);
  

        }


        [HttpPost("SalaServicioDisponibleInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleDto>>> SalaServicioDisponibleInsert(SalaServicioDisponibleDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaServicioDisponibleInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("SalaServicioDisponibleUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleDto>>> SalaServicioDisponibleUpdate(SalaServicioDisponibleDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaServicioDisponibleUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpDelete("SalaServicioDisponibleDelete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> SalaServicioDisponibleDelete(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            await _clientMsSala.SalaServicioDisponibleDeleteAsync(id);
            return NoContent();

        }
    }
}
