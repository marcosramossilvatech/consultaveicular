using ConsultaSerpro.Models;
using ConsultaSerpro.Models.Servico;
using Microsoft.EntityFrameworkCore;

namespace ConsultaSerpro.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private IConfiguration _configuration;
        private readonly ApiDBContext _context;

        public UsuarioRepositorio(IConfiguration configuration, ApiDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public  async Task<Usuario> RetornaUsario(string login, string senha)
        {
            try
            {
                Usuario? retorno = new Usuario();

                var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.UserName.Equals(login) && x.Password.Equals(senha) && x.IsActive==true);

                if(usuario!= null)
                {
                    var empresa = await _context.CnhVinculoUsuario.FirstOrDefaultAsync(x => x.Usuario == usuario.Id);
                    usuario.Empresa = empresa!= null ?(int)empresa.Empresa : 0;
                }
                
                return usuario;
            }
            catch (Exception ex)
            {
                return new Usuario();
              throw;
            }
        }
    }
}
