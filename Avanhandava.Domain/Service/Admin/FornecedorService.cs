using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class FornecedorService: IBaseService<Fornecedor>
    {
        private IBaseRepository<Fornecedor> repository;

        public FornecedorService()
        {
            repository = new EFRepository<Fornecedor>();
        }

        public IQueryable<Fornecedor> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Fornecedor item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Bairro = item.Bairro.ToUpper().Trim();
            item.Cidade = item.Cidade.ToUpper().Trim();
            item.Email = item.Email.ToLower().Trim();
            item.Endereco = item.Endereco.ToUpper().Trim();
            item.Fantasia = item.Fantasia.ToUpper().Trim();
            item.RazaoSocial = "" + item.RazaoSocial.ToUpper().Trim();
            item.IE = "" + item.IE;
            item.Ccm = "" + item.Ccm;
            item.Observ = "" + item.Observ;

            // valida
            if (repository.Listar().Where(x => (x.Fantasia == item.Fantasia || x.Cnpj == item.Cnpj || x.RazaoSocial == item.RazaoSocial) && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um fornecedor cadastrado com este nome fantasia/razão social/CNPJ");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Fornecedor Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var fornecedor = repository.Find(id);
                if (fornecedor != null)
                {
                    fornecedor.AlteradoEm = DateTime.Now;
                    fornecedor.Ativo = false;
                    return repository.Alterar(fornecedor);
                }
                return fornecedor;
            }
        }

        public Fornecedor Find(int id)
        {
            return repository.Find(id);
        }
    }
}
