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
    public class SalaController : Controller
    {
        private msSalaClient _clientMsSala;
        private readonly IMemoryCache _memoryCache;
        public SalaController(msSalaClient clientMsSala, IMemoryCache memoryCache)
        {
            _clientMsSala = clientMsSala;
            _memoryCache = memoryCache;
        }
        [HttpGet("SalaGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDto>>> SalaGetAll()
        {
            //var entidades = await _clientMsSala.SalaGetAllAsync();
            var entidades = await
               _memoryCache.GetOrCreateAsync("SalaGetAllAsync", entry =>
               {
                   entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                   entry.Priority = CacheItemPriority.Normal;
                   return _clientMsSala.SalaGetAllAsync(); 
               });
            if (entidades == null) return NotFound();
            return Ok(entidades);
        }
       /* private async  Task<ICollection<SalaDto>> SalaGetAllAsync() {

            var entidades = await _clientMsSala.SalaGetAllAsync();
           List<SalaDto>salida = new List<SalaDto>();
            foreach (SalaDto item in entidades)
            {
                int id = int.Parse(item.IdSala.ToString());
                SalaDto dato = new SalaDto();
                dato.IdSala = item.IdSala;
                dato.NombreSala = item.NombreSala;
                dato.Descripcion = item.Descripcion;
                dato.Capacidad = item.Capacidad;
                dato.CostoHora = item.CostoHora;
                dato.TiempoMinReserva = item.TiempoMinReserva;
                dato.TiempoMaxReserva = item.TiempoMaxReserva;
                dato.PoliticaCancelacion = item.PoliticaCancelacion;
                dato.IdTerminoContrato = item.IdTerminoContrato;
                dato.IdTipoPago = item.IdTipoPago;
                dato.LinkVista360 = item.LinkVista360;
                dato.IdTipoProducto = item.IdTipoProducto;
                dato.IdOrigenProducto = item.IdOrigenProducto;
                dato.CordX = item.CordX;
                dato.CordY = item.CordY;
                dato.BorradoLogico = item.BorradoLogico;

                var salaCategoria = await _clientMsSala.SalaCategoriaGetAsync(id);
                dato.SalaCategoria = salaCategoria;
                var salaCreacionPrecio = await _clientMsSala.SalaCreacionPrecioGetAsync(id);
                var salaDisponibilidad = await _clientMsSala.SalaDisponibilidadGetAsync(id);
                var salaImagen = await _clientMsSala.SalaImagenGetAsync(id);
                var salaProveedor = await _clientMsSala.SalaProveedorGetAsync(id);
                var salaServicioDisponible = await _clientMsSala.SalaServicioDisponibleGetAsync(id);
                var salaServicioAdicional = await _clientMsSala.SalaServicioAdicionalGetAsync(id);
                var idTerminoContratoNavigation = await _clientMsSala.SalaTerminosContratoGetAsync(int.Parse(item.IdTerminoContrato.ToString()));
                var salaUsuario = await _clientMsSala.SalaUsuarioGetAsync(id);


                salida.Add(dato);
            }


            return salida;

        }*/
        [HttpGet("SalaGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDto>>> SalaGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsSala.SalaGetAsync(id);
            var entidad = await
             _memoryCache.GetOrCreateAsync("SalaGetAsync"+id.ToString(), entry =>
             {
                 entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                 entry.Priority = CacheItemPriority.Normal;
                 return _clientMsSala.SalaGetAsync(id);
             });

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpPost("SalaSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDto>>> SalaSave(SalaDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsSala.SalaSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        [HttpPost("SalaInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDto>>> SalaInsert(SalaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("SalaUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaDto>>> SalaUpdate(SalaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsSala.SalaUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("SalaDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> SalaDelete(int id)
        //{
        //    if (id <= 0) return BadRequest(ModelState);
        //    var entidad = await _clientMsSala.SalaInsertAsync(new SalaDto { IdSala = id });
        //    return NoContent();
        //} 

    }
}
