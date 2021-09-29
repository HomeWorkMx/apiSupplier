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
    public class ProveedorController : Controller
    {
        private msMembresiaClient _clientMsMembresia;
        private msProductoClient _clientMsProducto;
        private msPaqueteClient _clientMsPaquete;
        private msSalaClient _clientMsSala;
        private readonly IMemoryCache _memoryCache;
        public ProveedorController(
            msMembresiaClient clientMsMembresia, IMemoryCache memoryCache,
            msProductoClient clientMsProducto,
            msPaqueteClient clientMsPaquete,
            msSalaClient clientMsSala)
        {
            _clientMsMembresia = clientMsMembresia;
            _memoryCache = memoryCache;
            _clientMsProducto = clientMsProducto;
            _clientMsPaquete = clientMsPaquete;
            _clientMsSala = clientMsSala;
        }

        [HttpGet("MembresiaProveedorByIdProveedor/{IdProveedor}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaProveedorDto>>> MembresiaProveedorByIdProveedor(int IdProveedor)
        {
            if (IdProveedor <= 0) return BadRequest(ModelState);

            var miMembresiaProveedor = await _clientMsMembresia.MembresiaProveedorGetAllAsync();
            miMembresiaProveedor= miMembresiaProveedor.Where(x => x.IdUsuario.Equals(IdProveedor)).ToList();
   
            if (miMembresiaProveedor == null) return NotFound();
            
            return Ok(miMembresiaProveedor);
        }
        [HttpGet("ProductoProveedorByIdProveedor/{IdProveedor}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductoProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ProductoProveedorDto>>> ProductoProveedorByIdProveedor(int IdProveedor)
        {
            if (IdProveedor <= 0) return BadRequest(ModelState);

            var miProductoProveedor = await _clientMsProducto.ProductoProveedorGetAllAsync();
            miProductoProveedor = miProductoProveedor.Where(x => x.IdUsuario.Equals(IdProveedor)).ToList();

            if (miProductoProveedor == null) return NotFound();

            return Ok(miProductoProveedor);
        }
        [HttpGet("PaqueteProveedorByIdProveedor/{IdProveedor}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaqueteProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PaqueteProveedorDto>>> PaqueteProveedorByIdProveedor(int IdProveedor)
        {
            if (IdProveedor <= 0) return BadRequest(ModelState);

            var miPaqueteProveedor = await _clientMsPaquete.PaqueteProveedorGetAllAsync();
            miPaqueteProveedor = miPaqueteProveedor.Where(x => x.IdUsuario.Equals(IdProveedor)).ToList();

            if (miPaqueteProveedor == null) return NotFound();

            return Ok(miPaqueteProveedor);
        }
        [HttpGet("SalaProveedorByIdProveedor/{IdProveedor}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaProveedorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaProveedorDto>>> SalaProveedorByIdProveedor(int IdProveedor)
        {
            if (IdProveedor <= 0) return BadRequest(ModelState);

            var miSalaProveedor = await _clientMsSala.SalaProveedorGetAllAsync();
            miSalaProveedor = miSalaProveedor.Where(x => x.IdUsuario.Equals(IdProveedor)).ToList();

            if (miSalaProveedor == null) return NotFound();

            return Ok(miSalaProveedor);
        }
        //  [HttpGet("MembresiaGet/{id}")]
        //  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        //  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //  [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //  [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //  public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaGet(int id)
        //  {
        //      if (id <= 0) return BadRequest(ModelState);
        //      //var entidad = await _clientMsMembresia.MembresiaGetAsync(id);

        //      var entidad = await
        //         _memoryCache.GetOrCreateAsync("MembresiaGetAsync"+id.ToString(), entry =>
        //         {
        //             entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
        //             entry.Priority = CacheItemPriority.Normal;
        //             return _clientMsMembresia.MembresiaGetAsync(id);
        //         });

        //      if (entidad == null) return NotFound();
        //      return Ok(entidad);
        //  }

        //[HttpGet("MembresiaGetByIdUsuario/{IdUsuario}")]
        //  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        //  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //  [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //  [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //  public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaGetByIdUsuario(int IdUsuario)
        //  {
        //      if (IdUsuario <= 0) return BadRequest(ModelState);
        //      List<MembresiaDto> entidades = new List<MembresiaDto>();
        //      var misMembresiaUsuario = await _clientMsMembresia.MembresiaUsuarioGetAllAsync();
        //      misMembresiaUsuario = misMembresiaUsuario.Where(x => x.IdUsuario.Equals(IdUsuario)).ToList();
        //      foreach (MembresiaUsuarioDto membresiaUsuario in misMembresiaUsuario)
        //      {
        //          entidades.Add(await _clientMsMembresia.MembresiaGetAsync((int)membresiaUsuario.IdMembresia));
        //      }
        //      return Ok(entidades);
        //  }

        //  [HttpPost("MembresiaSave")]
        //  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        //  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //  [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //  [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //  public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaSave(MembresiaDto input)
        //  {
        //      try
        //      {
        //          if (input == null) return BadRequest(input);
        //          var entidad = await _clientMsMembresia.MembresiaSaveAsync(input);
        //          if (entidad == null) return NotFound();
        //          return Ok(entidad);
        //      }
        //      catch (System.Exception ex )
        //      {

        //          throw;
        //      }
        //  }
        //  [HttpPost("MembresiaInsert")]
        //  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        //  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //  [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //  [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //  public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaInsert(MembresiaDto input)
        //  {
        //      if (input == null) return BadRequest(input);
        //      var entidad = await _clientMsMembresia.MembresiaInsertAsync(input);
        //      if (entidad == null) return NotFound();
        //      return Ok(entidad);
        //  }
        //  [HttpPut("MembresiaUpdate")]
        //  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        //  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //  [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //  [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //  public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaUpdate(MembresiaDto input)
        //  {
        //      if (input == null) return BadRequest(input);
        //      var entidad = await _clientMsMembresia.MembresiaUpdateAsync(input);
        //      if (entidad == null) return NotFound();
        //      return Ok(entidad);
        //  }
        //[HttpDelete("MembresiaDelete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<ActionResult> MembresiaDelete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0) return BadRequest(ModelState);
        //        var entidad = await _clientMsMembresia.MembresiaInsertAsync(new MembresiaDto { IdMembresia = id });
        //        return NoContent();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
