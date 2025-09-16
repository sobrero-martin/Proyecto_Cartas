using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class BilleteraRepositorio : Repositorio<Billetera>, IBilleteraRepositorio
    {
        private readonly AppDbContext context;

        public BilleteraRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
