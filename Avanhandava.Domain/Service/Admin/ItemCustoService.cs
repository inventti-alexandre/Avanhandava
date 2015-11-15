using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class ItemCustoService: IBaseService<ItemCusto>
    {
        private IBaseRepository<ItemCusto> repository;

        public ItemCustoService()
        {
            repository = new EFRepository<ItemCusto>();
        }

        public IQueryable<ItemCusto> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(ItemCusto item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;            

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um ítem de custo cadastrado com esta descrição");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public ItemCusto Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var custo = repository.Find(id);
                if (custo != null)
                {
                    custo.Ativo = false;
                    custo.AlteradoEm = DateTime.Now;
                    return repository.Alterar(custo);
                }
                return custo;
            }
        }

        public ItemCusto Find(int id)
        {
            return repository.Find(id);
        }
    }
}
