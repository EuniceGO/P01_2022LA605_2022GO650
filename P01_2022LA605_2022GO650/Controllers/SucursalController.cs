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

        [HttpGet]
        [Route("Obtener")]
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

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarSucursal([FromBody] Sucursal sucursal)
        {
            try
            {
                _context.Sucursales.Add(sucursal);
                _context.SaveChanges();
                return Ok(sucursal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarComentario(int id, [FromBody] Sucursal sucursal)
        {
            
            Sucursal? objSucursal = _context.Sucursales
                .FirstOrDefault(e => e.sucursal_id == id);

           
            if (objSucursal == null)
            {
                return NotFound();
            }

           
            objSucursal.Nombre = sucursal.Nombre;
            objSucursal.Direccion = sucursal.Direccion;
            objSucursal.Telefono = sucursal.Telefono;
            objSucursal.administrador_id = sucursal.administrador_id;
            objSucursal.num_espacios = sucursal.num_espacios;

            
            _context.Entry(objSucursal).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(objSucursal);
        }



        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            
            Sucursal? objsucursal = (from e in _context.Sucursales
                                         where e.sucursal_id == id
                                         select e).FirstOrDefault();

            
            if (objsucursal == null)
                return NotFound();

            _context.Sucursales.Attach(objsucursal);
            _context.Sucursales.Remove(objsucursal);
            _context.SaveChanges();

            return Ok(objsucursal);
        }

       
    }
}
