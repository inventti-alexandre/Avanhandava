using System;

namespace Avanhandava.Areas.Controle.Models
{
    public class CreditoModel
    {
        public int IdEmpresa { get; set; }
        public int IdConta { get; set; }
        public int IdTipoCredito { get; set; }
        public DateTime? DataCredito { get; set; }
        public DateTime? Referencia { get; set; }
    }
}