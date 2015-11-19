using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class CreditoService: IBaseService<Credito>
    {
        private IBaseRepository<Credito> repository;

        public CreditoService()
        {
            repository = new EFRepository<Credito>();
        }

        public IQueryable<Credito> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Credito item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Descricao = item.Descricao.ToUpper().Trim();            

            // valida
            if (item.DataCredito > DateTime.Today.Date)
            {
                throw new ArgumentException("A data do crédito não pode ser uma data futura");
            }

            if (item.DataCredito < item.Conta.SaldoData)
            {
                throw new ArgumentException(string.Format("A data do crédito não pode ser anterior a {0:d}", item.Conta.SaldoData));
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Credito Excluir(int id)
        {
            try
            {
                var credito = repository.Find(id);

                if (credito == null)
                {
                    throw new ArgumentException("Crédito inexistente");
                }

                if (credito.DataCredito < credito.Conta.SaldoData)
                {
                    throw new ArgumentException(string.Format("Não é possível excluir um crédito anterior à {0:d}", credito.Conta.SaldoData));
                }

                return repository.Excluir(id);
            }
            catch (Exception)
            {
                throw new ArgumentException("Não é possível excluir este crédito");
            }
        }

        public Credito Find(int id)
        {
            return repository.Find(id);
        }
    }
}
