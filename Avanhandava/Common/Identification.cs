using System.Linq;
using System.Web;

namespace Avanhandava.Common
{
    public static class Identification
    {
        public static int IdUsuario
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.UsuarioService().Listar()
                    .FirstOrDefault(x => x.Login.ToUpper() == HttpContext.Current.User.Identity.Name.ToUpper()).Id;
            }
        }

        public static string NomeUsuario
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.UsuarioService().Listar()
                    .FirstOrDefault(x => x.Login.ToUpper() == HttpContext.Current.User.Identity.Name.ToUpper()).Nome;
            }
        }
    }
}