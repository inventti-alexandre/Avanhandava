using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class TipoCreditoService: IBaseService<TipoCredito>
    {
        private IBaseRepository<TipoCredito> repository;

        public TipoCreditoService()
        {
            repository = new EFRepository<TipoCredito>();
        }

        public IQueryable<TipoCredito> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(TipoCredito item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um tipo de crédito cadastrado com esta descrição");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public TipoCredito Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var tipo = repository.Find(id);

                if (tipo != null)
                {
                    tipo.AlteradoEm = DateTime.Now;
                    tipo.Ativo = false;
                    return repository.Alterar(tipo);
                }
                return tipo;
            }
        }

        public TipoCredito Find(int id)
        {
            return repository.Find(id);
        }
    }
}
