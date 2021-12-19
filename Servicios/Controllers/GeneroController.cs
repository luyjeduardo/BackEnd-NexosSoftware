using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dominio.Entidades;
using Infraestructura.ContextoDB;
using Aplicacion._2Genero;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeneroController : ControllerBase
    {
        private readonly NexosSoftwareContexto _context;
        private Genero_aplic Singletongeneroaplicacion = Genero_aplic.GetSingleton();
        public GeneroController(NexosSoftwareContexto context)
        {
            _context = context;
            Singletongeneroaplicacion._context = _context;
        }

        [HttpGet]
        public IActionResult GetGeneros()
        {
            Task<List<Genero>> generos = Singletongeneroaplicacion.GetGeneros();
            if (generos.Result.Count > 0)
            {
                string mensajesuccess = "La consulta fue exitosa.";
                return Ok(
                    new
                    {
                        mensajesuccess,
                        generos
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
        public IActionResult GetGenero(int id)
        {
            Task<Genero> genero = Singletongeneroaplicacion.GetGenero(id);
            if (genero.Result == null)
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
                    genero
                }
            );
        }

        [HttpPost]
        public IActionResult PostGenero(Genero genero)
        {
            Task<string> response;
            if ((response = Singletongeneroaplicacion.PostGenero(genero)).Result.Equals("Ok"))
            {
                string mensaje = "El registro del género fue exitoso.";
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
        public IActionResult PutGenero(int id, Genero genero)
        {
            Task<string> response;
            if (id != genero.GeneroId)
            {
                string mensaje = "Lo sentimos, no coinciden los identificadores.";
                return BadRequest(
                    new
                    {
                        mensaje
                    }
                );
            }
            if ((response = Singletongeneroaplicacion.PutGenero(id, genero)).Result.Equals("Ok"))
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
