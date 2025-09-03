using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos.Entidades;

namespace Proyecto_Cartas.BD.Datos
{
    public class AppDbContext : DbContext
    {
        public DbSet<Amigo> Amigos { get; set; }
        public DbSet<Billetera> Billeteras { get; set; }
        public DbSet<Carta> Cartas { get; set; }
        public DbSet<CartaApertura> CartasApertura { get; set; }
        public DbSet<CartaSobre> CartasSobre { get; set; }
        public DbSet<CompraSobre> ComprasSobre { get; set; }
        public DbSet<ConfiguracionUsuario> ConfiguracionesUsuario { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<PerfilUsuario> PerfilesUsuario { get; set; }   
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<Sobre> Sobres { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioPartida> UsuariosPartida { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entities here
            // Example: modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
