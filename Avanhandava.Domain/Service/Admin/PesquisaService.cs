using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avanhandava.Domain.Service.Admin
{
    public class PesquisaService: IDisposable
    {
        private EFDbContext db = new EFDbContext();
        
        public List<Parcela> Pesquisar(PesquisaAgendamentoModel pesquisa)
        {
            if (pesquisa.IdEmpresa == 0
                && pesquisa.IdGrupoCusto == 0
                && pesquisa.IdItemCusto == 0
                && pesquisa.IdFornecedor == 0
                && pesquisa.Referencia == null
                && string.IsNullOrEmpty(pesquisa.Descricao)
                && pesquisa.VenctoInicial == null
                && pesquisa.VenctoFinal == null
                && pesquisa.Valor == null
                && pesquisa.IdPgto == 0
                && string.IsNullOrEmpty(pesquisa.Observ)
                && pesquisa.IdFpgto == 0
                && pesquisa.DataPgto == null
                && pesquisa.IdConta == 0
                && pesquisa.Cheque == null)
            {
                throw new ArgumentException("Nenhum parâmetro selecionado para pesquisa");
            }

            var lista = (from a in db.Agendamento
                         join p in db.Parcela on a.Id equals p.IdAgendamento
                         into ap
                         from p in ap
                         where (
                         (pesquisa.IdEmpresa == 0 || a.IdEmpresa == pesquisa.IdEmpresa)
                         && (pesquisa.IdGrupoCusto == 0 || a.IdGrupoCusto == pesquisa.IdGrupoCusto)
                         && (pesquisa.IdItemCusto == 0 || a.IdItemCusto == pesquisa.IdItemCusto)
                         && (pesquisa.IdFornecedor == 0 || a.IdFornecedor == pesquisa.IdFornecedor)
                         && (pesquisa.Referencia == null || a.Referencia == pesquisa.Referencia)
                         && (string.IsNullOrEmpty(pesquisa.Descricao) || a.Descricao.Contains(pesquisa.Descricao.ToUpper().Trim()))
                         && (pesquisa.VenctoInicial == null || p.Vencto >= pesquisa.VenctoInicial)
                         && (pesquisa.VenctoFinal == null || p.Vencto <= pesquisa.VenctoFinal)
                         && (pesquisa.Valor == null || p.Valor == pesquisa.Valor)
                         && (pesquisa.IdPgto == 0 || p.IdPgto == pesquisa.IdPgto)
                         && (string.IsNullOrEmpty(pesquisa.Observ) || p.Observ.Contains(pesquisa.Observ.ToUpper().Trim()))
                         && (pesquisa.IdFpgto == 0 || p.IdFpgto == pesquisa.IdFpgto)
                         && (pesquisa.DataPgto == null || p.DataPgto == pesquisa.DataPgto)
                         && (pesquisa.IdConta == 0 || p.IdConta == pesquisa.IdConta)
                         && (pesquisa.Cheque == null || p.Cheque == pesquisa.Cheque)
                         )
                         select p);

            return lista.
                OrderBy(x => x.Vencto)
                .ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
