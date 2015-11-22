using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class AgendamentoService: IBaseService<Agendamento>
    {
        private IBaseRepository<Agendamento> repository;

        public AgendamentoService()
        {
            repository = new EFRepository<Agendamento>();
        }

        public IQueryable<Agendamento> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Agendamento item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Descricao = string.IsNullOrEmpty(item.Descricao) ? string.Empty : item.Descricao.ToUpper().Trim();
            item.Observ = string.IsNullOrEmpty(item.Observ) ? string.Empty : item.Observ.ToUpper().Trim();
            item.Ativo = true;

            // grava
            if (item.Id == 0)
            {
                item.CadastradoEm = DateTime.Now;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Agendamento Excluir(int id)
        {
            try
            {
                var agendamento = repository.Find(id);

                if (agendamento == null)
                {
                    throw new ArgumentException("Agendamento inexistente");
                }

                // TODO: nao permite exclusao se houverem parcelas pagas
                // TODO: excluir as parcelas

                return repository.Excluir(id);
            }
            catch (Exception)
            {
                throw new ArgumentException("Não é possível excluir este agendamento");
            }
        }

        public Agendamento Find(int id)
        {
            return repository.Find(id);
        }
    }
}
