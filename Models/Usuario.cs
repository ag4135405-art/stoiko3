using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("usuario")]
    public class Usuario : BaseModel
    {
        [PrimaryKey("id_usu")]
        public int IdUsu { get; set; }

        [Column("email_usu")]
        public string EmailUsu { get; set; } = string.Empty;

        [Column("pass_usu")]
        public string PasswordUsu { get; set; } = string.Empty;

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }

        [Column("fecha_iniciosecion")]
        public DateTime FechaInicioSecion { get; set; }
    }
}