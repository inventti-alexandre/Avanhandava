using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avanhandava.Areas.Controle.Controllers
{
    public class SistemaParametroController : Controller
    {
        private IBaseService<SistemaParametro> _service;

        public SistemaParametroController(IBaseService<SistemaParametro> service)
        {
            _service = service;
        }

        // GET: Controle/SistemaParametro
        public ActionResult Index()
        {
            var parametros = _service.Listar()
                .OrderBy(x => x.Codigo);

            return View(parametros);
        }

        // GET: Controle/SistemaParametro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Controle/SistemaParametro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Controle/SistemaParametro/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Controle/SistemaParametro/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Controle/SistemaParametro/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Controle/SistemaParametro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Controle/SistemaParametro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
