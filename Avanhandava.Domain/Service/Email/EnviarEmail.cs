using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Avanhandava.Domain.Models.Email;
using Avanhandava.Domain.Abstract.Email;

namespace Avanhandava.Domain.Service.Email
{
    public class EnviarEmail : IEmail
    {
        EmailCredential credential;

        public EnviarEmail()
        {
            credential = new EmailCredential();
        }

        public bool Enviar(string nome, string destinatario, string assunto, string mensagem)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    // configuracoes para envio
                    smtpClient.EnableSsl = credential.UseSsl;
                    smtpClient.Host = credential.ServerSmtp;
                    smtpClient.Port = credential.ServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(credential.Sender, credential.SenderPassword);

                    // mensagem
                    var message = new MailMessage(credential.Sender, destinatario, assunto, mensagem);
                    message.IsBodyHtml = false;

                    // envia o email
                    smtpClient.Send(message);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
