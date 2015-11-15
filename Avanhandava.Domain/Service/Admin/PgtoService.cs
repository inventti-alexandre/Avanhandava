using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class PgtoService: IBaseService<Pgto>
    {
        private IBaseRepository<Pgto> repository;

        public PgtoService()
        {
            repository = new EFRepository<Pgto>();
        }

        public IQueryable<Pgto> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Pgto item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma previsão de pagamento cadastrada com esta descrição");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Pgto Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var pgto = repository.Find(id);

                if (pgto != null)
                {
                    pgto.AlteradoEm = DateTime.Now;
                    pgto.Ativo = false;
                    return repository.Alterar(pgto);
                }

                return pgto;
            }
        }

        public Pgto Find(int id)
        {
            return repository.Find(id);
        }
    }
}
