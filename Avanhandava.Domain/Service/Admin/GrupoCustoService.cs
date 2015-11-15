using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class GrupoCustoService: IBaseService<GrupoCusto>
    {
        private IBaseRepository<GrupoCusto> repository;

        public GrupoCustoService()
        {
            repository = new EFRepository<GrupoCusto>();
        }

        public IQueryable<GrupoCusto> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(GrupoCusto item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Descricao = item.Descricao.ToUpper().Trim();
            
            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um grupo de custo cadastrado com esta descrição");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public GrupoCusto Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var grupoCusto = repository.Find(id);

                if (grupoCusto != null)
                {
                    grupoCusto.AlteradoEm = DateTime.Now;
                    grupoCusto.Ativo = false;
                    return repository.Alterar(grupoCusto);
                }

                return grupoCusto;
            }
        }

        public GrupoCusto Find(int id)
        {
            return repository.Find(id);
        }
    }
}
