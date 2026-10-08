using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("ejercicio")]
    public class Ejercicio : BaseModel
    {
        [PrimaryKey("id_ejercicio")]
        public int Id_ejercicio { get; set; }

        [Column("nom_ejercicio")]
        public string? NomEjer { get; set; }

        [Column("descripcion")]
        public string? desc { get; set; }

        [Column("tipo_ejercicio")]
        public string? TipEjer { get; set; }

        [Column("musculos_especializos")]
        public string? MuscEsp { get; set; }

        [Column("puntos_ejercicio")]
        public double PuntosE { get; set; }
    }
}