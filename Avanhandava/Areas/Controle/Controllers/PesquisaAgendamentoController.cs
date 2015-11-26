using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Service.Admin;
using System;
using System.Web.Mvc;

namespace Avanhandava.Areas.Controle.Controllers
{
    [Authorize]
    public class PesquisaAgendamentoController : Controller
    {
        // GET: Controle/PesquisaAgendamento
        public ActionResult Index()
        {
            return View(new PesquisaAgendamentoModel());
        }

        // POST: Controle/PesquisaAgendamento
        [HttpPost]
        public ActionResult Index(PesquisaAgendamentoModel model, int idSituacao)
        {
            try
            {
                if (idSituacao == 0)
                {
                    model.Situacao = Situacao.Todos;
                }
                else if (idSituacao == 1)
                {
                    model.Situacao = Situacao.Pagos;
                }
                else
                {
                    model.Situacao = Situacao.EmAberto;
                }
                var service = new PesquisaService();
                model.Parcelas = service.Pesquisar(model);

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
    }
}
