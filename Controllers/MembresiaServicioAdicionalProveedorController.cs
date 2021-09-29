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
    public class MembresiaServicioAdicionalProveedorController : Controller
    {
        private msMembresiaClient _clientMsMembresia;
        private readonly IMemoryCache _memoryCache;
        public MembresiaServicioAdicionalProveedorController(msMembresiaClient clientMsMembresia, IMemoryCache memoryCache)
        {
            _clientMsMembresia = clientMsMembresia;
            _memoryCache = memoryCache;
        }

        [HttpGet("MembresiaServicioAdicionalProveedorGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalProveedorDto>>> MembresiaServicioAdicionalProveedorGetAll()
        {
            //var entidades = await _clientMsMembresia.MembresiaServicioAdicionalProveedorGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("MembresiaServicioAdicionalProveedorGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsMembresia.MembresiaServicioAdicionalProveedorGetAllAsync();
               });

            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpGet("MembresiaServicioAdicionalProveedorGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalProveedorDto>>> MembresiaServicioAdicionalProveedorGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsMembresia.MembresiaServicioAdicionalProveedorGetAsync(id);
            var entidad = await
              _memoryCache.GetOrCreateAsync("MembresiaServicioAdicionalProveedorGetAsync" + id.ToString(), entry =>
              {
                  entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                  entry.Priority = CacheItemPriority.Normal;
                  return _clientMsMembresia.MembresiaServicioAdicionalProveedorGetAsync(id);
              });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("MembresiaServicioAdicionalProveedorGetByIdProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalProveedorDto>>> MembresiaServicioAdicionalProveedorGetByIdProveedor(int idProveedor)
        {
            //var entidades = await _clientMsMembresia.MembresiaServicioAdicionalProveedorGetByIdProveedorAsync(idProveedor);
            var entidades = await
                _memoryCache.GetOrCreateAsync("MembresiaServicioAdicionalProveedorGetByIdProveedorAsync" + idProveedor.ToString(), entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                    entry.Priority = CacheItemPriority.Normal;
                    return _clientMsMembresia.MembresiaServicioAdicionalProveedorGetByIdProveedorAsync(idProveedor);
                });

            if (entidades == null) return NotFound();
            return Ok(entidades);
        }

        [HttpPost("MembresiaServicioAdicionalProveedorSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalProveedorDto>>> MembresiaServicioAdicionalProveedor(MembresiaServicioAdicionalProveedorDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsMembresia.MembresiaServicioAdicionalProveedorSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw;
            }
        }
        [HttpPost("MembresiaServicioAdicionalProveedorInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalProveedorDto>>> MembresiaServicioAdicionalProveedorInsert(MembresiaServicioAdicionalProveedorDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioAdicionalProveedorInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("MembresiaServicioAdicionalProveedorUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalProveedorDto>>> MembresiaServicioAdicionalProveedorUpdate(MembresiaServicioAdicionalProveedorDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioAdicionalProveedorUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpDelete("MembresiaServicioAdicionalProveedorDelete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> MembresiaServicioAdicionalProveedorDelete(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            await _clientMsMembresia.MembresiaServicioAdicionalProveedorDeleteAsync(id);
            return NoContent();
           
        }
    }
}
