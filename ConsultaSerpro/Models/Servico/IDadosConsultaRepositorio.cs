namespace ConsultaSerpro.Models.Servico
{
    public interface IDadosConsultaRepositorio
    {
        Task<DadosConsulta?> AdicionarAsync(DadosConsulta consulta);
        Task<DadosConsulta> Alterar(DadosConsulta consulta);
        void Alterar(Task<DadosConsulta?> dados);
        Task<IEnumerable<DadosConsulta>> RetornarDadosConsultas();
        List<DadosConsulta> RetornarDadosConsultasPorId(int id);
    }
}
