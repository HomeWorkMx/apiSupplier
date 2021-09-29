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
    public class SearchController : Controller
    {
        private msSearchClient _clientMsSearch;
        private readonly IMemoryCache _memoryCache;
        public SearchController(msSearchClient clientMsSearch,IMemoryCache memoryCache)
        {
            _clientMsSearch = clientMsSearch;
            _memoryCache = memoryCache;
        }
        [HttpGet("SearchGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SearchDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SearchDto>>> SearchGetAll()
        {
            try
            {
                //var entidades = await _clientMsSearch.SearchGetAllAsync();
                var entidades = await
                   _memoryCache.GetOrCreateAsync("SearchGetAllAsync", entry =>
                   {
                       entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                       entry.Priority = CacheItemPriority.Normal;
                       return _clientMsSearch.SearchGetAllAsync();
                   });
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        [HttpGet("SearchGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SearchDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SearchDto>>> SearchGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsSearch.SearchGetAsync(id);
            var entidad = await
               _memoryCache.GetOrCreateAsync("SearchGetAsync"+ id.ToString(), entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSearch.SearchGetAsync(id);
               });
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPost("SearchSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SearchDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SearchDto>>> SearchSave(SearchDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsSearch.SearchSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        [HttpPost("SearchInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SearchDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SearchDto>>> SearchInsert(SearchDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSearch.SearchInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("SearchUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SearchDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SearchDto>>> SearchUpdate(SearchDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSearch.SearchUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpDelete("SearchDelete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> SearchDelete(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsSearch.SearchInsertAsync(new SearchDto { Id = id });
            return NoContent();
        }

    }
}
