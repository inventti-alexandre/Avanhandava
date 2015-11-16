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
    public class ItemController : Controller
    {
        private IBaseService<ItemCusto> _service;
        private IBaseService<GrupoCusto> _grupo;
        private ILogin _login;

        public ItemController(IBaseService<ItemCusto> service, IBaseService<GrupoCusto> grupo, ILogin login)
        {
            _service = service;
            _grupo = grupo;
            _login = login;
        }

        // GET: Controle/Item
        public ActionResult Index(int idGrupoCusto)
        {
            var itens = _service.Listar()
                .Where(x => x.IdGrupoCusto == idGrupoCusto)
                .OrderBy(x => x.Descricao);

            ViewBag.GrupoCusto = _grupo.Find(idGrupoCusto).Descricao.ToLower();
            ViewBag.IdGrupoCusto = idGrupoCusto;
            return View(itens);
        }

        // GET: Controle/Item/Details/5
        public ActionResult Detalhes(int id)
        {
            var itemCusto = _service.Find(id);

            if (itemCusto == null)
            {
                return HttpNotFound();
            }

            return View(itemCusto);
        }

        // GET: Controle/Item/Incluir
        public ActionResult Incluir(int idGrupoCusto)
        {
            return View(new ItemCusto { IdGrupoCusto = idGrupoCusto });
        }

        // POST: Controle/Item/Incluir
        [HttpPost]
        public ActionResult Incluir([Bind(Include="IdGrupoCusto,Descricao")] ItemCusto itemCusto)
        {
            try
            {
                itemCusto.AlteradoEm = DateTime.Now;
                itemCusto.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(itemCusto);

                if (ModelState.IsValid)
                {
                    _service.Gravar(itemCusto);
                    return RedirectToAction("Index", new { idGrupoCusto = itemCusto.IdGrupoCusto });    
                }

                return View(itemCusto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(itemCusto);
            }
        }

        // GET: Controle/Item/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var itemCusto = _service.Find((int)id);

            if (itemCusto == null)
            {
                return HttpNotFound();   
            }

            return View(itemCusto);
        }

        // POST: Controle/Item/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,IdGrupoCusto,Descricao,Ativo")] ItemCusto itemCusto)
        {
            try
            {
                itemCusto.AlteradoEm = DateTime.Now;
                itemCusto.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(itemCusto);

                if (ModelState.IsValid)
                {
                    _service.Gravar(itemCusto);
                    return RedirectToAction("Index", new { idGrupoCusto = itemCusto.IdGrupoCusto });
                }
                return View(itemCusto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(itemCusto);
            }
        }

        // GET: Controle/Item/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var itemCusto = _service.Find((int)id);

            if (itemCusto == null)
            {
                return HttpNotFound();
            }

            return View(itemCusto);
        }

        // POST: Controle/Item/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var itemCusto = _service.Excluir(id);
                return RedirectToAction("Index", new { idGrupoCusto = itemCusto.IdGrupoCusto });    
            }
            catch
            {
                var itemCusto = _service.Find(id);
                if (itemCusto == null)
                {
                    return HttpNotFound();
                }
                return View(itemCusto);
            }
        }
    }
}
