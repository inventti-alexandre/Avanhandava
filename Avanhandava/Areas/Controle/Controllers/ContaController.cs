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
    public class ContaController : Controller
    {
        private IBaseService<Conta> _service;
        private IBaseService<Empresa> _empresa;
        private ILogin _login;

        public ContaController(IBaseService<Conta> service, IBaseService<Empresa> empresa, ILogin login)
        {
            _service = service;
            _empresa = empresa;
            _login = login;
        }

        // GET: Controle/Conta
        public ActionResult Index(int idEmpresa)
        {
            var empresa = _empresa.Find(idEmpresa);
            if (empresa == null)
            {
                return HttpNotFound();
            }

            var contas = _service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();
            
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.Fantasia = empresa.Fantasia;
            return View(contas);
        }

        // GET: Controle/Conta/Details/5
        public ActionResult Detalhes(int id)
        {
            var conta = _service.Find(id);

            if (conta == null)
            {
                return HttpNotFound();
            }

            return View(conta);
        }

        // GET: Controle/Conta/Create
        public ActionResult Incluir(int idEmpresa)
        {
            return View(new Conta { IdEmpresa = idEmpresa});
        }

        // POST: Controle/Conta/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include="IdEmpresa,Descricao,BancoNome,BancoNum,BancoAgencia,BancoConta")] Conta conta)
        {
            try
            {
                conta.AlteradoEm = DateTime.Now;
                conta.AlteradoPor = Identification.IdUsuario;
                conta.SaldoData = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                TryUpdateModel(conta);

                if (ModelState.IsValid)
                {
                    _service.Gravar(conta);
                    return RedirectToAction("Index", new { idEmpresa = conta.IdEmpresa });
                }
                return View(conta);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(conta);
            }
        }

        // GET: Controle/Conta/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var conta = _service.Find((int)id);

            if (conta == null)
            {
                return HttpNotFound();
            }

            return View(conta);
        }

        // POST: Controle/Conta/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include = "Id,IdEmpresa,Descricao,BancoNome,BancoNum,BancoAgencia,BancoConta,SaldoAnterior,SaldoAtual,SaldoData,Ativo")] Conta conta)
        {
            try
            {
                conta.AlteradoEm = DateTime.Now;
                conta.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(conta);

                if (ModelState.IsValid)
                {
                    _service.Gravar(conta);
                    return RedirectToAction("Index", new { idEmpresa = conta.IdEmpresa });
                }
                return View(conta);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(conta);
            }
        }

        // GET: Controle/Conta/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var conta = _service.Find((int)id);

            if (conta == null)
            {
                return HttpNotFound();
            }

            return View(conta);
        }

        // POST: Controle/Conta/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var conta = _service.Excluir(id);
                return RedirectToAction("Index", new { idEmpresa = conta.IdEmpresa });
            }
            catch
            {
                var conta = _service.Find(id);
                if (conta == null)
                {
                    return HttpNotFound();
                }
                return View(conta);
            }
        }
    }
}
