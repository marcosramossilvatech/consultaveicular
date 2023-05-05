using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaSerpro.Models
{
    [Table("retorno_veiculo")]
    public class RetornoVeiculo : RetornoBase
    {

        [Column("placa")]
        public string? Placa { get; set; }

        [Column("codigo_renavam")]
        public string? CodigoRenavam { get; set; }

        [Column("situacao")]
        public string? Situacao { get; set; }

        [Column("codigo_municipio_emplacamento")]
        public string? CodigoMunicipioEmplacamento { get; set; }

        [Column("descricao_municipio_emplacamento")]
        public string? DescricaoMunicipioEmplacamento { get; set; }

        [Column("uf_jurisdicao")]
        public string? UfJurisdicao { get; set; }

        [Column("codigo_remarcacao_chassi")]
        public string? CodigoRemarcacaoChassi { get; set; }

        [Column("descricao_remarcacao_chassi")]
        public string? DescricaoRemarcacaoChassi { get; set; }

        [Column("codigo_tipo_veiculo")]
        public string? CodigoTipoVeiculo { get; set; }

        [Column("descricao_tipo_veiculo")]
        public string? DescricaoTipoVeiculo { get; set; }

        [Column("codigo_marca_modelo")]
        public string? CodigoMarcaModelo { get; set; }

        [Column("descricao_marca_modelo")]
        public string? DescricaoMarcaModelo { get; set; }

        [Column("codigo_especie_veiculo")]
        public string? CodigoEspecieVeiculo { get; set; }

        [Column("descricao_especie_veiculo")]
        public string? DescricaoEspecieVeiculo { get; set; }

        [Column("codigo_tipo_carroceria")]
        public string? CodigoTipoCarroceria { get; set; }

        [Column("descricao_tipo_carroceria")]
        public string? DescricaoTipoCarroceria { get; set; }

        [Column("codigo_cor")]
        public string? CodigoCor { get; set; }

        [Column("descricao_cor")]
        public string? DescricaoCor { get; set; }

        [Column("codigo_categoria")]
        public string? CodigoCategoria { get; set; }

        [Column("descricao_categoria")]
        public string? DescricaoCategoria { get; set; }

        [Column("ano_modelo")]
        public int? AnoModelo { get; set; }

        [Column("ano_fabricacao")]
        public int? AnoFabricacao { get; set; }

        [Column("potencia")]
        public int? Potencia { get; set; }

        [Column("cilindradas")]
        public int? Cilindradas { get; set; }


        [Column("codigo_combustivel")]
        public string? CodigoCombustivel { get; set; }


        [Column("descricao_combustivel")]
        public string? DescricaoCombustivel { get; set; }

        [Column("cmt")]
        public decimal? Cmt { get; set; }

        [Column("pbt")]
        public decimal? Pbt { get; set; }

        [Column("cmc")]
        public decimal? Cmc { get; set; }

        [Column("procedencia")]
        public string? Procedencia { get; set; }

        [Column("numero_cambio")]
        public string? NumeroCambio { get; set; }

        [Column("numero_identificacao_proprietario")]
        public string? NumeroIdentificacaoProprietario { get; set; }

        [Column("codigo_tipo_arrendatario")]
        public string? CodigoTipoArrendatario { get; set; }

        [Column("descricao_tipo_arrendatario")]
        public string? DescricaoTipoArrendatario { get; set; }

        [Column("numero_identificacao_arrendatario")]
        public string? NumeroIdentificacaoArrendatario { get; set; }

        [Column("nome_arrendatario")]
        public string? NomeArrendatario { get; set; }

        [Column("numero_carroceria")]
        public string? NumeroCarroceria { get; set; }

        [Column("qtd_eixos")]
        public int? QtdEixos { get; set; }

        [Column("lotacao")]
        public int? Lotacao { get; set; }

        [Column("data_ultima_atualizacao_importacao")]
        public string? DataUltimaAtualizacaoImportacao { get; set; }

        [Column("indicador_siniav")]
        public bool? IndicadorSiniav { get; set; }

        [Column("descricao_restricao_rfb")]
        public string? DescricaoRestricaoRfb { get; set; }

    }
}
