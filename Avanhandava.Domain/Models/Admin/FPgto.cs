﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avanhandava.Domain.Models.Admin
{
    public class FPgto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a descrição da forma de pagamento")]
        [Display(Name = "Forma prevista de pagamento")]
        [StringLength(20, ErrorMessage = "A forma de pagamento é composta por no máximo 20 caracteres")]
        public string Descricao { get; set; }

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
