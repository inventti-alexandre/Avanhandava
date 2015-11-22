using Avanhandava.Common;
using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Abstract.Admin;
using Avanhandava.Domain.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Avanhandava.Areas.Controle.Controllers
{
    [Authorize]
    public class ParcelaController : Controller
    {
        private IBaseService<Parcela> _service;
        private ILogin _login;

        public ParcelaController(IBaseService<Parcela> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        // GET: Controle/Parcela
        public PartialViewResult Parcelas(int idAgendamento)
        {
            var parcelas = _service.Listar()
                .Where(x => x.IdAgendamento == idAgendamento)
                .OrderBy(x => x.Vencto)
                .ThenBy(x => x.Id)
                .ToList();

            ViewBag.IdAgendamento = idAgendamento;
            ViewBag.Total = parcelas.Count > 0 ? parcelas.Sum(x => x.Valor) : 0M;
            return PartialView(parcelas);
        }

        // GET: Controle/Parcela/Details/5
        public ActionResult Detalhes(int id)
        {
            var parcela = _service.Find(id);

            if (parcela == null)
            {
                return HttpNotFound();
            }

            return View(parcela);
        }

        // GET: Controle/Parcela/Create
        public ActionResult Incluir(int idAgendamento)
        {
            var parcela = new Parcela { IdAgendamento = idAgendamento, Vencto = DateTime.Today.Date };

            return View(parcela);
        }

        // POST: Controle/Parcela/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include="IdAgendamento,Vencto,Valor,IdPgto,Observ")] Parcela parcela)
        {
            try
            {
                parcela.AlteradoEm = DateTime.Now;
                parcela.AlteradoPor = Identification.IdUsuario;
                parcela.Observ = string.IsNullOrEmpty(parcela.Observ) ? string.Empty : parcela.Observ.ToUpper().Trim();
                TryUpdateModel(parcela);

                if (ModelState.IsValid)
                {
                    _service.Gravar(parcela);
                    return RedirectToAction("Index", "Agendamento", new { id = parcela.IdAgendamento });
                }

                return View(parcela);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(parcela);
            }
        }

        // GET: Controle/Parcela/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parcela = _service.Find((int)id);

            if (parcela == null)
            {
                return HttpNotFound();
            }

            return View(parcela);
        }

        // POST: Controle/Parcela/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,IdAgendamento,Vencto,Valor,IdPgto,Observ")] Parcela parcela)
        {
            try
            {
                parcela.AlteradoEm = DateTime.Now;
                parcela.AlteradoPor = Identification.IdUsuario;
                parcela.Observ = string.IsNullOrEmpty(parcela.Observ) ? string.Empty : parcela.Observ.ToUpper().Trim();
                TryUpdateModel(parcela);

                if (ModelState.IsValid)
                {
                    _service.Gravar(parcela);
                    return RedirectToAction("Index", "Agendamento", new { id = parcela.IdAgendamento });
                }

                return View(parcela);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(parcela);
            }
        }

        // GET: Controle/Parcela/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parcela = _service.Find((int)id);

            if (parcela == null)
            {
                return HttpNotFound();
            }

            return View(parcela);
        }

        // POST: Controle/Parcela/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var parcela = _service.Excluir(id);
                return RedirectToAction("Index", "Agendamento", new { id = parcela.IdAgendamento });
            }
            catch
            {
                var parcela = _service.Find(id);
                if (parcela == null)
                {
                    return HttpNotFound();
                }
                return View(parcela);
            }
        }
    }
}
