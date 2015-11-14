using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avanhandava.Domain.Models.Email
{
    public class EmailCredential
    {
        #region [ Properties ]

        public bool UseSsl { get; set; }
        public string ServerSmtp { get; set; }
        public int ServerPort { get; set; }
        public string Sender { get; set; }
        public string SenderPassword { get; set; }

        #endregion

        Avanhandava.Domain.Service.Admin.SistemaParametroService serviceParametro;

        public EmailCredential()
        {
            serviceParametro = new Avanhandava.Domain.Service.Admin.SistemaParametroService();

            UseSsl = Convert.ToBoolean(GetParametro("EMAIL_USESSL"));
            ServerSmtp = GetParametro("EMAIL_SERVERSMTP");
            ServerPort = Convert.ToInt32(GetParametro("EMAIL_SERVERPORT"));
            Sender = GetParametro("EMAIL_SENDER");
            SenderPassword = GetParametro("EMAIL_SENDERPASSWORD");
        }

        private string GetParametro(string codigo)
        {
            var parametro = serviceParametro.Listar().Where(x => x.Codigo == codigo).FirstOrDefault();

            if (parametro != null)
            {
                return parametro.Valor;
            }

            return null;
        }
    }
}
