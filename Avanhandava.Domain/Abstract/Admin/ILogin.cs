using Avanhandava.Domain.Models.Admin;

namespace Avanhandava.Domain.Abstract.Admin
{
    public interface ILogin
    {
        Usuario ValidaLogin(string login, string senha);
        int GetIdUsuario(string login);
    }
}
