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
    public class espacioParqueoController : ControllerBase
    {
        private readonly ParqueoContext _context;

        public espacioParqueoController(ParqueoContext context)
        {
            _context = context;
        }

        // GET: api/espacioParqueo/ObtenerDisponibles
        [HttpGet]
        [Route("ObtenerDisponibles")]
        public IActionResult ObtenerDisponibles()
        {
            List<espacioParqueo> espaciosDisponibles = _context.EspaciosParqueo
                .Where(e => e.estado == "disponible")
                .ToList();

            if (espaciosDisponibles.Count == 0)
            {
                return NotFound("No hay espacios de parqueo disponibles.");
            }

            return Ok(espaciosDisponibles);
        }

        // GET: api/espacioParqueo/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var espacioParqueo = _context.EspaciosParqueo
                .FirstOrDefault(e => e.espacio_id == id);

            if (espacioParqueo == null)
            {
                return NotFound();
            }

            return Ok(espacioParqueo);
        }

        // POST: api/espacioParqueo/Add
        [HttpPost]
        [Route("Add")]
        public IActionResult CrearEspacio([FromBody] espacioParqueo espacioParqueo)
        {
            try
            {
                // Verificar que la sucursal exista
                var sucursal = _context.Sucursales
                    .FirstOrDefault(s => s.sucursal_id == espacioParqueo.sucursal_id);

                if (sucursal == null)
                {
                    return NotFound("Sucursal no encontrada.");
                }

                // Agregar el nuevo espacio de parqueo
                _context.EspaciosParqueo.Add(espacioParqueo);
                _context.SaveChanges();

                return Ok(espacioParqueo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/espacioParqueo/actualizar/{id}
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEspacio(int id, [FromBody] espacioParqueo espacioParqueo)
        {
            var objEspacioParqueo = _context.EspaciosParqueo
                .FirstOrDefault(e => e.espacio_id == id);

            if (objEspacioParqueo == null)
            {
                return NotFound();
            }

            // Actualizar los campos del espacio de parqueo
            objEspacioParqueo.numero_espacio = espacioParqueo.numero_espacio;
            objEspacioParqueo.ubicacion = espacioParqueo.ubicacion;
            objEspacioParqueo.costo_hora = espacioParqueo.costo_hora;
            objEspacioParqueo.estado = espacioParqueo.estado;

            // Marcar el registro como modificado y guardar cambios
            _context.Entry(objEspacioParqueo).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(objEspacioParqueo);
        }

        // DELETE: api/espacioParqueo/eliminar/{id}
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEspacio(int id)
        {
            var objEspacioParqueo = _context.EspaciosParqueo
                .FirstOrDefault(e => e.espacio_id == id);

            if (objEspacioParqueo == null)
            {
                return NotFound();
            }

            // Eliminar el espacio de parqueo
            _context.EspaciosParqueo.Remove(objEspacioParqueo);
            _context.SaveChanges();

            return Ok(objEspacioParqueo);
        }
    }
}
