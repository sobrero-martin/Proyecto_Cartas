using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    [Index(nameof(Nombre), Name = "PerfilesUsuario_Nombre_UQ", IsUnique = true)]
    public class PerfilUsuario : EntidadBase
    {
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "El nombre de perfil es requerido.")]
        [MaxLength(20, ErrorMessage = "El nombre de perfil no puede exceder los 20 caracteres")]
        public required string Nombre { get; set; }

        public string Bio { get; set; } = string.Empty;

        public  int Nivel { get; set; } = 1;

        public  int Experiencia { get; set; } = 0;

        public  int PartidasJugadas { get; set; } = 0;

        public  int PartidasGanadas { get; set; } = 0;

        public  DateTime UltimoLogin { get; set; } = DateTime.UtcNow;
    }
}
