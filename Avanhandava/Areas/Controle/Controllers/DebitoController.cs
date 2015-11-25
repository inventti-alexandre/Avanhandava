using Avanhandava.Common;
using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Avanhandava.Areas.Controle.Controllers
{
    [Authorize]
    public class DebitoController : Controller       
    {
        // GET: Controle/Debito
        public ActionResult Index()
        {
            var parcelas = new PesquisaService().Pesquisar(new PesquisaAgendamentoModel
            {
                CadastradoEm = DateTime.Today.Date
            });

            return View(parcelas);
        }

        // GET: Controle/Debito/Incluir
        public ActionResult Incluir()
        {
            var empresa = new EmpresaService().Listar().Where(x => x.Ativo == true).OrderBy(x => x.Fantasia).FirstOrDefault();

            return View(new DebitoDiretoModel
            {
                Referencia = DateTime.Today.Date,
                Vencto = DateTime.Today.Date,
                Compensado = true,
                IdEmpresa = empresa == null ? 0 : empresa.Id
            });
        }

        // POST: Controle/Debito/Incluir
        [HttpPost]
        public ActionResult Incluir(DebitoDiretoModel model)
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

        // GET: Controle/Debito/Excluir
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parcela = 
            if (true)
            {
                
            }
        }

        // POST: Controle/Debito/Excluir
        public ActionResult Excluir(int id)
        {

        }
    }
}
