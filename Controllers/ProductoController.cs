using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apiSupplier.Interceptor;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiSupplier.Entities;
using ProblemDetails = apiSupplier.Entities.ProblemDetails;
using NotFoundResult = apiSupplier.Entities.NotFoundResult;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace apiSupplier.Controllers
{
    [TypeFilter(typeof(InterceptorLogAttribute))]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ProductoController : Controller
    {
        private msProductoClient _clientMsProducto;
        private readonly ILogger _logger;
        private readonly IMemoryCache _memoryCache;
        public ProductoController(msProductoClient clientMsProducto, IMemoryCache memoryCache)
        {
            _clientMsProducto = clientMsProducto;
            _memoryCache = memoryCache;
        }
        [HttpGet("ProductoGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> ProductoGetAll()
        {
            //var entidades = await _clientMsProducto.ProductoGetAllAsync();
            var entidades = await
                _memoryCache.GetOrCreateAsync("ProductoGetAllAsync", entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                    entry.Priority = CacheItemPriority.Normal;
                    return _clientMsProducto.ProductoGetAllAsync();
                });
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }
        [HttpGet("ProductoGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> ProductoGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsProducto.ProductoGetAsync(id);
            var entidad = await
                _memoryCache.GetOrCreateAsync("ProductoGetAsync"+ id.ToString(), entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                    entry.Priority = CacheItemPriority.Normal;
                    return _clientMsProducto.ProductoGetAsync(id);
                });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("ProductoSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> ProductoSave( ProductoDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsProducto.ProductoSaveAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPost("ProductoInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> ProductoInsert(ProductoDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsProducto.ProductoInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("ProductoUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> ProductoUpdate(ProductoDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsProducto.ProductoUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("ProductoDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> ProductoDelete(int id)
        //{
        //    if (id <= 0) return BadRequest(ModelState);
        //    var entidad = await _clientMsProducto.ProductoInsertAsync(new ProductoDto { IdProducto = id });
        //    return NoContent();
        //}

    }
}
