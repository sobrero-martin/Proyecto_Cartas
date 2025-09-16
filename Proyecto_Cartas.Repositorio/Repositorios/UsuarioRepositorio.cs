using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly AppDbContext context;

        public UsuarioRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Usuario?> GetByEmail(string email)
        {
            return await context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<int> Login(UsuarioAuthDTO login)
        {
            var user = await context.Usuarios.FirstOrDefaultAsync(x => x.Email == login.Email && x.PasswordHash == login.Password);
            if (user == null) return 0;
            return user.Id;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await context.Usuarios.AnyAsync(x => x.Email == email);
        }

        public async Task<int> Register(UsuarioAuthDTO register)
        {

            if (await EmailExists(register.Email)) return 0;

            var newUser = new Usuario
            {
                Email = register.Email,
                PasswordHash = register.Password
            };

            int usuarioId = await Post(newUser);

            var Billetera = new Billetera
            {
                UsuarioID = usuarioId,
                cantidadMonedas = 100 
            };

            await context.Billeteras.AddAsync(Billetera);

            var config = new ConfiguracionUsuario
            {
                UsuarioID = usuarioId,
                VolumenMusica = 50,
                VolumenSFX = 50,
            };

            await context.ConfiguracionesUsuario.AddAsync(config);

            await context.SaveChangesAsync();

            return usuarioId;
        }
    }
}
