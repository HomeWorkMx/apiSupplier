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
    public class MembresiaServicioDisponibleProveedorController : Controller
    {
        private msMembresiaClient _clientMsMembresia;
        private readonly IMemoryCache _memoryCache;
        public MembresiaServicioDisponibleProveedorController(msMembresiaClient clientMsMembresia, IMemoryCache memoryCache)
        {
            _clientMsMembresia = clientMsMembresia;
            _memoryCache = memoryCache;
        }

        [HttpGet("MembresiaServicioDisponibleProveedorGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleProveedorDto>>> MembresiaServicioDisponibleProveedorGetAll()
        {
            //var entidades = await _clientMsMembresia.MembresiaServicioDisponibleProveedorGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("MembresiaServicioDisponibleProveedorGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsMembresia.MembresiaServicioDisponibleProveedorGetAllAsync();
               });

            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("MembresiaServicioDisponibleProveedorGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleProveedorDto>>> MembresiaServicioDisponibleProveedorGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsMembresia.MembresiaServicioDisponibleProveedorGetAsync(id);
            var entidad = await
              _memoryCache.GetOrCreateAsync("MembresiaServicioDisponibleProveedorGetAsync" + id.ToString(), entry =>
              {
                  entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                  entry.Priority = CacheItemPriority.Normal;
                  return _clientMsMembresia.MembresiaServicioDisponibleProveedorGetAsync(id);
              });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("MembresiaServicioDisponibleProveedorGetByIdProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleProveedorDto>>> MembresiaServicioDisponibleProveedorGetByIdProveedor(int idProveedor)
        {
            var entidades = await _clientMsMembresia.MembresiaServicioDisponibleProveedorGetByIdProveedorAsync(idProveedor);
            /*var entidades = await
                _memoryCache.GetOrCreateAsync("MembresiaServicioDisponibleProveedorGetByIdProveedorAsync" + idProveedor.ToString(), entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                    entry.Priority = CacheItemPriority.Normal;
                    return _clientMsMembresia.MembresiaServicioDisponibleProveedorGetByIdProveedorAsync(idProveedor);
                });*/

            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpPost("MembresiaServicioDisponibleProveedorSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleProveedorDto>>> MembresiaServicioDisponibleProveedor(MembresiaServicioDisponibleProveedorDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsMembresia.MembresiaServicioDisponibleProveedorSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw;
            }
        }
        [HttpPost("MembresiaServicioDisponibleProveedorInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleProveedorDto>>> MembresiaServicioDisponibleProveedorInsert(MembresiaServicioDisponibleProveedorDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioDisponibleProveedorInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("MembresiaServicioDisponibleProveedorUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioDisponibleProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioDisponibleProveedorDto>>> MembresiaServicioDisponibleProveedorUpdate(MembresiaServicioDisponibleProveedorDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioDisponibleProveedorUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpDelete("MembresiaServicioDisponibleProveedorDelete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> MembresiaServicioDisponibleProveedorDelete(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            await _clientMsMembresia.MembresiaServicioDisponibleProveedorDeleteAsync(id);
            return NoContent();
           
        }
    }
}
