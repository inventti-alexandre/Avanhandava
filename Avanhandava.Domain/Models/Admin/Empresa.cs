using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avanhandava.Domain.Models.Admin
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome fantasia da empresa")]
        [StringLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        [Display(Name = "Nome fantasia")]
        public string Fantasia { get; set; }

        [Required(ErrorMessage="Informe a razão social da empresa")]
        [StringLength(60, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Razão social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "CNPJ")]
        [Required(ErrorMessage="Informe o CNPJ da empresa")]
        [StringLength(14,ErrorMessage="O CNPJ é composto por no máximo 14 caracteres")]
        public string Cnpj { get; set; }

        [Display(Name = "Inscrição Estadual")]
        [StringLength(12,ErrorMessage="A inscrição estadual é composta por no máximo 12 caracteres")]
        public string IE { get; set; }

        [Display(Name="CCM")]
        [StringLength(10,ErrorMessage="O CCM é composto por no máximo 10 caracteres")]
        public string Ccm { get; set; }

        [Required(ErrorMessage="Informe o endereço")]
        [Display(Name="Endereço")]
        [StringLength(100,ErrorMessage="O endereço é composto por no máximo 100 caracteres")]
        public string Endereco { get; set; }

        [Required(ErrorMessage="Informe o bairro")]
        [StringLength(60,ErrorMessage="O bairro é composto por no máximo 60 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage="Informe a cidade")]
        [StringLength(60,ErrorMessage="A cidade é composta por no máximo 60 caracteres")]
        public string Cidade { get; set; }

        [Range(1,double.MaxValue,ErrorMessage="Selecione o Estado")]
        [Display(Name="Estado")]
        public int IdEstado { get; set; }

        [Required]
        [Display(Name = "CEP", Prompt = "99999-999", Description = "Código de endereçamento postal")]
        [StringLength(8, ErrorMessage = "O CEP é composto por 8 caracteres", MinimumLength = 8)]
        public string Cep { get; set; }

        [Display(Name="Observações")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

        [Required]
        [StringLength(60,ErrorMessage="O campo telefone comporta até 60 caracteres")]
        public string Telefone { get; set; }

        [Required]
        [StringLength(100,ErrorMessage="O email é composto por no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

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
        [Display(Name = "Estado")]
        public virtual Estado Estado
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.EstadoService().Find(IdEstado);
            }
            set { }
        }
    }
}
