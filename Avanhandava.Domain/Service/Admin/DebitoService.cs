using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using System;

namespace Avanhandava.Domain.Service.Admin
{
    public class DebitoService
    {
        IBaseService<Agendamento> _agendamento;
        IBaseService<Parcela> _parcela;

        public DebitoService()
        {
            _agendamento = new AgendamentoService();
            _parcela = new ParcelaService();
        }

        /// <summary>
        /// Inclui agendamento/parcela e retorna idAgendamento
        /// </summary>
        /// <param name="debito"></param>
        /// <returns>idAgendamento</returns>
        public int Debitar(DebitoDiretoModel debito)
        {
            var idAgendamento = GetIdAgendamento(debito);
            try
            {
                return GravarParcela(debito, idAgendamento);
            }
            catch (Exception ex)
            {
                _agendamento.Excluir(idAgendamento);
                throw new Exception("Não foi possível incluir este agendamento direto: " + ex.Message);
            }
        }

        private int GravarParcela(DebitoDiretoModel debito, int idAgendamento)
        {
            var parcela = new Parcela
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = debito.AlteradoPor,
                Cheque = debito.Cheque,
                Compensado = debito.Compensado,
                CompensadoEm = debito.Vencto,
                DataPgto = DateTime.Today.Date,
                IdAgendamento = idAgendamento,
                IdConta = debito.IdConta,
                IdFpgto = debito.IdFpgto,
                IdPgto = debito.IdPgto,
                Observ = string.Empty,
                Valor = debito.Valor,
                Vencto = debito.Vencto
            };
            return _parcela.Gravar(parcela);
        }

        private int GetIdAgendamento(DebitoDiretoModel debito)
        {
            var agendamento = new Agendamento
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = debito.AlteradoPor,
                CadastradoEm = DateTime.Now,
                CadastradoPor = debito.AlteradoPor,
                Ativo = true,
                Descricao = string.IsNullOrEmpty(debito.Descricao) ? string.Empty : debito.Descricao.ToUpper().Trim(),
                IdEmpresa = debito.IdEmpresa,
                IdFornecedor = debito.IdFornecedor,
                IdGrupoCusto = debito.IdGrupoCusto,
                IdItemCusto = debito.IdItemCusto,
                Observ = string.Empty,
                Referencia = debito.Referencia
            };
            return _agendamento.Gravar(agendamento);
        }
    }
}