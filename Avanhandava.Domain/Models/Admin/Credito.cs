using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avanhandava.Domain.Models.Admin
{
    public class Credito
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Empresa")]
        [Range(1,double.MaxValue, ErrorMessage="Selecione a empresa")]
        public int IdEmpresa { get; set; }

        [Display(Name="Conta")]
        [Range(1,double.MaxValue, ErrorMessage="Selecione a conta")]
        public int IdConta { get; set; }

        [Display(Name="Tipo de crédito")]
        [Range(1,double.MaxValue, ErrorMessage="Selecione o tipo de crédito")]
        public int IdTipoCredito { get; set; }

        [Display(Name="Data do crédito")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        [Required(ErrorMessage="Informe a data do crédito")]
        public DateTime DataCredito { get; set;}

        [Display(Name = "Referência")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}",ApplyFormatInEditMode=true)]
        [Required(ErrorMessage = "Informa a data de referência do crédito")]
        public DateTime Referencia { get; set; }

        [Required(ErrorMessage="Informe a descrição do crédito")]
        [Display(Name="Descrição")]
        [StringLength(100,ErrorMessage="A descrição do crédito é composta por no máximo 100 caracteres")]
        public string Descricao { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage="Valor do crédito inválido")]
        [DisplayFormat(DataFormatString="{0:C2}")]
        public decimal Valor { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name = "Usuário")]
        public virtual Usuario Usuario
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.UsuarioService().Find(AlteradoPor);
            }
            set { }
        }

        [NotMapped]
        [Display(Name="Empresa")]
        public virtual Empresa Empresa
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.EmpresaService().Find(IdEmpresa);
            }
        }

        [NotMapped]
        [Display(Name="Tipo de crédito")]
        public virtual TipoCredito TipoCredito
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.TipoCreditoService().Find(IdTipoCredito);
            }
        }

        [NotMapped]
        [Display(Name="Conta")]
        public virtual Conta Conta
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.ContaService().Find(IdConta);
            }
        }
    }
}
