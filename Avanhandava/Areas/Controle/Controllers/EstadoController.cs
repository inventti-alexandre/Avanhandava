using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Avanhandava.Areas.Controle.Controllers
{
    public class EstadoController : Controller
    {
        private IBaseService<Estado> _service;

        public EstadoController(IBaseService<Estado> service)
        {
            _service = service;
        }

        // GET: Controle/Estado
        public ActionResult Index()
        {
            var estados = _service.Listar()
                .OrderBy(x => x.Descricao);

            return View(estados);
        }

        // GET: Controle/Estado/Details/5
        public ActionResult Detalhes(int id)
        {
            var estado = _service.Find(id);

            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // GET: Controle/Estado/Create
        public ActionResult Incluir()
        {
            return View(new Estado());
        }

        // POST: Controle/Estado/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include="Descricao,UF")] Estado estado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Gravar(estado);
                    return RedirectToAction("Index");
                }

                return View(estado);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(estado);
            }
        }

        // GET: Controle/Estado/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estado = _service.Find((int)id);

            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // POST: Controle/Estado/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,Descricao,UF,Ativo")] Estado estado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Gravar(estado);
                    return RedirectToAction("Index");
                }

                return View(estado);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(estado);
            }
        }

        // GET: Controle/Estado/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estado = _service.Find((int)id);

            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // POST: Controle/Estado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var estado = _service.Find(id);
                if (estado == null)
                {
                    return HttpNotFound();
                }
                return View(estado);
            }
        }
    }
}
