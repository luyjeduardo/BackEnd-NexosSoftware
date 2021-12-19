using Infraestructura.ContextoDB;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Aplicacion._1Autor
{
    public class Autor_aplic
    {
        private static Autor_aplic Instancia;
        public NexosSoftwareContexto _context;
        public static Autor_aplic GetSingleton()
        {
            if (Instancia == null)
            {
                Instancia = new Autor_aplic();
            }
            return Instancia;
        }
        public async Task<List<Autor>> GetAutores()
        {
            return await _context.Autores.ToListAsync();
        }
        public async Task<Autor> GetAutor(int id)
        {
            return await _context.Autores.FirstOrDefaultAsync(us => us.AutorId == id);
        }
        public async Task<string> PostAutor(Autor autor)
        {
            try
            {
                if (autor.ValidarPropiedades().Equals("success"))
                {
                    _context.Autores.Add(autor);
                    await _context.SaveChangesAsync();
                    return "Ok";
                }
                else
                {
                    return autor.ValidarPropiedades();
                }
            }
            catch (Exception)
            {
                return "Ha ocurrido un error en el BackEnd y/o Base de datos.";
            }
        }
        public async Task<string> PutAutor(int id, Autor autor)
        {
            if (autor.ValidarPropiedades().Equals("success"))
            {
                _context.Entry(autor).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                    return "Ok";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(id))
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
                return autor.ValidarPropiedades();
            }
        }
        private bool AutorExists(int id)
        {
            return _context.Autores.Any(e => e.AutorId == id);
        }
    }
}
