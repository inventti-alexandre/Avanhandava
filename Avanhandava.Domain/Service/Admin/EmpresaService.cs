using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class EmpresaService: IBaseService<Empresa>
    {
        private IBaseRepository<Empresa> repository;

        public EmpresaService()
        {
            repository = new EFRepository<Empresa>();
        }

        public IQueryable<Empresa> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Empresa item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Bairro = item.Bairro.ToUpper().Trim();
            item.Cidade = item.Cidade.ToUpper().Trim();
            item.Email = item.Email.ToLower().Trim();
            item.Endereco = item.Endereco.ToUpper().Trim();
            item.Fantasia = item.Fantasia.ToUpper().Trim();
            item.RazaoSocial = item.RazaoSocial.ToUpper().Trim();
            item.IE = "" + item.IE;
            item.Ccm = "" + item.Ccm;
            item.Observ = "" + item.Observ;

            // valida
            if (repository.Listar().Where(x => (x.Fantasia == item.Fantasia || x.Cnpj == item.Cnpj || x.RazaoSocial == item.RazaoSocial) && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma empresa cadastrada com este nome fantasia/razão social/CNPJ");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Empresa Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var empresa = repository.Find(id);
                if (empresa != null)
                {
                    empresa.Ativo = false;
                    empresa.AlteradoEm = DateTime.Now;
                    return repository.Alterar(empresa);
                }
                return empresa;
            }
        }

        public Empresa Find(int id)
        {
            return repository.Find(id);
        }
    }
}
