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
    public class MembresiaServicioAdicionalClienteController : Controller
    {
        private msMembresiaClient _clientMsMembresia;
        private readonly IMemoryCache _memoryCache;
        public MembresiaServicioAdicionalClienteController(msMembresiaClient clientMsMembresia, IMemoryCache memoryCache)
        {
            _clientMsMembresia = clientMsMembresia;
            _memoryCache = memoryCache;
        }

        [HttpGet("MembresiaServicioAdicionalClienteGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalClienteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalClienteDto>>> MembresiaServicioAdicionalClienteGetAll()
        {
            //var entidades = await _clientMsMembresia.MembresiaServicioAdicionalClienteGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("MembresiaServicioAdicionalClienteGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsMembresia.MembresiaServicioAdicionalClienteGetAllAsync();
               });

            if (entidades == null) return NotFound();
            return Ok(entidades);
        }
        [HttpGet("MembresiaServicioAdicionalClienteGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalClienteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalClienteDto>>> MembresiaServicioAdicionalClienteGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsMembresia.MembresiaServicioAdicionalClienteGetAsync(id);
            var entidad = await
               _memoryCache.GetOrCreateAsync("MembresiaServicioAdicionalClienteGetAsync"+id.ToString(), entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsMembresia.MembresiaServicioAdicionalClienteGetAsync(id);
               });
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpGet("MembresiaServicioAdicionalClienteGetByIdCliente")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalClienteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalClienteDto>>> MembresiaServicioAdicionalClienteGetByIdCliente(int idCliente)
        {
           // var entidades = await _clientMsMembresia.MembresiaServicioAdicionalClienteGetByIdClienteAsync(idCliente);
            var entidades = await
               _memoryCache.GetOrCreateAsync("MembresiaServicioAdicionalClienteGetByIdClienteAsync"+ idCliente.ToString(), entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsMembresia.MembresiaServicioAdicionalClienteGetByIdClienteAsync(idCliente);
               });
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }
        [HttpPost("MembresiaServicioAdicionalClienteSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalClienteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalClienteDto>>> MembresiaServicioAdicionalCliente(MembresiaServicioAdicionalClienteDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsMembresia.MembresiaServicioAdicionalClienteSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw;
            }
        }
        [HttpPost("MembresiaServicioAdicionalClienteInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalClienteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalClienteDto>>> MembresiaServicioAdicionalClienteInsert(MembresiaServicioAdicionalClienteDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioAdicionalClienteInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("MembresiaServicioAdicionalClienteUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaServicioAdicionalClienteDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaServicioAdicionalClienteDto>>> MembresiaServicioAdicionalClienteUpdate(MembresiaServicioAdicionalClienteDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaServicioAdicionalClienteUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpDelete("MembresiaServicioAdicionalClienteDelete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult> MembresiaServicioAdicionalClienteDelete(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            await _clientMsMembresia.MembresiaServicioAdicionalClienteDeleteAsync(id);
            return NoContent();
           
        }
    }
}
