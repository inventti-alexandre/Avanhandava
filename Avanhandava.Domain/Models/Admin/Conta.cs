using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avanhandava.Domain.Models.Admin
{
    public class Conta
    {
        [Key]
        public int Id { get; set; }

        [Range(1,double.MaxValue,ErrorMessage="Selecione a empresa titular da conta")]
        [Display(Name="Empresa")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage="Informe o nome da conta")]
        [StringLength(40,ErrorMessage="O nome da conta é composto por no máximo 40 caracteres")]
        [Display(Name="Nome da conta")]
        public string Descricao { get; set; }

        [Required(ErrorMessage="Informe o nome do banco")]
        [StringLength(40,ErrorMessage="O nome do banco é composto por no máximo 80 caracteres")]
        [Display(Name="Banco")]        
        public string BancoNome { get; set; }

        [Required(ErrorMessage="Informe o número do do banco")]
        [StringLength(40,ErrorMessage="O nome do banco é composto por no máximo 3 caracteres")]
        [Display(Name="Número do banco")]
        public string BancoNum { get; set; }

        [Required(ErrorMessage="Informe o número da agência")]
        [StringLength(40,ErrorMessage="O número da agência é composto por no máximo 10 caracteres")]
        [Display(Name="Número da agência")]
        public string BancoAgencia { get; set; }

        [Required(ErrorMessage="Informe o número da conta")]
        [StringLength(40,ErrorMessage="O número da conta é composto por no máximo 20 caracteres")]
        [Display(Name="Número da conta")]
        public string BancoConta { get; set; }

        [DisplayFormat(DataFormatString="{0:N2}")]
        [Display(Name="Saldo anterior")]
        public decimal SaldoAnterior { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name="Saldo atual")]
        public decimal SaldoAtual { get; set; }

        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        [Display(Name="Data saldo")]
        public DateTime SaldoData { get; set; }

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
    }
}
