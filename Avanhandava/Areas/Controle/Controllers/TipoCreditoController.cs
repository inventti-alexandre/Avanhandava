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
    public class TipoCreditoController : Controller
    {
        private IBaseService<TipoCredito> _service;
        private ILogin _login;

        public TipoCreditoController(IBaseService<TipoCredito> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        // GET: Controle/TipoCredito
        public ActionResult Index()
        {
            var tipos = _service.Listar()
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(tipos);
        }

        // GET: Controle/TipoCredito/Details/5
        public ActionResult Detalhes(int id)
        {
            var tipo = _service.Find(id);

            if (tipo == null)
            {
                return HttpNotFound();
            }

            return View(tipo);
        }

        // GET: Controle/TipoCredito/Create
        public ActionResult Incluir()
        {
            return View(new TipoCredito());
        }

        // POST: Controle/TipoCredito/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include="Descricao")] TipoCredito tipo)
        {
            try
            {
                tipo.AlteradoEm = DateTime.Now;
                tipo.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(tipo);

                if (ModelState.IsValid)
                {
                    _service.Gravar(tipo);
                    return RedirectToAction("Index");
                }

                return View(tipo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(tipo);
            }
        }

        // GET: Controle/TipoCredito/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipo = _service.Find((int)id);

            if (tipo == null)
            {
                return HttpNotFound();
            }

            return View(tipo);
        }

        // POST: Controle/TipoCredito/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,Descricao,Ativo")] TipoCredito tipo)
        {
            try
            {
                tipo.AlteradoPor = Identification.IdUsuario;
                tipo.AlteradoEm = DateTime.Now;
                TryUpdateModel(tipo);

                if (ModelState.IsValid)
                {
                    _service.Gravar(tipo);
                    return RedirectToAction("Index");
                }

                return View(tipo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(tipo);
            }
        }

        // GET: Controle/TipoCredito/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipo = _service.Find((int)id);

            if (tipo == null)
            {
                return HttpNotFound();
            }

            return View(tipo);
        }

        // POST: Controle/TipoCredito/Delete/5
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
                var tipo = _service.Find(id);
                if (tipo == null)
                {
                    return HttpNotFound();
                }
                return View(tipo);
            }
        }
    }
}
