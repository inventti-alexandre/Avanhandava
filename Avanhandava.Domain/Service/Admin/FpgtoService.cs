using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class FPgtoService: IBaseService<FPgto>
    {
        private IBaseRepository<FPgto> repository;

        public FPgtoService()
        {
            repository = new EFRepository<FPgto>();
        }

        public IQueryable<FPgto> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(FPgto item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma forma de pagamento cadastrada com esta descrição");
            }

            // gravar
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public FPgto Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var fpgto = repository.Find(id);

                if (fpgto != null)
                {
                    fpgto.AlteradoEm = DateTime.Now;
                    fpgto.Ativo = false;
                    return repository.Alterar(fpgto);
                }

                return fpgto;
            }
        }

        public FPgto Find(int id)
        {
            return repository.Find(id);
        }
    }
}
