using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dominio.Entidades;
using Infraestructura.ContextoDB;
using Aplicacion._3Libro;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibroController : ControllerBase
    {
        private readonly NexosSoftwareContexto _context;
        private Libro_aplic Singletonlibroaplicacion = Libro_aplic.GetSingleton();
        public LibroController(NexosSoftwareContexto context)
        {
            _context = context;
            Singletonlibroaplicacion._context = _context;
        }

        [HttpGet]
        public IActionResult GetLibros()
        {
            Task<List<Libro>> libros = Singletonlibroaplicacion.GetLibros();
            if (libros.Result.Count > 0)
            {
                string mensajesuccess = "La consulta fue exitosa.";
                return Ok(
                    new
                    {
                        mensajesuccess,
                        libros
                    }
                );
            }
            else
            {
                string mensaje = "Consulta sin resultados.";
                return NotFound(
                   new
                   {
                       mensaje
                   }
                );
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLibro(int id)
        {
            Task<Libro> libro = Singletonlibroaplicacion.GetLibro(id);
            if (libro.Result == null)
            {
                string mensaje = "Consulta sin resultados.";
                return NotFound(
                   new
                   {
                       mensaje
                   }
                );
            }
            string mensajesuccess = "La consulta fue exitosa.";
            return Ok(
                new
                {
                    mensajesuccess,
                    libro
                }
            );
        }

        [HttpPost]
        public IActionResult PostLibro(Libro libro)
        {
            Task<string> response;
            if ((response = Singletonlibroaplicacion.PostLibro(libro)).Result.Equals("Ok"))
            {
                string mensaje = "El registro del libro fue exitoso.";
                return Ok(
                    new
                    {
                        response,
                        mensaje
                    }
                );
            }
            else
            {
                return BadRequest(
                    new
                    {
                        response.Result
                    }
                );
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutLibro(int id, Libro libro)
        {
            Task<string> response;
            if (id != libro.LibroId)
            {
                string mensaje = "Lo sentimos, no coinciden los identificadores.";
                return BadRequest(
                    new
                    {
                        mensaje
                    }
                );
            }
            if ((response = Singletonlibroaplicacion.PutLibro(id, libro)).Result.Equals("Ok"))
            {
                string mensaje = "La modificación fue exitosa.";
                return Ok(
                    new
                    {
                        response,
                        mensaje
                    }
                );
            }
            else if (response.Result.Equals("Not found") || response.Result.Equals("Error"))
            {
                var mensaje = "";
                if (response.Result.Equals("Not found"))
                    mensaje = "Los id no coincidieron con ningún registro de la Base de datos.";
                else if (response.Result.Equals("Error"))
                    mensaje = "Ocurrió un error con la Base de datos.";
                return NotFound(
                    new
                    {
                        mensaje
                    }
                );
            }
            else
            {
                return BadRequest(
                    new
                    {
                        response.Result
                    }
                );
            }
        }
    }
}
