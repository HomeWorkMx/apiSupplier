using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apiSupplier.Interceptor;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiSupplier.Entities;
using Microsoft.Extensions.Caching.Memory;
using System;
using ProblemDetails = apiSupplier.Entities.ProblemDetails;
using NotFoundResult = apiSupplier.Entities.NotFoundResult;

namespace apiSupplier.Controllers
{
    [TypeFilter(typeof(InterceptorLogAttribute))]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class SalaServicioAdicionalController : Controller
    {
        private msSalaClient _clientMsSala;
        private readonly IMemoryCache _memoryCache;
        public SalaServicioAdicionalController(msSalaClient clientMsSala, IMemoryCache memoryCache)
        {
            _clientMsSala = clientMsSala;
            _memoryCache = memoryCache;
        }
        [HttpGet("SalaServicioAdicionalGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalDto>>> SalaServicioAdicionalGetAll()
        {
            //var entidades = await _clientMsSala.SalaGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("SalaServicioAdicionalGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSala.SalaServicioAdicionalGetAllAsync(); 
               });
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("SalaServicioAdicionalGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalDto>>> SalaServicioAdicionalGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsSalaServicioAdicional.SalaServicioAdicionalGetAsync(id);
            var entidad = await
             _memoryCache.GetOrCreateAsync("SalaServicioAdicionalGetAsync"+id.ToString(), entry =>
             {
                 entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                 entry.Priority = CacheItemPriority.Normal;
                 return _clientMsSala.SalaServicioAdicionalGetByIdAsync(id);
             });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("SalaServicioAdicionalGetAllDistinct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalDto>>> SalaServicioAdicionalGetAllDistinct()
        {
            var entidades = await
               _memoryCache.GetOrCreateAsync("SalaServicioAdicionalGetAllDistinct", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSala.SalaServicioAdicionalGetAllDistinctAsync();
               });
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpPost("SalaServicioAdicionalSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalDto>>> SalaServicioAdicionalSave(SalaServicioAdicionalDto input)
        {
            try
            {

                if (input == null) return BadRequest(input);
                var entidad = await _clientMsSala.SalaServicioAdicionalSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        [HttpPost("SalaServicioAdicionalSaveMasive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalDto>>> SalaServicioAdicionalSaveMasive(List<SalaServicioAdicionalDto> input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                List<SalaServicioAdicionalDto> serviciosAdicionales = new List<SalaServicioAdicionalDto>();
                foreach (SalaServicioAdicionalDto SalaServicioAdicional in input)
                {
                    SalaServicioAdicionalDto servicioAdicional = await _clientMsSala.SalaServicioAdicionalSaveAsync(SalaServicioAdicional);
                    serviciosAdicionales.Add(servicioAdicional);
                }
                return Ok(serviciosAdicionales);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("SalaServicioAdicionalInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalDto>>> SalaServicioAdicionalInsert(SalaServicioAdicionalDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaServicioAdicionalInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("SalaServicioAdicionalUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalDto>>> SalaServicioAdicionalUpdate(SalaServicioAdicionalDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaServicioAdicionalUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }


    }
}
