using Supabase.Postgrest.Attributes;
 using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;
namespace stoiko3.Models
{
     [Table("perfil_usu")]
    public class PerfilUsu : BaseModel
    {
        [PrimaryKey("id_perfil")]
         [Key]
        public int IdPerfil { get; set; }

         [Column("nom_perfil")]
        public string NomPerfil { get; set; } = string.Empty;

         [Column("score_total")]
        public double ScoreTotal { get; set; }

         [Column("score_muscular")]
        public double ScoreMuscular { get; set; }
    }
}