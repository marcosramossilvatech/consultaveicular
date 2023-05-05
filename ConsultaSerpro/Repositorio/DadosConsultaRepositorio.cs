using ConsultaSerpro.Models;
using ConsultaSerpro.Models.Servico;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsultaSerpro.Repositorio
{
    public class DadosConsultaRepositorio : IDadosConsultaRepositorio
    {
        private IConfiguration _configuration;
        private readonly ApiDBContext _context;

        public DadosConsultaRepositorio(IConfiguration configuration, ApiDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<DadosConsulta?> AdicionarAsync(DadosConsulta consulta)
        {
            try
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return consulta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<DadosConsulta> Alterar(DadosConsulta consulta)
        {
            try
            {
                _context.Update(consulta);
                await _context.SaveChangesAsync();
                return consulta;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<DadosConsulta>> RetornarDadosConsultas()        
        {
            return await _context.DadosConsulta.ToListAsync();
        }

        public List<DadosConsulta> RetornarDadosConsultasPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Alterar(Task<DadosConsulta?> dados)
        {
            throw new NotImplementedException();
        }
    }
}
