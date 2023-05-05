using ConsultaSerpro.Models;
using ConsultaSerpro.Models.Servico;

namespace ConsultaSerpro.Repositorio
{
    public class RetornoCNHRepositorio : IRetornoCNHRepositorio
    {
        private readonly ApiDBContext _context;

        public RetornoCNHRepositorio(ApiDBContext context)
        {
            _context = context;
        }

        public async void AdicionarValidacaoAsync(RetornoCNH cnh)
        {
            try
            {
                await _context.RetornoCNH.AddAsync(cnh);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
