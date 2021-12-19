using Dominio.Entidades;
using Infraestructura.ContextoDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion._2Genero
{
    public class Genero_aplic
    {
        private static Genero_aplic Instancia;
        public NexosSoftwareContexto _context;
        public static Genero_aplic GetSingleton()
        {
            if (Instancia == null)
            {
                Instancia = new Genero_aplic();
            }
            return Instancia;
        }
        public async Task<List<Genero>> GetGeneros()
        {
            return await _context.Generos.ToListAsync();
        }
        public async Task<Genero> GetGenero(int id)
        {
            return await _context.Generos.FirstOrDefaultAsync(us => us.GeneroId == id);
        }
        public async Task<string> PostGenero(Genero genero)
        {
            try
            {
                if (genero.ValidarPropiedades().Equals("success"))
                {
                    _context.Generos.Add(genero);
                    await _context.SaveChangesAsync();
                    return "Ok";
                }
                else
                {
                    return genero.ValidarPropiedades();
                }
            }
            catch (Exception)
            {
                return "Ha ocurrido un error en el BackEnd y/o Base de datos.";
            }
        }
        public async Task<string> PutGenero(int id, Genero genero)
        {
            if (genero.ValidarPropiedades().Equals("success"))
            {
                _context.Entry(genero).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                    return "Ok";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneroExists(id))
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
                return genero.ValidarPropiedades();
            }
        }
        private bool GeneroExists(int id)
        {
            return _context.Generos.Any(e => e.GeneroId == id);
        }
    }
}
