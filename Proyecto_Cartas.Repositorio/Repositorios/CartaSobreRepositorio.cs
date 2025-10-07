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
    public class CartaSobreRepositorio : Repositorio<CartaSobre>, ICartaSobreRepositorio
    {
        private readonly AppDbContext context;

        public CartaSobreRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<CartaSobreDTO?>> ListaCartaSobre()
        {
            var lista = await context.CartasSobre
                .OrderBy(cs => cs.Sobre!.NombreSobre)
                .Select(cs => new CartaSobreDTO
                {
                    NombreSobre = cs.Sobre!.NombreSobre!,
                    NombreCarta = cs.Carta!.NombreCarta!,
                    ProbabilidadCartaSobre = cs.ProbabilidadCartaSobre
                })
                .ToListAsync();
            return lista;
        }

        public async Task<List<CartaSobreDTO?>> ListaCartaSobreNombre(string nombreSobre)
        {
            var lista = await context.CartasSobre
                .Where(cs => cs.Sobre!.NombreSobre == nombreSobre)
                .Select(cs => new CartaSobreDTO
                {
                    NombreSobre = cs.Sobre!.NombreSobre!,
                    NombreCarta = cs.Carta!.NombreCarta!,
                    ProbabilidadCartaSobre = cs.ProbabilidadCartaSobre
                })
                .ToListAsync();
            return lista;
        }

        public async Task<List<CartaSobreDTO?>> ListaCartaSobreNombreCarta(string nombreCarta)
        {
            var lista = await context.CartasSobre
                .Where(cs => cs.Carta!.NombreCarta == nombreCarta)
                .Select(cs => new CartaSobreDTO
                {
                    NombreSobre = cs.Sobre!.NombreSobre!,
                    NombreCarta = cs.Carta!.NombreCarta!,
                    ProbabilidadCartaSobre = cs.ProbabilidadCartaSobre
                })
                .ToListAsync();
            return lista;
        }
    }
}
