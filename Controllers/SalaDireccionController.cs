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
    public class SalaDireccionController : Controller
    {
        private msSalaClient _clientMsSala;
        private readonly IMemoryCache _memoryCache;
        public SalaDireccionController(msSalaClient clientMsSala, IMemoryCache memoryCache)
        {
            _clientMsSala = clientMsSala;
            _memoryCache = memoryCache;
        }
        [HttpGet("SalaDireccionGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDireccionDto>>> SalaDireccionGetAll()
        {
            //var entidades = await _clientMsSala.SalaGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("SalaDireccionGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSala.SalaDireccionGetAllAsync(); 
               });
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("SalaDireccionGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDireccionDto>>> SalaDireccionGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await
             _memoryCache.GetOrCreateAsync("SalaDireccionGetAsync" + id.ToString(), entry =>
             {
                 entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                 entry.Priority = CacheItemPriority.Normal;
                 return _clientMsSala.SalaDireccionGetAsync(id);
             });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("SalaDireccionSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDireccionDto>>> SalaDireccionSave(SalaDireccionDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsSala.SalaDireccionSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        [HttpPost("SalaDireccionSaveMasive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDireccionDto>>> SalaDireccionSaveMasive(List<SalaDireccionDto> input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                List<SalaDireccionDto> salaDirecciones = new List<SalaDireccionDto>();
                foreach (SalaDireccionDto salaDireccion in input)
                {
                    SalaDireccionDto saladireccion = await _clientMsSala.SalaDireccionSaveAsync(salaDireccion);

                    salaDirecciones.Add(saladireccion);
                }
                return Ok(salaDirecciones);

   
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        [HttpPost("SalaDireccionInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDireccionDto>>> SalaDireccionInsert(SalaDireccionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaDireccionInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("SalaDireccionUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDireccionDto>>> SalaDireccionUpdate(SalaDireccionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaDireccionUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("SalaForDireccionGetByIdProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DireccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<DireccionDto>>> SalaForDireccionGetByIdProveedor(int idProveedor)
        {
            try
            {
                var direcicones = await _clientMsSala.SalaForDireccionGetByIdProveedorAsync(idProveedor);
                if (direcicones == null) return NotFound();
                return Ok(direcicones);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
