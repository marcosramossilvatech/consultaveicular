using ConsultaSerpro.Uteis;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsultaSerpro.Models
{
    [Table("retorno_validacao_cnh")]
    public class RetornoCNH : RetornoBase
    {
        [Column("nome_mae")]
        public string? NomeMae { get; set; }

        [Column("data_emissao")]
        public string? DataEmissao { get; set; }

        [Column("nome_condutor")]
        public string? NomeCondutor { get; set; }

        [Column("numero_registro")]
        public string? NumeroRegistro { get; set; }

        [Column("numero_formulario_cnh")]
        public string? NumeroFormularioCnh { get; set; }

        [Column("data_validade")]
        public string? DataValidade { get; set; }

        [Column("categoria")]
        public string? Categoria { get; set; }

        [Column("numero_seguranca")]
        public string? NumeroSeguranca { get; set; }

        [Column("numero_formulario_renach")]
        public string? NumeroFormularioRenach { get; set; }

        [Column("data_nascimento")]
        public string? DataNascimento { get; set; }

        [Column("codigo_nacionalidade")]
        public string? CodigoNacionalidade { get; set; }

        [Column("descricao_nacionalidade")]
        public string? DescricaoNacionalidade { get; set; }

        [Column("nome_pai")]
        public string? NomePai { get; set; }

        [Column("endereco_uf")]
        public string? EnderecoUf { get; set; }

        [Column("data_primeirahabilitacao")]
        public string? DataPrimeiraHabilitacao { get; set; }

        [Column("uf_habilitacaoatual")]
        public string? UfHabilitacaoAtual { get; set; }

        [Column("situacao")]
        public string? Situacao { get; set; }
    }
}
