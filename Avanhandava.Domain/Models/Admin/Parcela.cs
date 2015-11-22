using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avanhandava.Domain.Models.Admin
{
    public class Parcela
    {
        [Key]
        public int Id { get; set; }

        [Range(1,double.MaxValue,ErrorMessage="Agendamento inválido")]
        public int IdAgendamento { get; set; }

        [Required(ErrorMessage="Informe a data de vencimento da parcela")]
        [Display(Name="Vencimento")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime Vencto { get; set; }

        [Required(ErrorMessage="Informe o valor do agendamento")]
        [Range(0.01,double.MaxValue,ErrorMessage="Informe o valor do agendamento")]
        public decimal Valor { get; set; }

        [Display(Name="Pagável em")]
        [Range(0,int.MaxValue,ErrorMessage="Selecione a forma de pagamento")]
        public int IdPgto { get; set; }

        [Display(Name="Observações")]
        public string Observ { get; set; }

        [Display(Name="Forma de pagamento")]
        public int? IdFpgto { get; set; }

        [Display(Name="Data do pagamento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime? DataPgto { get; set; }

        [Display(Name="Conta")]
        public int? IdConta { get; set; }

        [Display(Name="Cheque")]
        public int Cheque { get; set; }

        public bool Compensado { get; set; }

        [Display(Name="Compensado em")]
        public DateTime? CompensadoEm { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [NotMapped]
        [Display(Name = "Usuário")]
        public virtual Usuario Usuario
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.UsuarioService().Find(AlteradoPor);
            }
        }

        [NotMapped]
        [Display(Name = "Pagável em")]
        public virtual Pgto Pgto
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.PgtoService().Find(IdPgto);
            }
        }

        [NotMapped]
        [Display(Name="Forma de pagamento")]
        public virtual string FPgto {
            get 
            {
                if (IdFpgto == null)
                {
                    return string.Empty;
                }

                var fpgto = new Avanhandava.Domain.Service.Admin.FPgtoService().Find((int)IdFpgto);
                
                if (fpgto == null)
                {
                    return string.Empty;
                }
                
                return fpgto.Descricao;
            }
        }

        [NotMapped]
        [Display(Name="Conta")]
        public virtual string Conta
        {
            get
            {
                if (IdConta == null)
                {
                    return string.Empty;
                }

                var conta = new Avanhandava.Domain.Service.Admin.ContaService().Find((int)IdConta);

                if (conta == null)
                {
                    return string.Empty;
                }

                return conta.Descricao;
            }
        }
    }
}
