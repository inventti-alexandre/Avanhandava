
namespace Avanhandava.Domain.Abstract.Email
{
    public interface IEmail
    {
        bool Enviar(string nome, string email, string assunto, string mensagem);
    }
}
