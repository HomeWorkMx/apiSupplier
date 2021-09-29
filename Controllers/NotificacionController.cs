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
using System.Linq;

namespace apiSupplier.Controllers
{
    [TypeFilter(typeof(InterceptorLogAttribute))]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class NotificacionController : Controller
    {
        private msNotificacionesClient _clientMsNotificacion;
        private readonly IMemoryCache _memoryCache;
        public NotificacionController(msNotificacionesClient clientMsNotificacion, IMemoryCache memoryCache)
        {
            _clientMsNotificacion = clientMsNotificacion;
            _memoryCache = memoryCache;
        }
        [HttpGet("NotificacionGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionDto>>> NotificacionGetAll()
        {
            //var entidades = await _clientMsNotificacion.NotificacionGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("NotificacionGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsNotificacion.NotificacionGetAllAsync();
               });

            if (entidades == null) return NotFound();
            return Ok(entidades);
        }



        [HttpGet("NotificacionUsuarioGetByIdUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionUsuarioDto>>> NotificacionUsuarioGetByIdUsuario(int idUsuario)
        {
            try
            {
                List<NotificacionDto> entidades = new List<NotificacionDto>();
                var misNotificaciones =  await _clientMsNotificacion.NotificacionUsuarioGetAllAsync();
                misNotificaciones = misNotificaciones.Where(x => x.IdUsuario.Equals(idUsuario)).ToList();
                foreach (NotificacionUsuarioDto item in misNotificaciones)
                {
                    var notificacion = await _clientMsNotificacion.NotificacionGetByIdAsync(int.Parse(item.IdNotificacion.ToString()));
                    entidades.Add(notificacion);                    
                }

                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        [HttpGet("NotificacionGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionDto>>> NotificacionGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsNotificacion.NotificacionGetByIdAsync(id);
            var entidad = await
               _memoryCache.GetOrCreateAsync("NotificacionGetByIdAsync"+id.ToString(), entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsNotificacion.NotificacionGetByIdAsync(id);
               });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPost("NotificacionSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionDto>>> NotificacionSave(NotificacionDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsNotificacion.NotificacionSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        [HttpPost("NotificacionInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionDto>>> NotificacionInsert(NotificacionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsNotificacion.NotificacionInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("NotificacionUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionDto>>> NotificacionUpdate(NotificacionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsNotificacion.NotificacionUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("NotificacionDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> NotificacionDelete(int id)
        //{
        //    if (id <= 0) return BadRequest(ModelState);
        //    var entidad = await _clientMsNotificacion.NotificacionInsertAsync(new NotificacionDto { IdNotificacion = id });
        //    return NoContent();
        //} 
    }
}
