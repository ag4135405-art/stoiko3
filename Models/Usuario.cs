using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("usuario")]
    public class Usuario : BaseModel
    {
        // 1. Identificador (Mapeado a id_usu, soporta Id e IdUsu)
        [PrimaryKey("id_usu", false)]
        public int Id { get; set; }

        public int IdUsu
        {
            get => Id;
            set => Id = value;
        }

        // 2. Correo Electrónico (Mapeado a email_usu, soporta Email e EmailUsu)
        [Column("email_usu")]
        public string Email { get; set; } = string.Empty;

        public string EmailUsu
        {
            get => Email;
            set => Email = value;
        }

        // 3. Contraseña (Mapeado a pass_usu, soporta PassUsu, PasswordHash y PasswordUsu)
        [Column("pass_usu")]
        public string PassUsu { get; set; } = string.Empty;

        public string PasswordHash
        {
            get => PassUsu;
            set => PassUsu = value;
        }

        public string PasswordUsu
        {
            get => PassUsu;
            set => PassUsu = value;
        }

        // 4. Fechas de control de tu base de datos
        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        [Column("fecha_iniciosecion")]
        public DateTime FechaInicioSecion { get; set; } = DateTime.UtcNow;
    }
}