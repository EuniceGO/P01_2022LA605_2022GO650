using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_2022LA605_2022GO650.Models;

namespace P01_2022LA605_2022GO650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ParqueoContext _context;

        public ReservaController(ParqueoContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("ReservarEspacio")]
        public IActionResult ReservarEspacio(int espacioId, DateTime fecha, int cantidadHoras, int id)
        {
 
            var espacio = _context.EspaciosParqueo.FirstOrDefault(e => e.espacio_id == espacioId && e.estado == "disponible");

            if (espacio == null)
            {
                return BadRequest("El espacio no está disponible para reservar.");
            }

            
            var horaReserva = fecha.Hour;  

            var nuevaReserva = new reserva
            {
                espacio_id = espacioId,
                fecha_reserva = fecha.Date, 
                hora_reserva = horaReserva, 
                cantidad_horas = cantidadHoras,
                usuario_id = id,
                estado = "activa"  
            };


            _context.Reservas.Add(nuevaReserva);

            espacio.estado = "ocupado";
            _context.SaveChanges();

            return Ok(new { mensaje = "Reserva realizada con éxito", reservaId = nuevaReserva.reserva_id });
        }




        [HttpGet]
        [Route("ReservaActivas")]
        public IActionResult ReservasActivas(int id)
        {
            var _reserva = (from r in _context.Reservas
                                where  r.usuario_id == id && r.estado == "activa"
                                select new
                                {
                                    r.usuario_id,
                                    r.reserva_id,
                                    r.estado
                                }).ToList();
            if(_reserva == null)
            {
                return NotFound();
            }
            return Ok(_reserva);
        }

        [HttpGet]
        [Route("ListaEspaciosDia")]
        public IActionResult EspaciosReservados(DateTime fechaDia)
        {
            var espaciosReservados = (from r in _context.Reservas
                                      join ee in _context.EspaciosParqueo on r.espacio_id equals ee.espacio_id
                                      where r.fecha_reserva.Date == fechaDia.Date 
                                            && ee.estado == "ocupado" 
                                      select new
                                      {
                                          r.reserva_id,
                                          r.espacio_id,
                                          r.fecha_reserva,
                                          r.estado,
                                          r.usuario_id,
                                          ee.sucursal_id,
                                      }).ToList();
            if (espaciosReservados == null)
            {
                return NotFound();
            }
            return Ok(espaciosReservados);
        }

        [HttpGet]
        [Route("EspacioXFechas")]
        public IActionResult EspaciosFechas(DateTime fecha1, DateTime fecha2, int sucursalId)
        {
            var _reserva = (from r in _context.Reservas
                                      join ee in _context.EspaciosParqueo on r.espacio_id equals ee.espacio_id
                                      where r.fecha_reserva >= fecha1 && r.fecha_reserva <= fecha2
                                            && ee.sucursal_id == sucursalId 
                                            && ee.estado == "ocupado" 
                                      select new
                                      {
                                          r.reserva_id,
                                          r.espacio_id,
                                          r.fecha_reserva,
                                          ee.estado,
                                          ee.sucursal_id,
                                         
                                      }).ToList();

            if (_reserva == null)
            {
                return NotFound();
            }
            return Ok(_reserva);
        }


        [HttpPut]
        [Route("CancelarReserva/{id}")]
        public IActionResult CancelarReserva(int id)
        {
            var reserva = _context.Reservas.Find(id);
            if (reserva == null)
            {
                return NotFound("No se encontró la reserva.");
            }

            if (reserva.estado == "Cancelada" || reserva.estado == "Usada")
            {
                return BadRequest("La reserva ya está cancelada o ha sido usada.");
            }

            if (reserva.fecha_reserva < DateTime.Now)
            {
                return BadRequest("No se puede cancelar una reserva que ya ha pasado.");
            }

            reserva.estado = "Cancelada";

            var espacio = _context.EspaciosParqueo.Find(reserva.espacio_id);
            if (espacio != null)
            {
                espacio.estado = "Disponible";
            }

            return Ok(new
            {
                Message = "Reserva cancelada exitosamente.",
                Reserva = reserva,
                Espacio = espacio
            });
        }
    }
}
