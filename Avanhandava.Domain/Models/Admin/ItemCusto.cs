using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avanhandava.Domain.Models.Admin
{
    public class ItemCusto
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Grupo de custo")]
        [Range(1, double.MaxValue,ErrorMessage="Selecione o grupo de custo")]
        public int IdGrupoCusto { get; set; }

        [Required(ErrorMessage = "Informe a descrição da previsão do pagamento")]
        [Display(Name = "Forma prevista de pagamento")]
        [StringLength(60, ErrorMessage = "A forma de pagamento é composta por no máximo 60 caracteres")]
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

        [NotMapped]
        [Display(Name = "Grupo custo")]
        public virtual GrupoCusto GrupoCusto
        {
            get
            {
                return new Avanhandava.Domain.Service.Admin.GrupoCustoService().Find(IdGrupoCusto);
            }
        }
    }
}
