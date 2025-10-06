using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class EstadoCartaRepositorio : Repositorio<EstadoCarta>, IEstadoCartaRepositorio
    {
        private readonly AppDbContext context;

        public EstadoCartaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
