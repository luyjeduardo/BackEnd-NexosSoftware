using Dominio.Entidades;
using Infraestructura.ContextoDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion._3Libro
{
    public class Libro_aplic
    {
        private static Libro_aplic Instancia;
        public NexosSoftwareContexto _context;
        public static Libro_aplic GetSingleton()
        {
            if (Instancia == null)
            {
                Instancia = new Libro_aplic();
            }
            return Instancia;
        }
        public async Task<List<Libro>> GetLibros()
        {
            return await _context.Libros.ToListAsync();
        }
        public async Task<Libro> GetLibro(int id)
        {
            return await _context.Libros.FirstOrDefaultAsync(us => us.LibroId == id);
        }
        public async Task<string> PostLibro(Libro libro)
        {
            try
            {
                if (libro.ValidarPropiedades().Equals("success"))
                {
                    Genero genero = await _context.Generos.FirstOrDefaultAsync(g => g.GeneroId == libro.GeneroId);
                    Autor autor = await _context.Autores.FirstOrDefaultAsync(a => a.AutorId == libro.AutorId);
                    if (genero != null)
                    {
                        if (autor != null)
                        {
                            _context.Libros.Add(libro);
                            await _context.SaveChangesAsync();
                            return "Ok";
                        }
                        else
                        {
                            return "No existe el autor en la base de datos.";
                        }
                    }
                    else
                    {
                        return "No existe el genero en la base de datos.";
                    }     
                }
                else
                {
                    return libro.ValidarPropiedades();
                }
            }
            catch (Exception)
            {
                return "Ha ocurrido un error en el BackEnd y/o Base de datos.";
            }
        }
        public async Task<string> PutLibro(int id, Libro libro)
        {
            if (libro.ValidarPropiedades().Equals("success"))
            {
                _context.Entry(libro).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                    return "Ok";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(id))
                    {
                        return "Not found";
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
            else
            {
                return libro.ValidarPropiedades();
            }
        }
        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.LibroId == id);
        }
    }
}
