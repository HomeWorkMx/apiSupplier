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
    public class BusquedaController : Controller
    {
        private msSearchClient _clientMsBusqueda;
        private readonly IMemoryCache _memoryCache;
        public BusquedaController(msSearchClient clientMsBusqueda, IMemoryCache memoryCache)
        {
            _clientMsBusqueda = clientMsBusqueda;
            _memoryCache = memoryCache;
        }

        [HttpGet("BusquedaLike/{busqueda}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BusquedaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<BusquedaDto>>> BusquedaLike(string busqueda, string filtro)
        {
            try
            {
                if (busqueda == string.Empty) return BadRequest(ModelState);
               // var entidad = await _clientMsBusqueda.BusquedaPostLikeAsync(busqueda,filtro);
                var entidad = await
                  _memoryCache.GetOrCreateAsync("BusquedaPostLikeAsync"+busqueda+filtro, entry =>
                  {
                      entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                      entry.Priority = CacheItemPriority.Normal;
                      return _clientMsBusqueda.BusquedaPostLikeAsync(busqueda, filtro);
                  });
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        [HttpGet("ReloadSearch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> ReloadSearch()
        {
            try
            {
                var item = _clientMsBusqueda.ReloadSearchAsync("parametro");
               // if (item <= 1) return NotFound();
                return NoContent();
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }


    }
}
