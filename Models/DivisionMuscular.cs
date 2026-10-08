using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("division_muscular")]
    public class DivisionMuscular : BaseModel
    {
        [PrimaryKey("id_dm")]
        public int IdDm { get; set; }

        [Column("nom_dm")]
        public string? NomDiv { get; set; }

        [Column("score_dm")]
        public string? ScoreDm { get; set; }
    }
}