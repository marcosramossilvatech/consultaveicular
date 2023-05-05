using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ConsultaSerpro.Models
{
    [Table("cnh_vinculousuario")]
    public class CnhVinculoUsuario
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("cpf")]
        public string CPF { get; set; }

        [Column("ativo")]
        public bool Ativo { get; set; }

        [Column("consulta")]
        public bool Consulta { get; set; }

        [Column("empresa")]
        public long Empresa { get; set; }

        [Column("usuario")]
        public int Usuario { get; set; }
    }
}
