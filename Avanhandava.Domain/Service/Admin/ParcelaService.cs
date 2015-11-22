using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using Avanhandava.Domain.Service.Admin;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class ParcelaService : IBaseService<Parcela>
    {
        private IBaseRepository<Parcela> repository;

        public ParcelaService()
        {
            repository = new EFRepository<Parcela>();
        }

        public IQueryable<Parcela> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Parcela item)
        {
            // formata
            item.Observ = string.IsNullOrEmpty(item.Observ) ? string.Empty : item.Observ.ToUpper().Trim();            

            // valida
            if (item.Vencto < DateTime.Today.Date && PermiteAgendamentoDataPassada() == false)
            {
                throw new ArgumentException("Não é permitido agendamento com data passada");
            }

            if (item.Cheque == 0)
            {
                item.IdFpgto = null;
                item.Compensado = false;
                item.CompensadoEm = null;
            }

            // grava
            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Parcela Excluir(int id)
        {
            try
            {
                var parcela = repository.Find(id);

                if (parcela == null)
                {
                    throw new ArgumentException("Parcela inexistente");
                }

                if (parcela.Cheque > 0)
                {
                    throw new ArgumentException("Esta parcela já foi paga, portanto, não é passível de exclusão");
                }

                return repository.Excluir(id);
            }
            catch (Exception)
            {
                throw new ArgumentException("Não é possível excluir esta parcela");
            }
        }

        public Parcela Find(int id)
        {
            return repository.Find(id);
        }

        private bool PermiteAgendamentoDataPassada()
        {
            var parametro = new SistemaParametroService().Listar()
                .Where(x => x.Codigo == "AG_ANTERIOR")
                .FirstOrDefault();

            if (parametro != null)
            {
                if (parametro.Valor.ToLower().Trim() == "true")
                {
                    return true;
                }
            }

            return false;
        }

    }
}
