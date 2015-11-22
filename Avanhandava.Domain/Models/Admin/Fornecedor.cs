using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avanhandava.Domain.Models.Admin
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome fantasia da empresa")]
        [StringLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        [Display(Name = "Nome fantasia")]
        public string Fantasia { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O email é composto por no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "O campo telefone comporta até 60 caracteres")]
        public string Telefone { get; set; }

        [StringLength(40, ErrorMessage="O contato é composto por no máximo 40 caracteres")]
        public string Contato { get; set; }

        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

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
    }
}
