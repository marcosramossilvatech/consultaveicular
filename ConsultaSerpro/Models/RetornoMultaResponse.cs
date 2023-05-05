namespace ConsultaSerpro.Models
{
    public class RetornoMultaResponse
    {
        public int? Quantidade { get; set; }
        public int? QuantidadeReal { get; set; }
        public List<RetornoMulta> Multas { get; set; }

    }
}
