using Avanhandava.Common;
using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Abstract.Admin;
using Avanhandava.Domain.Models.Admin;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Avanhandava.Areas.Controle.Controllers
{
    [Authorize]
    public class FormaPrevistaPagamentoController : Controller
    {
        private IBaseService<Pgto> _service;
        private ILogin _login;

        public FormaPrevistaPagamentoController(IBaseService<Pgto> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        // GET: Controle/FormaPrevistaPagamento
        public ActionResult Index()
        {
            var pgtos = _service.Listar()
                .OrderBy(x => x.Descricao);

            return View(pgtos);
        }

        // GET: Controle/FormaPrevistaPagamento/Details/5
        public ActionResult Detalhes(int id)
        {
            var pgto = _service.Find(id);

            if (pgto == null)
            {
                return HttpNotFound();
            }

            return View(pgto);
        }

        // GET: Controle/FormaPrevistaPagamento/Create
        public ActionResult Incluir()
        {
            return View(new Pgto());
        }

        // POST: Controle/FormaPrevistaPagamento/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include="Descricao")] Pgto pgto)
        {
            try
            {
                pgto.AlteradoPor = Identification.IdUsuario;
                pgto.AlteradoEm = DateTime.Now;
                TryUpdateModel(pgto);

                if (ModelState.IsValid)
                {
                    _service.Gravar(pgto);
                    return RedirectToAction("Index");
                }
                return View(pgto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(pgto);
            }
        }

        // GET: Controle/FormaPrevistaPagamento/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pgto = _service.Find((int)id);

            if (pgto == null)
            {
                return HttpNotFound();
            }

            return View(pgto);
        }

        // POST: Controle/FormaPrevistaPagamento/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,Descricao,Ativo")] Pgto pgto)
        {
            try
            {
                pgto.AlteradoPor = Identification.IdUsuario;
                pgto.AlteradoEm = DateTime.Now;
                TryUpdateModel(pgto);

                if (ModelState.IsValid)
                {
                    _service.Gravar(pgto);
                    return RedirectToAction("Index");
                }

                return View(pgto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(pgto);
            }
        }

        // GET: Controle/FormaPrevistaPagamento/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pgto = _service.Find((int)id);

            if (pgto == null)
            {
                return HttpNotFound();
            }

            return View(pgto);
        }

        // POST: Controle/FormaPrevistaPagamento/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                _service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var pgto = _service.Find(id);
                if (pgto == null)
                {
                    return HttpNotFound();
                }
                return View(pgto);
            }
        }
    }
}
