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
    public class SalaCategoriaController : Controller
    {
        private msSalaClient _clientMsSala;
        private readonly IMemoryCache _memoryCache;
        public SalaCategoriaController(msSalaClient clientMsSala, IMemoryCache memoryCache)
        {
            _clientMsSala = clientMsSala;
            _memoryCache = memoryCache;
        }
        [HttpGet("SalaCategoriaGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaCategoriaDto>>> SalaCategoriaGetAll()
        {
            //var entidades = await _clientMsSala.SalaGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("SalaCategoriaGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSala.SalaCategoriaGetAllAsync(); 
               });
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("SalaCategoriaGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaCategoriaDto>>> SalaCategoriaGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsSalaCategoria.SalaCategoriaGetAsync(id);
            var entidad = await
             _memoryCache.GetOrCreateAsync("SalaCategoriaGetAsync"+id.ToString(), entry =>
             {
                 entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                 entry.Priority = CacheItemPriority.Normal;
                 return _clientMsSala.SalaCategoriaGetAsync(id);
             });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("SalaCategoriaSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaCategoriaDto>>> SalaCategoriaSave(SalaCategoriaDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsSala.SalaCategoriaSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        [HttpPost("SalaCategoriaSaveMasive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaCategoriaDto>>> SalaCategoriaSaveMasive(List<SalaCategoriaDto> input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                List<SalaCategoriaDto> salasCategorias = new List<SalaCategoriaDto>();
                foreach (SalaCategoriaDto SalaCategoria in input)
                {
                    SalaCategoriaDto salaCategoria = await _clientMsSala.SalaCategoriaSaveAsync(SalaCategoria);
                    salasCategorias.Add(salaCategoria);
                }
                return Ok(salasCategorias);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost("SalaCategoriaInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaCategoriaDto>>> SalaCategoriaInsert(SalaCategoriaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaCategoriaInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("SalaCategoriaUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaCategoriaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaCategoriaDto>>> SalaCategoriaUpdate(SalaCategoriaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaCategoriaUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }


    }
}
