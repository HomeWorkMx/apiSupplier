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
    public class MembresiaController : Controller
    {
        private msMembresiaClient _clientMsMembresia;
        private readonly IMemoryCache _memoryCache;
        public MembresiaController(msMembresiaClient clientMsMembresia, IMemoryCache memoryCache)
        {
            _clientMsMembresia = clientMsMembresia;
            _memoryCache = memoryCache;
        }

        [HttpGet("MembresiaGetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaGetAll()
        {
            var entidades = await _clientMsMembresia.MembresiaGetAllAsync();
            /*_memoryCache.GetOrCreateAsync("MembresiaGetAllAsync", entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                    entry.Priority = CacheItemPriority.Normal;
                    var membresias = _clientMsMembresia.MembresiaGetAllAsync();
                    return _clientMsMembresia.MembresiaGetAllAsync();
                });*/
            if (entidades == null) return NotFound();

            return Ok(entidades);
        }

        [HttpGet("MembresiaGet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaGet(int id)
        {
            if (id <= 0) return BadRequest(ModelState);
            var entidad = await _clientMsMembresia.MembresiaGetAsync(id);

           /* var entidad = await
               _memoryCache.GetOrCreateAsync("MembresiaGetAsync" + id.ToString(), entry =>
                 {
                     entry.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                     entry.Priority = CacheItemPriority.Normal;
                     return _clientMsMembresia.MembresiaGetAsync(id);
                 });*/

            if (entidad == null) return NotFound();
            return Ok(entidad);
        }

        [HttpGet("MembresiaGetByIdUsuario/{IdUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaGetByIdUsuario(int IdUsuario)
        {
            if (IdUsuario <= 0) return BadRequest(ModelState);
            List<MembresiaDto> entidades = new List<MembresiaDto>();
            var misMembresiaUsuario = await _clientMsMembresia.MembresiaUsuarioGetAllAsync();
            misMembresiaUsuario = misMembresiaUsuario.Where(x => x.IdUsuario.Equals(IdUsuario)).ToList();
            foreach (MembresiaUsuarioDto membresiaUsuario in misMembresiaUsuario)
            {
                entidades.Add(await _clientMsMembresia.MembresiaGetAsync((int)membresiaUsuario.IdMembresia));
            }
            return Ok(entidades);
        }
        [HttpGet("MembresiaGetByIdProveedor/{IdProveedor}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaGetByIdProveedor(int IdProveedor)
        {
            if (IdProveedor <= 0) return BadRequest(ModelState);

            var miMembresiaProveedor = await _clientMsMembresia.MembresiaProveedorGetAllAsync();
            miMembresiaProveedor = miMembresiaProveedor.Where(x => x.IdUsuario.Equals(IdProveedor)).ToList();

            if (miMembresiaProveedor == null) return NotFound();

            List<MembresiaDto> entidades = new List<MembresiaDto>();

            foreach (MembresiaProveedorDto membresiaProveedor in miMembresiaProveedor)
            {
                entidades.Add(await _clientMsMembresia.MembresiaGetAsync((int)membresiaProveedor.IdMembresia));
            }

            if (entidades == null) return NotFound();

            return Ok(entidades);
        }

        [HttpPost("MembresiaSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaSave(MembresiaDto input)
        {
            try
            {
                if (input == null) return BadRequest(input);
                var entidad = await _clientMsMembresia.MembresiaSaveAsync(input);
                if (entidad == null) return NotFound();

                //var memProveedor = await _clientMsMembresia.MembresiaProveedorInsertAsync(new MembresiaProveedorDto()
                //{
                //    IdMembresiaUsuario = 0,
                //    BorradoLogico = 0,
                //    IdMembresia = entidad.IdMembresia,
                //    IdUsuario = idUsuario
                //});

                //if (memProveedor == null) return NotFound();
                return Ok(entidad);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        [HttpPost("MembresiaSaveProveedor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaSaveProveedor(MembresiaDto input, int idProveedor)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaSaveAsync(input);
            if (entidad == null) return NotFound();

            var memProveedor = await _clientMsMembresia.MembresiaProveedorInsertAsync(new MembresiaProveedorDto()
            {
                IdMembresiaUsuario = 0,
                BorradoLogico = 0,
                IdMembresia = entidad.IdMembresia,
                IdUsuario = idProveedor
            });

            if (memProveedor == null) return NotFound();
            return Ok(entidad);
        }

       
        [HttpPost("MembresiaInsert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaInsert(MembresiaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaInsertAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
        [HttpPut("MembresiaUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MembresiaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<MembresiaDto>>> MembresiaUpdate(MembresiaDto input)
        {
            if (input == null) return BadRequest(input);
            var entidad = await _clientMsMembresia.MembresiaUpdateAsync(input);
            if (entidad == null) return NotFound();
            return Ok(entidad);
        }
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
