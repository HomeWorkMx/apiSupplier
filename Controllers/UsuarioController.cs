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
    public class UsuarioController : Controller
    {
        private msMembresiaClient _clientMsMembresia;
        private msProductoClient _clientMsProducto;
        private msPaqueteClient _clientMsPaquete;
        private msSalaClient _clientMsSala;
        private readonly IMemoryCache _memoryCache;
        public UsuarioController(
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

        [HttpGet("MembresiaUsuarioByIdUsuario/{IdUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaUsuarioDto>>> MembresiaUsuarioByIdUsuario(int IdUsuario)
        {
            if (IdUsuario <= 0) return BadRequest(ModelState);

            var miMembresiaUsuario = await _clientMsMembresia.MembresiaUsuarioGetAllAsync();
            miMembresiaUsuario= miMembresiaUsuario.Where(x => x.IdUsuario.Equals(IdUsuario)).ToList();
   
            if (miMembresiaUsuario == null) return NotFound();
            
            return Ok(miMembresiaUsuario);
        }
        [HttpGet("ProductoUsuarioByIdUsuario/{IdUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductoUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ProductoUsuarioDto>>> ProductoUsuarioByIdUsuario(int IdUsuario)
        {
            if (IdUsuario <= 0) return BadRequest(ModelState);

            var miProductoUsuario = await _clientMsProducto.ProductoUsuarioGetAllAsync();
            miProductoUsuario = miProductoUsuario.Where(x => x.IdUsuario.Equals(IdUsuario)).ToList();

            if (miProductoUsuario == null) return NotFound();

            return Ok(miProductoUsuario);
        }
        [HttpGet("PaqueteUsuarioByIdUsuario/{IdUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaqueteUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<PaqueteUsuarioDto>>> PaqueteUsuarioByIdUsuario(int IdUsuario)
        {
            if (IdUsuario <= 0) return BadRequest(ModelState);

            var miPaqueteUsuario = await _clientMsPaquete.PaqueteUsuarioGetAllAsync();
            miPaqueteUsuario = miPaqueteUsuario.Where(x => x.IdUsuario.Equals(IdUsuario)).ToList();

            if (miPaqueteUsuario == null) return NotFound();

            return Ok(miPaqueteUsuario);
        }
        [HttpGet("SalaUsuarioByIdUsuario/{IdUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaUsuarioDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SalaUsuarioDto>>> SalaUsuarioByIdUsuario(int IdUsuario)
        {
            if (IdUsuario <= 0) return BadRequest(ModelState);

            var miSalaUsuario = await _clientMsSala.SalaUsuarioGetAllAsync();
            miSalaUsuario = miSalaUsuario.Where(x => x.IdUsuario.Equals(IdUsuario)).ToList();

            if (miSalaUsuario == null) return NotFound();

            return Ok(miSalaUsuario);
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
