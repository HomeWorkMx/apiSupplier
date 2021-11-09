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
    public class SalaServicioAdicionalProveedorController : Controller
    {
        private msSalaClient _clientMsSala;
        private readonly IMemoryCache _memoryCache;
        public SalaServicioAdicionalProveedorController(msSalaClient clientMsSala, IMemoryCache memoryCache)
        {
            _clientMsSala = clientMsSala;
            _memoryCache = memoryCache;
        }
        [HttpGet("SalaServicioAdicionalProveedorGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalProveedorDto>>> SalaServicioAdicionalProveedorGetAll()
        {
            //var entidades = await _clientMsSala.SalaGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("SalaServicioAdicionalProveedorGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSala.SalaServicioAdicionalProveedorGetAllAsync(); 
               });
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("SalaServicioAdicionalProveedorGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalProveedorDto>>> SalaServicioAdicionalProveedorGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsSalaServicioAdicionalProveedor.SalaServicioAdicionalProveedorGetAsync(id);
            var entidad = await
             _memoryCache.GetOrCreateAsync("SalaServicioAdicionalProveedorGetAsync"+id.ToString(), entry =>
             {
                 entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                 entry.Priority = CacheItemPriority.Normal;
                 return _clientMsSala.SalaServicioAdicionalProveedorGetAsync(id);
             });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("SalaServicioAdicionalProveedorSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalProveedorDto>>> SalaServicioAdicionalProveedorSave(SalaServicioAdicionalProveedorDto input)
        {
            try
            {

                if (input == null) return BadRequest(input);
                var entidad = await _clientMsSala.SalaServicioAdicionalProveedorSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        [HttpPost("SalaServicioAdicionalProveedorSaveMasive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalProveedorDto>>> SalaServicioAdicionalProveedorSaveMasive(List<SalaServicioAdicionalProveedorDto> input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                List<SalaServicioAdicionalProveedorDto> serviciosAdicionales = new List<SalaServicioAdicionalProveedorDto>();
                foreach (SalaServicioAdicionalProveedorDto SalaServicioAdicionalProveedor in input)
                {
                    SalaServicioAdicionalProveedorDto servicioAdicional = await _clientMsSala.SalaServicioAdicionalProveedorSaveAsync(SalaServicioAdicionalProveedor);
                    serviciosAdicionales.Add(servicioAdicional);
                }
                return Ok(serviciosAdicionales);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("SalaServicioAdicionalProveedorInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalProveedorDto>>> SalaServicioAdicionalProveedorInsert(SalaServicioAdicionalProveedorDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaServicioAdicionalProveedorInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("SalaServicioAdicionalProveedorUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioAdicionalProveedorDto>>> SalaServicioAdicionalProveedorUpdate(SalaServicioAdicionalProveedorDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaServicioAdicionalProveedorUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }


    }
}
