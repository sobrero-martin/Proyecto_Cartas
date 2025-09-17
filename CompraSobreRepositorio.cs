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
    public class CompraSobreRepositorio : Repositorio<CompraSobre>, ICompraSobreRepositorio
    {
        private readonly AppDbContext context;

        public CompraSobreRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<CompraSobreMostrarDTO?>> ListaCompraSobre()
        {
            var lista = await context.ComprasSobre
                .OrderBy(cs => cs.FechaCompra)
                .Select(cs => new CompraSobreMostrarDTO
                {
                    FechaCompra = cs.FechaCompra,
                    NombreUsuario = cs.Usuario!.Nombre!,
                    NombreSobre = cs.Sobre!.NombreSobre!
                })
                .ToListAsync();
            return lista;
        }

        public async Task<List<CompraSobreMostrarDTO?>> FechaDeCompra(DateTime fecha)
        {
            var lista = await context.ComprasSobre
                .Where(cs => cs.FechaCompra.Date == fecha.Date)
                .OrderBy(cs => cs.FechaCompra)
                .Select(cs => new CompraSobreMostrarDTO
                {
                    FechaCompra = cs.FechaCompra,
                    NombreUsuario = cs.Usuario!.Nombre!,
                    NombreSobre = cs.Sobre!.NombreSobre!
                })
                .ToListAsync();
            return lista;
        }

        public async Task<List<CompraSobreMostrarDTO?>> ListaCompraSobreUsuario(string nombreUsuario)
        {
            var lista = await context.ComprasSobre
                .Where(cs => cs.Usuario!.Nombre == nombreUsuario)
                .Select(cs => new CompraSobreMostrarDTO
                {

                    NombreUsuario = cs.Usuario!.Nombre!,
                    NombreSobre = cs.Sobre!.NombreSobre!,
                    FechaCompra = cs.FechaCompra
                })
                .ToListAsync();
            return lista;
        }

        public async Task<List<CompraSobreMostrarDTO?>> ListaQuienComproSobre(string nombreSobre)
        {
            var lista = await context.ComprasSobre
                .Where(cs => cs.Sobre!.NombreSobre == nombreSobre)
                .Select(cs => new CompraSobreMostrarDTO
                {
                    NombreUsuario = cs.Usuario!.Nombre!,
                    NombreSobre = cs.Sobre!.NombreSobre!,
                    FechaCompra = cs.FechaCompra
                })
                .ToListAsync();
            return lista;
        }
    }
}
