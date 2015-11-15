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
    public class SistemaParametroController : Controller
    {
        private IBaseService<SistemaParametro> _service;
        private ILogin _login;

        public SistemaParametroController(IBaseService<SistemaParametro> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        // GET: Controle/SistemaParametro
        public ActionResult Index()
        {
            var parametros = _service.Listar()
                .OrderBy(x => x.Codigo);

            return View(parametros);
        }

        // GET: Controle/SistemaParametro/Detalhes/5
        public ActionResult Detalhes(int id)
        {
            var parametro = _service.Find(id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        // GET: Controle/SistemaParametro/Incluir
        public ActionResult Incluir()
        {
            return View(new SistemaParametro());
        }

        // POST: Controle/SistemaParametro/Incluir
        [HttpPost]
        public ActionResult Incluir([Bind(Include="Codigo,Valor,Descricao")] SistemaParametro parametro)
        {
            try
            {
                parametro.AlteradoEm = DateTime.Now;
                parametro.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(parametro);

                if (ModelState.IsValid)
                {
                    _service.Gravar(parametro);
                    return RedirectToAction("Index");
                }

                return View(parametro);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(parametro);
            }
        }

        // GET: Controle/SistemaParametro/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parametro = _service.Find((int)id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        // POST: Controle/SistemaParametro/Editar/5
        [HttpPost]
        public ActionResult Editar([Bind(Include = "Id,Codigo,Valor,Descricao")] SistemaParametro parametro)
        {
            try
            {
                parametro.AlteradoEm = DateTime.Now;
                parametro.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(parametro);

                if (ModelState.IsValid)
                {
                    _service.Gravar(parametro);
                    return RedirectToAction("Index");
                }

                return View(parametro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(parametro);
            }
        }

        // GET: Controle/SistemaParametro/Excluir/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parametro = _service.Find((int)id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        // POST: Controle/SistemaParametro/Excluir/5
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
                var parametro = _service.Find(id);
                if (parametro == null)
                {
                    return HttpNotFound();
                }
                return View(parametro);
            }
        }
    }
}
