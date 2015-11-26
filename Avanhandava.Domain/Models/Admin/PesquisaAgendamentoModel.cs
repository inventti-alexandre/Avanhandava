using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Avanhandava.Domain.Models.Admin
{
    public enum Situacao
    {
        [Description("")]
        Todos,
        [Description("PAGOS")]
        Pagos,
        [Description("EM ABERTO")]
        EmAberto
    }

    public class PesquisaAgendamentoModel
    {
        [Display(Name = "Empresa")]
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
        public DateTime? Referencia { get; set; }

        [Display(Name = "Descrição (opcional)")]
        [StringLength(100, ErrorMessage = "A descrição é composta por no máximo 100 caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Vencimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VenctoInicial { get; set; }

        [Display(Name = "Vencimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VenctoFinal { get; set; }

        public decimal? Valor { get; set; }

        [Display(Name = "Pagável em")]
        public int IdPgto { get; set; }

        [Display(Name = "Observações")]
        public string Observ { get; set; }

        [Display(Name = "Forma de pagamento")]
        public int IdFpgto { get; set; }

        [Display(Name = "Data do pagamento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime? DataPgto { get; set; }

        [Display(Name = "Conta")]
        public int IdConta { get; set; }

        [Display(Name = "Cheque")]
        public int? Cheque { get; set; }

        [Display(Name = "Cadastrado em")]
        public DateTime? CadastradoEm { get; set; }

        [Display(Name = "Cadastrado a partir de")]
        public DateTime? CadastradoAPartirDe { get; set; }

        [Display(Name = "Situação")]
        public Situacao Situacao { get; set; }

        //public bool Compensado { get; set; }

        public List<Parcela> Parcelas { get; set; }

        public virtual decimal TotalPesquisa 
        {
            get
            {
                if (Parcelas == null)
                {
                    return 0;
                }

                return Parcelas.Sum(x => x.Valor);
            }
        }
    }
}