using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IPerfilUsuarioRepositorio : IRepositorio<PerfilUsuario>
    {
        Task<PerfilUsuarioDTO?> GetPerfilByName(string nombre);

        Task<PerfilUsuarioDTO?> GetPerfilByUserId(int id);

        Task<PerfilUsuarioDTO?> GetPerfilById(int id);
    }
}
