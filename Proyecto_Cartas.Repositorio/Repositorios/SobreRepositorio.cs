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
    public class SobreRepositorio : Repositorio<Sobre>, ISobreRepositorio
    {
        private readonly AppDbContext context;

        public SobreRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<SobreDTO?> NombreSobre(string nombre)
        {
            var sobre = await context.Sobres
                .Where(s => s.NombreSobre == nombre)
                .Select(s => new SobreDTO
                {
                    Id = s.Id,
                    Nombre = s.NombreSobre!,
                    CantidadCartas = s.CantidadCartas!
                })
                .FirstOrDefaultAsync();
            return sobre;
        }

        public async Task<List<SobreDTO?>> ListaNombreSobre()
        {
            var lista = await context.Sobres
                .Select(s => new SobreDTO
                {
                    Id = s.Id!,
                    Nombre = s.NombreSobre!,
                    CantidadCartas = s.CantidadCartas!
                })
                .ToListAsync();
            return lista;
        }
    }
}
