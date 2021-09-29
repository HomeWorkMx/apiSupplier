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
    public class NotificacionUsuarioController : Controller
    {
        private msNotificacionesClient _clientMsNotificacionUsuario;
        private readonly IMemoryCache _memoryCache;
        public NotificacionUsuarioController(msNotificacionesClient clientMsNotificacionUsuario, IMemoryCache memoryCache)
        {
            _clientMsNotificacionUsuario = clientMsNotificacionUsuario;
            _memoryCache = memoryCache;
        }

        [HttpGet("NotificacionUsuarioGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionUsuarioDto>>> NotificacionUsuarioGetAll()
        {
            var entidades = await
                _memoryCache.GetOrCreateAsync("NotificacionUsuarioGetAllAsync", entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                    entry.Priority = CacheItemPriority.Normal;
                    var NotificacionUsuarios = _clientMsNotificacionUsuario.NotificacionUsuarioGetAllAsync();
                    return _clientMsNotificacionUsuario.NotificacionUsuarioGetAllAsync();
                });
            if (entidades == null) return NotFound();
            
            return Ok(entidades);
        }

        [HttpGet("NotificacionUsuarioGetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionUsuarioDto>>> NotificacionUsuarioGetById(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsNotificacionUsuario.NotificacionUsuarioGetAsync(id);

            var entidad = await
               _memoryCache.GetOrCreateAsync("NotificacionUsuarioGetByIdAsync" + id.ToString(), entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsNotificacionUsuario.NotificacionUsuarioGetByIdAsync(id);
               });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("NotificacionUsuarioGetByIdUsuario/{idUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionUsuarioDto>>> NotificacionUsuarioGetByIdUsuario(int idUsuario)
        {

            var entidades = await _clientMsNotificacionUsuario.NotificacionUsuarioGetAllAsync();
       
            entidades = entidades.Where(x => x.IdUsuario == idUsuario).ToList();
            if (entidades == null) return NotFound();

            return Ok(entidades);
        }

        [HttpPost("NotificacionUsuarioSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionUsuarioDto>>> NotificacionUsuarioSave(NotificacionUsuarioDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsNotificacionUsuario.NotificacionUsuarioSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw;
            }
        }
        [HttpPost("NotificacionUsuarioInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionUsuarioDto>>> NotificacionUsuarioInsert(NotificacionUsuarioDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsNotificacionUsuario.NotificacionUsuarioInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("NotificacionUsuarioUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NotificacionUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<NotificacionUsuarioDto>>> NotificacionUsuarioUpdate(NotificacionUsuarioDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsNotificacionUsuario.NotificacionUsuarioUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("NotificacionUsuarioDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> NotificacionUsuarioDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsNotificacionUsuario.NotificacionUsuarioInsertAsync(new NotificacionUsuarioDto { IdNotificacionUsuario = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
