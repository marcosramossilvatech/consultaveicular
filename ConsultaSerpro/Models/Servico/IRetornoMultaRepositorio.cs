using System.Threading.Tasks;

namespace ConsultaSerpro.Models.Servico
{
    public interface IRetornoMultaRepositorio
    {
        void AdicionarMultasAsync(List<RetornoMulta> multas, long consultaId, string retorno);
    }
}
