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
    public class CalificacionController : Controller
    {
        private msCalificacionClient _clientMsCalificacion;
        private msTransaccionClient _clientMsTransaccion;
        private readonly IMemoryCache _memoryCache;
        public CalificacionController(msCalificacionClient clientMsCalificacion, msTransaccionClient clientMsTransaccion, IMemoryCache memoryCache)
        {
            _clientMsCalificacion = clientMsCalificacion;
            _clientMsTransaccion = clientMsTransaccion;
            _memoryCache = memoryCache;
        }

        [HttpGet("CalificacionGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CalificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CalificacionDto>>> CalificacionGetAll()
        {
            try
            {
                //var entidades = await _clientMsCalificacion.CalificacionGetAllAsync();
                var entidades = await
                _memoryCache.GetOrCreateAsync("CalificacionGetAllAsync", entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                    entry.Priority = CacheItemPriority.Normal;
                    return _clientMsCalificacion.CalificacionGetAllAsync();
                });
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
        [HttpGet("CalificacionGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CalificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CalificacionDto>>> CalificacionGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
           // var entidad = await _clientMsCalificacion.CalificacionGetAsync(id);
            var entidad = await
               _memoryCache.GetOrCreateAsync("CalificacionGetAsync"+id.ToString(), entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsCalificacion.CalificacionGetAsync(id);
               });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("CalificacionGetByIdUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CalificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CalificacionDto>>> CalificacionGetByIdUsuario(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsCalificacion.GetByIdUsuarioAsync(id);
            var entidad = await
           _memoryCache.GetOrCreateAsync("GetByIdUsuarioAsync"+id.ToString(), entry =>
           {
               entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
               entry.Priority = CacheItemPriority.Normal;
               return _clientMsCalificacion.GetByIdUsuarioAsync(id);
           });

            if (entidad == null) return NotFound();
            return Ok(entidad);

        }

        [HttpGet("CalificacionGetByIdProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CalificacionDto2>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CalificacionDto2>>> CalificacionGetByIdProveedor(int id)
        {
            if (id <= 0) return BadRequest(ModelState);

              var entidad = await _clientMsCalificacion.GetByIdProveedorAsync(id);

              List<CalificacionDto2> ListCalificacion2 = new List<CalificacionDto2>();
              foreach (CalificacionDto calificacion in entidad)
              {
                  CalificacionDto2 calificacion2 = new CalificacionDto2();

                  calificacion2.IdCalificacion = calificacion.IdCalificacion;
                  calificacion2.IdTipoCategoria = calificacion.IdTipoCategoria;
                  calificacion2.IdUsuario = calificacion.IdUsuario;
                  calificacion2.IdProveedor = calificacion.IdProveedor;
                  calificacion2.Valoracion = calificacion.Valoracion;
                  calificacion2.Valorado = calificacion.Valorado;
                  calificacion2.Detalle = calificacion.Detalle;
                  calificacion2.BorradoLogico = calificacion.BorradoLogico;
                  calificacion2.IdTipoCategoriaNavigation = calificacion.IdTipoCategoriaNavigation;

                  var transaccion = await _clientMsTransaccion.TransaccionGetByIdAsync(Int32.Parse(calificacion.IdTransaccion.ToString()));
                  calificacion2.IdTransaccion = transaccion;
                  ListCalificacion2.Add(calificacion2);
              }
              if (ListCalificacion2 == null) return NotFound();
              return Ok(ListCalificacion2);

            /*var entidad = await
            _memoryCache.GetOrCreateAsync("CalificacionGetByIdProveedor", entry =>
            {
               entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
               entry.Priority = CacheItemPriority.Normal;
                var entidades =  _clientMsCalificacion.GetByIdProveedorAsync(id);

                List<CalificacionDto2> ListCalificacion2 = new List<CalificacionDto2>();
                foreach (CalificacionDto calificacion in entidades)
                {
                    CalificacionDto2 calificacion2 = new CalificacionDto2();

                    calificacion2.IdCalificacion = calificacion.IdCalificacion;
                    calificacion2.IdTipoCategoria = calificacion.IdTipoCategoria;
                    calificacion2.IdUsuario = calificacion.IdUsuario;
                    calificacion2.IdProveedor = calificacion.IdProveedor;
                    calificacion2.Valoracion = calificacion.Valoracion;
                    calificacion2.Valorado = calificacion.Valorado;
                    calificacion2.Detalle = calificacion.Detalle;
                    calificacion2.BorradoLogico = calificacion.BorradoLogico;
                    calificacion2.IdTipoCategoriaNavigation = calificacion.IdTipoCategoriaNavigation;

                    var transaccion =  _clientMsTransaccion.TransaccionGetByIdAsync(Int32.Parse(calificacion.IdTransaccion.ToString()));
                    calificacion2.IdTransaccion = transaccion;
                    ListCalificacion2.Add(calificacion2);
                }
                return (ListCalificacion2);
            });
            if (entidad == null) return NotFound();
            return Ok(entidad); */

        }

        [HttpPost("CalificacionSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CalificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CalificacionDto>>> CalificacionSave(CalificacionDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsCalificacion.CalificacionSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex )
            {

                throw new System.Exception(ex.Message);
            }
        }
        [HttpPost("CalificacionInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CalificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CalificacionDto>>> CalificacionInsert(CalificacionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsCalificacion.CalificacionInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("CalificacionUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CalificacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CalificacionDto>>> CalificacionUpdate(CalificacionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsCalificacion.CalificacionUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("CalificacionDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> CalificacionDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsCalificacion.CalificacionInsertAsync(new CalificacionDto { IdCalificacion = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
