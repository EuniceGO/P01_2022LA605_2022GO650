using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_2022LA605_2022GO650.Models;

namespace P01_2022LA605_2022GO650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ParqueoContext _parqueoContext;

        public UsuarioController(ParqueoContext parqueocontext)
        {
            _parqueoContext = parqueocontext;
        }

        [HttpPost]
        [Route("RegistrarUsuarios")]
        public IActionResult CrearUsuarios([FromBody] usuario _usuario) 
        {
            try
            {
                _parqueoContext.usuarios.Add(_usuario);
                _parqueoContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            
            
            }

        }


        [HttpGet]
        [Route("ValidacionCredenciales")]
        public IActionResult ValidarCredencials(string usuario, string contraseña) 
        {
            string mensaje;
            usuario? ObjUsuario = (from u in _parqueoContext.usuarios
                                  where u.nombre == usuario && u.contrasena == contraseña
                                  select u).FirstOrDefault();

            if (ObjUsuario != null)
            {
                mensaje = "Credenciales válidas";
                return Ok(new {mensaje });
            }
            else
            {
                mensaje = "Usuario o contraseña incorrectos";
                return NotFound(new { mensaje });
            }
            
        }

        [HttpPut]
        [Route("ActualizartUsuarios")]
        public IActionResult ActualizarUsuarios(int id,[FromBody] usuario _usuario)
        {
           usuario? ObUsuario = (from u in _parqueoContext.usuarios
                                where u.usuario_id == id
                                select u).FirstOrDefault() ;
            if(ObUsuario == null)
            {
                return NotFound();
            }
            ObUsuario.nombre = _usuario.nombre;
            ObUsuario.correo = _usuario.correo;
            ObUsuario.telefono = _usuario.telefono;
            ObUsuario.contrasena = _usuario.contrasena;
            ObUsuario.rol = _usuario.rol;
            _parqueoContext.Entry(ObUsuario).State = EntityState.Modified;
            _parqueoContext.SaveChanges();
            return Ok(ObUsuario);
        }

        [HttpDelete]
        [Route("EliminarUsuario")]
        public IActionResult EliminarUsuario(int id) 
        {
            
            usuario? usuario = (from u in _parqueoContext.usuarios
                                where u.usuario_id == id
                                select u).FirstOrDefault();

            if(usuario == null)
            {
                return NotFound();
            }
            _parqueoContext.usuarios.Attach(usuario);
            _parqueoContext.usuarios.Remove(usuario);
            _parqueoContext.SaveChanges();
            return Ok(usuario);
        }
        [HttpGet]
        [Route("ObtenerUsuarios")]

        public IActionResult Get()
        {
            List<usuario> listadoUsuarios = (from u in _parqueoContext.usuarios
                                                          select u).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }
    }
}
