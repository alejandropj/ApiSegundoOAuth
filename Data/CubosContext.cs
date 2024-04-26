using ApiSegundoOAuth.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiSegundoOAuth.Data
{
    public class CubosContext: DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options)
        : base(options) { }

        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<CompraCubos> CompraCubos { get; set; }
        public DbSet<UsuarioCubo> UsuariosCubos { get; set; }
    }
}
