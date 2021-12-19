using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.ContextoDB
{
    public class NexosSoftwareContexto : DbContext
    {
        public NexosSoftwareContexto(DbContextOptions<NexosSoftwareContexto> chousen) : base(chousen)
        {
        } 
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
