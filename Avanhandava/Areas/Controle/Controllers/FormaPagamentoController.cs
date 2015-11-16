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
    public class FormaPagamentoController : Controller
    {
        private IBaseService<FPgto> _service;
        private ILogin _login;

        public FormaPagamentoController(IBaseService<FPgto> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        // GET: Controle/FormaPagamento
        public ActionResult Index()
        {
            var fpgtos = _service.Listar()
                .OrderBy(x => x.Descricao);

            return View(fpgtos);
        }

        // GET: Controle/FormaPagamento/Details/5
        public ActionResult Detalhes(int id)
        {
            var fpgto = _service.Find(id);

            if (fpgto == null)
            {
                return HttpNotFound();
            }

            return View(fpgto);
        }

        // GET: Controle/FormaPagamento/Create
        public ActionResult Incluir()
        {
            return View(new FPgto());
        }

        // POST: Controle/FormaPagamento/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include = "Descricao")] FPgto fpgto)
        {
            try
            {
                fpgto.AlteradoPor = Identification.IdUsuario;
                fpgto.AlteradoEm = DateTime.Now;
                TryUpdateModel(fpgto);

                if (ModelState.IsValid)
                {
                    _service.Gravar(fpgto);
                    return RedirectToAction("Index");    
                }
                return View(fpgto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(fpgto);
            }
        }

        // GET: Controle/FormaPagamento/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fpgto = _service.Find((int)id);

            if (fpgto == null)
            {
                return HttpNotFound();
            }
            
            return View(fpgto);
        }

        // POST: Controle/FormaPagamento/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,Descricao,Ativo")] FPgto fpgto)
        {
            try
            {
                fpgto.AlteradoPor = Identification.IdUsuario;
                fpgto.AlteradoEm = DateTime.Now;
                TryUpdateModel(fpgto);

                if (ModelState.IsValid)
                {
                    _service.Gravar(fpgto);
                    return RedirectToAction("Index");
                }

                return View(fpgto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(fpgto);
            }
        }

        // GET: Controle/FormaPagamento/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fpgto = _service.Find((int)id);

            if (fpgto == null)
            {
                return HttpNotFound();
            }

            return View(fpgto);
        }

        // POST: Controle/FormaPagamento/Delete/5
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
                var fpgto = _service.Find(id);
                if (fpgto == null)
                {
                    return HttpNotFound();
                }
                return View(fpgto);
            }
        }
    }
}
