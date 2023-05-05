using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsultaSerpro.Models
{
    public abstract class RetornoBase
    {
        [Column("id")]
        [JsonIgnore]
        public int Id { get; set; }

        [Column("id_consulta")]
        [JsonIgnore]
        public long IdConsulta { get; set; }

        [Column("retorno")]
        [JsonIgnore]
        public string Retorno { get; set; }
    }
}
