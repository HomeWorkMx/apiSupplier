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
    public class SalaController : Controller
    {
        private msSalaClient _clientMsSala;
        private readonly IMemoryCache _memoryCache;
        public SalaController(msSalaClient clientMsSala, IMemoryCache memoryCache)
        {
            _clientMsSala = clientMsSala;
            _memoryCache = memoryCache;
        }
        [HttpGet("SalaGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDto>>> SalaGetAll()
        {
            var entidades = await _clientMsSala.SalaGetAllAsync();
            /*var entidades = await
                _memoryCache.GetOrCreateAsync("SalaGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSala.SalaGetAllAsync(); 
               });*/
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("SalaGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDto>>> SalaGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsSala.SalaGetAsync(id);
           /* var entidad = await
             _memoryCache.GetOrCreateAsync("SalaGetAsync"+id.ToString(), entry =>
             {
                 entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                 entry.Priority = CacheItemPriority.Normal;
                 return _clientMsSala.SalaGetAsync(id);
             });*/

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("SalaSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDto>>> SalaSave(SalaDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsSala.SalaSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }
        [HttpPost("SalaInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDto>>> SalaInsert(SalaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("SalaUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDto>>> SalaUpdate(SalaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }


    }
}
