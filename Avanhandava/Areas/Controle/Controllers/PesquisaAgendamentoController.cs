using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Index(PesquisaAgendamentoModel model)
        {
            var service = new PesquisaService();
            model.Parcelas = service.Pesquisar(model);

            return View(model);
        }
    }
}
