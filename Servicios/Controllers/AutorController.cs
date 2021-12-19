using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dominio.Entidades;
using Infraestructura.ContextoDB;
using Aplicacion._1Autor;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutorController : ControllerBase
    {
        private readonly NexosSoftwareContexto _context;
        private Autor_aplic Singletonautoraplicacion = Autor_aplic.GetSingleton();
        public AutorController(NexosSoftwareContexto context)
        {
            _context = context;
            Singletonautoraplicacion._context = _context;
        }

        [HttpGet]
        public IActionResult GetAutores()
        {
            Task<List<Autor>> autores = Singletonautoraplicacion.GetAutores();
            if (autores.Result.Count > 0)
            {
                string mensajesuccess = "La consulta fue exitosa.";
                return Ok(
                    new
                    {
                        mensajesuccess,
                        autores
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
        public IActionResult GetAutor(int id)
        {
            Task<Autor> autor = Singletonautoraplicacion.GetAutor(id);
            if (autor.Result == null)
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
                    autor
                }
            );
        }

        [HttpPost]
        public IActionResult PostAutor(Autor autor)
        {
            Task<string> response;
            if ((response = Singletonautoraplicacion.PostAutor(autor)).Result.Equals("Ok"))
            {
                string mensaje = "El registro del Autor fue exitoso.";
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
        public IActionResult PutAutor(int id, Autor autor)
        {
            Task<string> response;
            if (id != autor.AutorId)
            {
                string mensaje = "Lo sentimos, no coinciden los identificadores.";
                return BadRequest(
                    new
                    {
                        mensaje
                    }
                );
            }
            if ((response = Singletonautoraplicacion.PutAutor(id, autor)).Result.Equals("Ok"))
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
