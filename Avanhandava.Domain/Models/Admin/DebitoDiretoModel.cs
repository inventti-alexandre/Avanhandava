using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avanhandava.Domain.Models.Admin
{
    public class DebitoDiretoModel
    {
        public int Id { get; set; }

        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione a empresa")]
        public int IdEmpresa { get; set; }

        [Display(Name = "Grupo de custo")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione o grupo de custo")]
        public int IdGrupoCusto { get; set; }

        [Display(Name = "Ítem de custo")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione o ítem de custo")]
        public int IdItemCusto { get; set; }

        [Display(Name = "Fornecedor")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione o fornecedor")]
        public int IdFornecedor { get; set; }

        [Display(Name = "Referência")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Informa a data de referência")]
        public DateTime Referencia { get; set; }

        [Display(Name = "Descrição (opcional)")]
        [StringLength(100, ErrorMessage = "A descrição é composta por no máximo 100 caracteres")]
        public string Descricao { get; set; }


        [Display(Name = "Pagável em")]
        [Range(0, int.MaxValue, ErrorMessage = "Selecione a forma de pagamento")]
        public int IdPgto { get; set; }

        [Required(ErrorMessage = "Informe a data de vencimento da parcela")]
        [Display(Name = "Vencimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Vencto { get; set; }

        [Required(ErrorMessage = "Informe o valor do agendamento")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Informe o valor do agendamento")]
        public decimal Valor { get; set; }

        [Display(Name = "Forma de pagamento")]
        public int IdFpgto { get; set; }

        [Display(Name = "Conta")]
        public int IdConta { get; set; }

        [Display(Name = "Cheque")]
        [Required(ErrorMessage="Informe o número do cheque")]
        [Range(1,int.MaxValue, ErrorMessage="Informe o número do cheque")]
        public int Cheque { get; set; }

        [Display(Name = "Situação do pagamento")]
        public bool Compensado { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

    }
}
