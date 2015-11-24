using Avanhandava.Common;
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
    public class DebitoController : Controller
    {
        // GET: Controle/Debito
        public ActionResult Index()
        {
            return View(new DebitoDiretoModel { Referencia = DateTime.Today.Date, Vencto = DateTime.Today.Date, Compensado = true });
        }

        // POST: Controle/Debito
        [HttpPost]
        public ActionResult Index(DebitoDiretoModel model)
        {
            model.AlteradoPor = Identification.IdUsuario;
            TryUpdateModel(model);

            if (ModelState.IsValid)
            {
                int idAgendamento = new DebitoService().Debitar(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
