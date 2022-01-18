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
    public class SalaServicioDisponibleProveedorController : Controller
    {
        private msSalaClient _clientMsSala;
        private readonly IMemoryCache _memoryCache;
        public SalaServicioDisponibleProveedorController(msSalaClient clientMsSala, IMemoryCache memoryCache)
        {
            _clientMsSala = clientMsSala;
            _memoryCache = memoryCache;
        }
        [HttpGet("SalaServicioDisponibleProveedorGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleProveedorDto>>> SalaServicioDisponibleProveedorGetAll()
        {
            //var entidades = await _clientMsSala.SalaGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("SalaServicioDisponibleProveedorGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSala.SalaServicioDisponibleProveedorGetAllAsync(); 
               });
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("SalaServicioDisponibleProveedorGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleProveedorDto>>> SalaServicioDisponibleProveedorGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsSalaServicioDisponibleProveedor.SalaServicioDisponibleProveedorGetAsync(id);
            var entidad = await
             _memoryCache.GetOrCreateAsync("SalaServicioDisponibleProveedorGetAsync"+id.ToString(), entry =>
             {
                 entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                 entry.Priority = CacheItemPriority.Normal;
                 return _clientMsSala.SalaServicioDisponibleProveedorGetAsync(id);
             });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }


        [HttpGet("SalaServicioDisponibleProveedorGetByIdProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleProveedorDto>>> SalaServicioDisponibleProveedorGetByIdProveedor(int idProveedor)
        {
            var entidades = await _clientMsSala.SalaServicioDisponibleProveedorGetByIdProveedorAsync(idProveedor);
            /*var entidades = await
                _memoryCache.GetOrCreateAsync("SalaServicioDisponibleProveedorGetByIdProveedorAsync" + idProveedor.ToString(), entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                    entry.Priority = CacheItemPriority.Normal;
                    return _clientMsSala.SalaServicioDisponibleProveedorGetByIdProveedorAsync(idProveedor);
                });*/

            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpPost("SalaServicioDisponibleProveedorSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleProveedorDto>>> SalaServicioDisponibleProveedorSave(SalaServicioDisponibleProveedorDto input)
        {
            try
            {

                if (input == null) return BadRequest(input);
                var entidad = await _clientMsSala.SalaServicioDisponibleProveedorSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        [HttpPost("SalaServicioDisponibleProveedorSaveMasive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleProveedorDto>>> SalaServicioDisponibleProveedorSaveMasive(List<SalaServicioDisponibleProveedorDto> input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                List<SalaServicioDisponibleProveedorDto> serviciosDisponiblees = new List<SalaServicioDisponibleProveedorDto>();
                foreach (SalaServicioDisponibleProveedorDto SalaServicioDisponibleProveedor in input)
                {
                    SalaServicioDisponibleProveedorDto servicioDisponible = await _clientMsSala.SalaServicioDisponibleProveedorSaveAsync(SalaServicioDisponibleProveedor);
                    serviciosDisponiblees.Add(servicioDisponible);
                }
                return Ok(serviciosDisponiblees);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("SalaServicioDisponibleProveedorInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleProveedorDto>>> SalaServicioDisponibleProveedorInsert(SalaServicioDisponibleProveedorDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaServicioDisponibleProveedorInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("SalaServicioDisponibleProveedorUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaServicioDisponibleProveedorDto>>> SalaServicioDisponibleProveedorUpdate(SalaServicioDisponibleProveedorDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaServicioDisponibleProveedorUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }


    }
}
