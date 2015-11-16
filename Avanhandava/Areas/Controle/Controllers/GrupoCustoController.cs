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
    public class GrupoCustoController : Controller
    {
        private IBaseService<GrupoCusto> _service;
        private ILogin _login;

        public GrupoCustoController(IBaseService<GrupoCusto> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        // GET: Controle/GrupoCusto
        public ActionResult Index()
        {
            var grupos = _service.Listar()
                .OrderBy(x => x.Descricao);

            return View(grupos);
        }

        // GET: Controle/GrupoCusto/Details/5
        public ActionResult Detalhes(int id)
        {
            var grupo = _service.Find(id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // GET: Controle/GrupoCusto/Create
        public ActionResult Incluir()
        {
            return View(new GrupoCusto());
        }

        // POST: Controle/GrupoCusto/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include="Descricao")] GrupoCusto grupo)
        {
            try
            {
                grupo.AlteradoEm = DateTime.Now;
                grupo.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(grupo);

                if (ModelState.IsValid)
                {
                    _service.Gravar(grupo);
                    return RedirectToAction("Index");
                }

                return View(grupo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(grupo);
            }
        }

        // GET: Controle/GrupoCusto/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = _service.Find((int)id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // POST: Controle/GrupoCusto/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,Descricao,Ativo")] GrupoCusto grupo)
        {
            try
            {
                grupo.AlteradoPor = Identification.IdUsuario;
                grupo.AlteradoEm = DateTime.Now;
                TryUpdateModel(grupo);

                if (ModelState.IsValid)
	            {
                    _service.Gravar(grupo);
		            return RedirectToAction("Index");
	            }
                return View(grupo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(grupo);
            }
        }

        // GET: Controle/GrupoCusto/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = _service.Find((int)id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // POST: Controle/GrupoCusto/Delete/5
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
                var grupo = _service.Find(id);
                if (grupo == null)
                {
                    return HttpNotFound();
                }
                return View(grupo);
            }
        }
    }
}
