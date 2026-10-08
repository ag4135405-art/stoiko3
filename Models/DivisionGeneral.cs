using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("division_general")]
    public class DivisionGeneral : BaseModel
    {
        [PrimaryKey("id_division")]
        public int IdDivision { get; set; }

        [Column("nom_division")]
        public string NomDivision { get; set; } = string.Empty;

        [Column("score_division")]
        public double ScoreDivision { get; set; }
    }
}