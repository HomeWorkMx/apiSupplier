using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apiSupplier.Interceptor;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiSupplier.Entities;
using System.Linq;
using ProblemDetails = apiSupplier.Entities.ProblemDetails;
using NotFoundResult = apiSupplier.Entities.NotFoundResult;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace apiSupplier.Controllers
{
    [TypeFilter(typeof(InterceptorLogAttribute))]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class TransaccionProductosController : Controller
    {
        private msTransaccionClient _clientMsTransaccionProductos;
        private readonly IMemoryCache _memoryCache;
        public TransaccionProductosController(msTransaccionClient clientMsTransaccionProductos, IMemoryCache memoryCache)
        {
            _clientMsTransaccionProductos = clientMsTransaccionProductos;
            _memoryCache = memoryCache;
        }
        [HttpGet("TransaccionProductosGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionProductosDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionProductosDto>>> TransaccionProductosGetAll()
        {
            try
            {
               // var entidades = await _clientMsTransaccionProductos.TransaccionProductosGetAllAsync();
                var entidades = await
                  _memoryCache.GetOrCreateAsync("TransaccionProductosGetAllAsync", entry =>
                  {
                      entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                      entry.Priority = CacheItemPriority.Normal;
                      return _clientMsTransaccionProductos.TransaccionProductosGetAllAsync();
                  });
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("TransaccionProductosGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionProductosDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionProductosDto>>> TransaccionProductosGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsTransaccionProductos.TransaccionProductosGetByIdAsync(id);
            var entidad = await
                  _memoryCache.GetOrCreateAsync("TransaccionProductosGetByIdAsync"+id.ToString(), entry =>
                  {
                      entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                      entry.Priority = CacheItemPriority.Normal;
                      return  _clientMsTransaccionProductos.TransaccionProductosGetByIdAsync(id);
                  });
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpGet("TransaccionProductosGetByIdTransaccion/{IdTransaccion}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionProductosDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionProductosDto>>> TransaccionProductosGetByIdTransaccion(int IdTransaccion)
        {
            if (IdTransaccion <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsTransaccionProductos.TransaccionProductosGetAllAsync();
            var entidad = await
                 _memoryCache.GetOrCreateAsync("TransaccionProductosGetAllAsync", entry =>
                 {
                     entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                     entry.Priority = CacheItemPriority.Normal;
                     return _clientMsTransaccionProductos.TransaccionProductosGetAllAsync();
                 });
            if (entidad == null) return NotFound();
            return Ok(entidad.Where(x => x.IdTransaccion == IdTransaccion));
        }
        [HttpPost("TransaccionProductosSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionProductosDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionProductosDto>>> TransaccionProductosSave(TransaccionProductosDto2 input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsTransaccionProductos.TransaccionProductosSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        [HttpPost("TransaccionProductosInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionProductosDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionProductosDto>>> TransaccionProductosInsert(TransaccionProductosDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTransaccionProductos.TransaccionProductosInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("TransaccionProductosUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionProductosDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionProductosDto>>> TransaccionProductosUpdate(TransaccionProductosDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTransaccionProductos.TransaccionProductosUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("TransaccionProductosDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> TransaccionProductosDelete(int id)
        //{
        //    if (id <= 0) return BadRequest(ModelState);
        //    var entidad = await _clientMsTransaccionProductos.TransaccionProductosInsertAsync(new TransaccionProductosDto { IdTransaccionProductos = id });
        //    return NoContent();
        //} 

    }
}
