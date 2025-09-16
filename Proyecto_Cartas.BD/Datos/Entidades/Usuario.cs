using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    [Index(nameof(Email), Name = "Usuarios_Email_UQ", IsUnique = true)]
    public class Usuario : EntidadBase
    {
        [Required(ErrorMessage = "El email es requerido.")]
        [MaxLength(255, ErrorMessage = "El email no puede exceder los 255 caracteres")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [MaxLength(20, ErrorMessage = "La contraseña no puede exceder los 20 caracteres")]
        public required string PasswordHash { get; set; }

    }
}
