using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticaCore2Iniciales.Models;

namespace PracticaCore2Iniciales.Data
{
    public class LibrosContext: DbContext
    {
        public LibrosContext(DbContextOptions<LibrosContext> options) : base(options) { }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

    }
}
