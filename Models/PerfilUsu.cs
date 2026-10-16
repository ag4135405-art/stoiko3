using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("perfil_usu")]
    public class PerfilUsu : BaseModel
    {
        [PrimaryKey("id_perfil", false)]
        public int IdPerfil { get; set; }

        [Column("nom_perfil")]
        public string NomPerfil { get; set; } = string.Empty;

        [Column("score_total")]
        public double ScoreTotal { get; set; }

        [Column("score_muscular")]
        public double ScoreMuscular { get; set; }

        [Column("id_division")]
        public int? IdDivision { get; set; }

        // Propiedad que solucionará el error CS0117 en las líneas 224
        [Column("numero_rachas")]
        public int? NumeroRachas { get; set; }

        // Propiedad que solucionará los errores CS0117 y CS1061 en las líneas 136 y 221
        [Column("id_usu")]
        public int? IdUsu { get; set; }
    }
}