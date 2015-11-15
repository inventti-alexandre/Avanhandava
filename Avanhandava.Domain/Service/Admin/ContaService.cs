using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class ContaService: IBaseService<Conta>
    {
        private IBaseRepository<Conta> repository;

        public ContaService()
        {
            repository = new EFRepository<Conta>();
        }

        public IQueryable<Conta> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Conta item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.BancoAgencia = item.BancoAgencia.ToUpper().Trim();
            item.BancoConta = item.BancoConta.ToUpper().Trim();
            item.BancoNome = item.BancoNome.ToUpper().Trim();
            item.BancoNum = item.BancoNum.ToUpper().Trim();
            item.Descricao = item.Descricao.ToUpper().Trim();            

            // valida
            if (item.SaldoData > DateTime.Today.Date)
            {
                throw new ArgumentException("Data do saldo inválida");
            }

            if (repository.Listar().Where(x => (x.Descricao == item.Descricao || x.BancoConta == item.BancoConta) && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma conta cadastrada com este nome/número de conta");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }
            return repository.Alterar(item).Id;
        }

        public Conta Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var conta = repository.Find(id);
                if (conta != null)
                {
                    conta.Ativo = false;
                    conta.AlteradoEm = DateTime.Now;
                    return repository.Alterar(conta);
                }
                return conta;
            }
        }

        public Conta Find(int id)
        {
            return repository.Find(id);
        }
    }
}
