using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaSerpro.Models
{
    [Table("retorno_multa")]
    public class RetornoMulta : RetornoBase
    {
        [Column("placa")]
        public string? Placa { get; set; }

        [Column("renavam")]
        public string? Renavam { get; set; }

        [Column("numero_identificacao_proprietario")]
        public string? NumeroIdentificacaoProprietario { get; set; }

        [Column("codigo_orgao_autuador")]
        public string? CodigoOrgaoAutuador { get; set; }

        [Column("descricao_orgao_autuador")]
        public string? DescricaoOrgaoAutuador { get; set; }

        [Column("numero_auto_infracao")]
        public string? NumeroAutoInfracao { get; set; }

        [Column("codigo_infracao")]
        public string? CodigoInfracao { get; set; }

        [Column("codigo_desdobramento_infracao")]
        public string? CodigoDesdobramentoInfracao { get; set; }

        [Column("descricao_infracao")]
        public string? DescricaoInfracao { get; set; }

        [Column("data_infracao")]
        public DateTime? DataInfracao { get; set; }

        [Column("hora_infracao")]
        public string? HoraInfracao { get; set; }

        [Column("local_ocorrencia_infracao")]
        public string? LocalOcorrenciaInfracao { get; set; }

        [Column("codigo_renainf")]
        public string? CodigoRenainf { get; set; }

        [Column("valor_multa")]
        public Decimal? ValorMulta { get; set; }

        [Column("data_vencimento")]
        public DateTime? DataVencimento { get; set; }
    }
}
