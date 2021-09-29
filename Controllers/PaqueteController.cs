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
    public class PaqueteController : Controller
    {
        private msPaqueteClient _clientMsPaquete;
        private readonly IMemoryCache _memoryCache;
        public PaqueteController(msPaqueteClient clientMsPaquete, IMemoryCache memoryCache)
        {
            _clientMsPaquete = clientMsPaquete;
            _memoryCache = memoryCache;
        }

        [HttpGet("PaqueteGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaqueteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PaqueteDto>>> PaqueteGetAll()
        {
            try
            {
                //var entidades = await _clientMsPaquete.PaqueteGetAllAsync();
                var entidades = await
                _memoryCache.GetOrCreateAsync("PaqueteGetAllAsync", entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                    entry.Priority = CacheItemPriority.Normal;
                    return _clientMsPaquete.PaqueteGetAllAsync();
                });

                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
        [HttpGet("PaqueteGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaqueteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PaqueteDto>>> PaqueteGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsPaquete.PaqueteGetAsync(id);
            var entidad = await
               _memoryCache.GetOrCreateAsync("PaqueteGetAsync"+id.ToString(), entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsPaquete.PaqueteGetAsync(id);
               });
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("PaqueteSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaqueteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PaqueteDto>>> PaqueteSave(PaqueteDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsPaquete.PaqueteSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
        [HttpPost("PaqueteInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaqueteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PaqueteDto>>> PaqueteInsert(PaqueteDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsPaquete.PaqueteInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("PaqueteUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaqueteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PaqueteDto>>> PaqueteUpdate(PaqueteDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsPaquete.PaqueteUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("PaqueteDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> PaqueteDelete(int id)
        //{
        //    if (id <= 0) return BadRequest(ModelState);
        //    var entidad = await _clientMsPaquete.PaqueteInsertAsync(new PaqueteDto { IdPaquete = id });
        //    return NoContent();
        //}
    }
}
