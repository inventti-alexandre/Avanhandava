using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Avanhandava.Domain.Models.Admin
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione a empresa")]
        public int IdEmpresa { get; set; }

        [Display(Name="Grupo de custo")]
        [Range(1,double.MaxValue, ErrorMessage="Selecione o grupo de custo")]
        public int IdGrupoCusto { get; set; }

        [Display(Name="Ítem de custo")]
        [Range(1,double.MaxValue, ErrorMessage="Selecione o ítem de custo")]
        public int IdItemCusto { get; set; }

        [Display(Name="Fornecedor")]
        [Range(1, double.MaxValue, ErrorMessage="Selecione o fornecedor")]
        public int IdFornecedor { get; set; }

        [Display(Name = "Referência")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Informa a data de referência")]
        public DateTime Referencia { get; set; }

        [Display(Name = "Descrição (opcional)")]
        [StringLength(100,ErrorMessage="A descrição é composta por no máximo 100 caracteres")]
        public string Descricao { get; set; }

        [Display(Name="Observações (opcional)")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name="Cadastrado por")]
        public int CadastradoPor { get; set; }

        [Required]
        [Display(Name = "Cadastrado em")]
        public DateTime CadastradoEm { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name = "Cadastrado por")]
        public virtual Usuario UsuarioCadastradoPor
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.UsuarioService().Find(CadastradoPor);
            }
        }

        [NotMapped]
        [Display(Name = "Alterado por")]
        public virtual Usuario UsuarioAlteradoPor
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.UsuarioService().Find(AlteradoPor);
            }
        }

        [NotMapped]
        [Display(Name = "Empresa")]
        public virtual Empresa Empresa
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.EmpresaService().Find(IdEmpresa);
            }
        }

        [NotMapped]
        [Display(Name="Grupo de custo")]
        public virtual GrupoCusto GrupoCusto 
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.GrupoCustoService().Find(IdGrupoCusto);
            }
        }
    
        [NotMapped]
        [Display(Name = "Ítem de custo")]
        public virtual ItemCusto ItemCusto
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.ItemCustoService().Find(IdItemCusto);
            }
        }

        [NotMapped]
        [Display(Name = "Fornecedor")]
        public virtual Fornecedor Fornecedor
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.FornecedorService().Find(IdFornecedor);
            }
        }

        [NotMapped]
        [Display(Name = "Parcelas")]
        public virtual IQueryable<Parcela> Parcelas
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.ParcelaService()
                    .Listar()
                    .Where(x => x.IdAgendamento == Id);
            }
        }
    }
}
