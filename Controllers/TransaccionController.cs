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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using apiSupplier.Models;
using System.Linq;


namespace apiSupplier.Controllers
{
    [TypeFilter(typeof(InterceptorLogAttribute))]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class TransaccionController : Controller 
    {
        private msTransaccionClient _clientMsTransaccion;
        private msUsuarioClient _clientMsUsuario;
        private msMembresiaClient _clientMsMembresia;
        private msSalaClient _clientMsSala;
        private msPaqueteClient _clientMsPaquete;
        private msProductoClient _clientMsProducto;



        private readonly IMemoryCache _memoryCache;
        public TransaccionController(msTransaccionClient clientMsTransaccion, IMemoryCache memoryCache
            ,msUsuarioClient clientMsUsuario
            , msMembresiaClient clientMsMembresia
            , msSalaClient clientMsSala
            , msPaqueteClient clientMsPaquete
            , msProductoClient clientMsProducto
            )
        {
            try
            {
                _clientMsTransaccion = clientMsTransaccion;
                _memoryCache = memoryCache;
                _clientMsUsuario = clientMsUsuario;
                _clientMsMembresia = clientMsMembresia;
                _clientMsSala = clientMsSala;
                _clientMsPaquete = clientMsPaquete;
                _clientMsProducto = clientMsProducto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet("TransaccionGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionDto>>> TransaccionGetAll()
        {
            try
            {
                //var entidades = await _clientMsTransaccion.TransaccionGetAllAsync();
                var entidades = await
                  _memoryCache.GetOrCreateAsync("TransaccionGetAllAsync", entry =>
                  {
                      entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                      entry.Priority = CacheItemPriority.Normal;
                      return _clientMsTransaccion.TransaccionGetAllAsync();
                  });
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("VerPagos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionDto>>> VerPagos([FromForm, DefaultValue(0)] int idCliente,
            [FromForm, DefaultValue(0)] int idProveedor,
            [FromForm, DefaultValue(0)] string ArrayIdTipoEstadoTransaccion)
        {
            if (idCliente < 0 && idProveedor < 0 ) return BadRequest(ModelState);
  
            var entidad = await
              _memoryCache.GetOrCreateAsync("VerPagos_" + idCliente.ToString()+"_"+idProveedor.ToString() + "_" + ArrayIdTipoEstadoTransaccion, entry =>
              {
                  entry.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
                  entry.Priority = CacheItemPriority.Normal;
                  return _clientMsTransaccion.VerPagosAsync(idCliente,idProveedor, ArrayIdTipoEstadoTransaccion);
              });
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }


        [HttpPost("VerContratos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ContratoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionDto>>> VerContratos([FromForm, DefaultValue(0)] int idCliente,
           [FromForm, DefaultValue(0)] int idProveedor,
           [FromForm, DefaultValue(0)] string ArrayIdTipoEstadoTransaccion)
        {
            try
            {
                if (idCliente <= 0) return BadRequest(ModelState);

                var itemsTransacciones = await _clientMsTransaccion.VerPagosAsync(idCliente, idProveedor, ArrayIdTipoEstadoTransaccion);
                
                if (itemsTransacciones == null) return NotFound();

                var aux = await _clientMsTransaccion.TransaccionProductosGetAllAsync();

                foreach (TransaccionDto itemTransaccion in itemsTransacciones)
                {
                    var  misProductos = aux.Where(x => x.IdTransaccion == itemTransaccion.IdTransacciones);
                    if (misProductos != null) {
                        foreach (TransaccionProductosDto miProducto in misProductos)
                        {
                            //miProducto.IdMembresiaNavigation = miProducto.IdMembresia != null ? await _clientMsMembresia.MembresiaGetAsync(int.Parse(miProducto.IdMembresia.ToString())) : null;
                            //miProducto.IdSalaNavigation = miProducto.IdSala != null ? await _clientMsSala.SalaGetAsync(int.Parse(miProducto.IdSala.ToString())) : null;
                            //miProducto.IdPaqueteNavigation = miProducto.IdPaquete != null ? await _clientMsPaquete.PaqueteGetAsync(int.Parse(miProducto.IdPaquete.ToString())) : null;
                            //miProducto.IdProductoNavigation = miProducto.IdProducto != null ? await _clientMsProducto.ProductoGetAsync(int.Parse(miProducto.IdProducto.ToString())) : null;
                          /* if(miProducto.IdMembresiaNavigation!= null||
                                miProducto.IdSalaNavigation != null ||
                                miProducto.IdPaqueteNavigation != null ||
                                miProducto.IdProductoNavigation != null)*/
                                itemTransaccion.TransaccionProductos.Add(miProducto);
                        }
                    }
                }
                var filt = itemsTransacciones.Where(x => x.TransaccionProductos.Count > 0);

                return Ok(filt);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        [HttpGet("TransaccionGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionDto>>> TransaccionGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            //var entidad = await _clientMsTransaccion.TransaccionGetByIdAsync(id);
            var entidad = await
              _memoryCache.GetOrCreateAsync("TransaccionGetByIdAsync" + id.ToString(), entry =>
              {
                  entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                  entry.Priority = CacheItemPriority.Normal;
                  return _clientMsTransaccion.TransaccionGetByIdAsync(id);
              });
            if (entidad == null) return NotFound();
            var miUsuarioDto2 = await _clientMsUsuario.UsuarioGetBasicAsync(int.Parse(entidad.IdUsuarioCliente.ToString()));
            entidad.IdUsuarioClienteNavigation = new UsuarioDto();
            entidad.IdUsuarioClienteNavigation.IdUsuario = miUsuarioDto2.IdUsuario;
            entidad.IdUsuarioClienteNavigation.IdTipoUsuario = miUsuarioDto2.IdTipoUsuario;
            entidad.IdUsuarioClienteNavigation.FechaIngreso = miUsuarioDto2.FechaIngreso;
            //entidad.IdUsuarioClienteNavigation.Imagen = miUsuarioDto2.Imagen;
            entidad.IdUsuarioClienteNavigation.NombreUsuario = miUsuarioDto2.NombreUsuario;
            entidad.IdUsuarioClienteNavigation.PaginaWeb = miUsuarioDto2.PaginaWeb;
            entidad.IdUsuarioClienteNavigation.RazonSocial = miUsuarioDto2.RazonSocial;
            entidad.IdUsuarioClienteNavigation.Telefono = miUsuarioDto2.Telefono;
            entidad.IdUsuarioClienteNavigation.Correo = miUsuarioDto2.Correo;
            entidad.IdUsuarioClienteNavigation.Direccion = miUsuarioDto2.Direccion;
            entidad.IdUsuarioClienteNavigation.Descripcion = miUsuarioDto2.Descripcion;

            var miProveedorDto2 = await _clientMsUsuario.UsuarioGetBasicAsync(int.Parse(entidad.IdUsuarioProveedor.ToString()));
            entidad.IdUsuarioProveedorNavigation = new UsuarioDto();
            entidad.IdUsuarioProveedorNavigation.IdUsuario = miProveedorDto2.IdUsuario;
            entidad.IdUsuarioProveedorNavigation.IdTipoUsuario = miProveedorDto2.IdTipoUsuario;
            entidad.IdUsuarioProveedorNavigation.FechaIngreso = miProveedorDto2.FechaIngreso;
            //entidad.IdUsuarioProveedorNavigation.Imagen = miProveedorDto2.Imagen;
            entidad.IdUsuarioProveedorNavigation.NombreUsuario = miProveedorDto2.NombreUsuario;
            entidad.IdUsuarioProveedorNavigation.PaginaWeb = miProveedorDto2.PaginaWeb;
            entidad.IdUsuarioProveedorNavigation.RazonSocial = miProveedorDto2.RazonSocial;
            entidad.IdUsuarioProveedorNavigation.Telefono = miProveedorDto2.Telefono;
            entidad.IdUsuarioProveedorNavigation.Correo = miProveedorDto2.Correo;
            entidad.IdUsuarioProveedorNavigation.Direccion = miProveedorDto2.Direccion;
            entidad.IdUsuarioProveedorNavigation.Descripcion = miProveedorDto2.Descripcion;

            foreach (TransaccionProductosDto miProducto in entidad.TransaccionProductos)
            {
                miProducto.IdMembresiaNavigation = miProducto.IdMembresia != null ? await _clientMsMembresia.MembresiaGetAsync(int.Parse(miProducto.IdMembresia.ToString())) : null;
                miProducto.IdSalaNavigation = miProducto.IdSala != null ? await _clientMsSala.SalaGetAsync(int.Parse(miProducto.IdSala.ToString())) : null;
                miProducto.IdPaqueteNavigation = miProducto.IdPaquete != null ? await _clientMsPaquete.PaqueteGetAsync(int.Parse(miProducto.IdPaquete.ToString())) : null;
                miProducto.IdProductoNavigation = miProducto.IdProducto != null ? await _clientMsProducto.ProductoGetAsync(int.Parse(miProducto.IdProducto.ToString())) : null;
            }

            return Ok(entidad);

        }

        [HttpPost("TransaccionSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionDto>>> TransaccionSave(TransaccionDto2 input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsTransaccion.TransaccionSaveAsync(input);
                if (entidad == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        [HttpPost("TransaccionInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionDto>>> TransaccionInsert(TransaccionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTransaccion.InsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("TransaccionUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransaccionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TransaccionDto>>> TransaccionUpdate(TransaccionDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsTransaccion.TransaccionUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        //[HttpDelete("TransaccionDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> TransaccionDelete(int id)
        //{
        //    if (id <= 0) return BadRequest(ModelState);
        //    var entidad = await _clientMsTransaccion.TransaccionInsertAsync(new TransaccionDto { IdTransaccion = id });
        //    return NoContent();
        //} 

    }
}
