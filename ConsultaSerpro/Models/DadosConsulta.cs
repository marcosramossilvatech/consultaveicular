using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaSerpro.Models
{
    [Table("dado_consulta")]
    public class DadosConsulta : Base
    {
        [Column("id_empresa")]
        public long IdEmpresa { get;  set; }

        [Column("id_usuario_empresa")]
        public int IdUsuarioEmpresa { get;  set; }

        [Column("id_servico")]
        public int IdServico { get;  set; }

        public DadosConsulta(long idEmpresa,int idUsuarioEmpresa, int idServico )
        {
            IdEmpresa = idEmpresa;
            IdUsuarioEmpresa = idUsuarioEmpresa;
            IdServico = idServico;            
        }
    }
}
