using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("musculo")]
    public class Musculo : BaseModel
    {
        [PrimaryKey("id_musculo")]
        public int Id_musc { get; set; }

        [Column("nom_musculo")]
        public string? NomMusc { get; set; }

        [Column("grupo_muscular")]
        public string? GrupMusc { get; set; }
    }
}