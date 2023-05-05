using ConsultaSerpro.Models;
using ConsultaSerpro.Models.Servico;

namespace ConsultaSerpro.Repositorio
{
    public class RetornoVeiculoRepositorio : IRetornoVeiculoRepositorio
    {
        private readonly ApiDBContext _context;

        public RetornoVeiculoRepositorio(ApiDBContext context)
        {
            _context = context;
        }
        public async void AdicionarVeiculoAsync(RetornoVeiculo veiculo)
        {
            try
            {
                await _context.RetornoVeiculo.AddAsync(veiculo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
