namespace ConsultaSerpro.Models.Servico
{
    public  interface IUsuarioRepositorio
    {
        Task<Usuario> RetornaUsario(string login, string senha);
    }
}
