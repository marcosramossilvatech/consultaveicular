using ConsultaSerpro.Models;
using ConsultaSerpro.Models.Servico;
using Microsoft.Extensions.Configuration;

namespace ConsultaSerpro.Repositorio
{
    public class RetornoMultaRepositorio : IRetornoMultaRepositorio
    {
        private readonly ApiDBContext _context;

        public RetornoMultaRepositorio(ApiDBContext context)
        {
            _context = context;
        }

        async void IRetornoMultaRepositorio.AdicionarMultasAsync(List<RetornoMulta> multas, long consultaId, string retorno)
        {
            if (multas.Any())
            {
                multas.ForEach(x => x.IdConsulta = consultaId);
                multas.ForEach(x => x.Retorno = retorno);
                await _context.RetornoMulta.AddRangeAsync(multas);
                await _context.SaveChangesAsync();
            }
            else
            {
                RetornoMulta mul = new RetornoMulta();
                mul.IdConsulta = consultaId;
                mul.Retorno = retorno;
                await _context.RetornoMulta.AddAsync(mul);
                await _context.SaveChangesAsync();
            }


        }
    }
}
