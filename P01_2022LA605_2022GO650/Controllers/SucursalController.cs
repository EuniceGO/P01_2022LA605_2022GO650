using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_2022LA605_2022GO650.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_2022LA605_2022GO650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly ParqueoContext _context;

        public SucursalesController(ParqueoContext context)
        {
            _context = context;
        }

        // GET: api/Sucursales
        [HttpGet]
        
        public IActionResult Get()
        {
            List<Sucursal> listadoUsuarios = (from e in _context.Sucursales
                                                   select e).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }


        // GET: api/Sucursales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursal>> GetSucursal(int id)
        {
            var sucursal = await _context.Sucursales.Include(s => s.Administrador)
                                                     .FirstOrDefaultAsync(s => s.SucursalId == id);

            if (sucursal == null)
            {
                return NotFound();
            }

            return sucursal;
        }

        // POST: api/Sucursales
        [HttpPost]
        public async Task<ActionResult<Sucursal>> PostSucursal(Sucursal sucursal)
        {
            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSucursal", new { id = sucursal.SucursalId }, sucursal);
        }

        // PUT: api/Sucursales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSucursal(int id, Sucursal sucursal)
        {
            if (id != sucursal.SucursalId)
            {
                return BadRequest();
            }

            _context.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Sucursales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSucursal(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            _context.Sucursales.Remove(sucursal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SucursalExists(int id)
        {
            return _context.Sucursales.Any(e => e.SucursalId == id);
        }
    }
}
