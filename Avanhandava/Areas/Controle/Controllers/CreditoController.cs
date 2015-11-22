using Avanhandava.Areas.Controle.Models;
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
    public class CreditoController : Controller
    {
        private IBaseService<Credito> _service;
        private IBaseService<Empresa> _empresa;
        private IBaseService<Conta> _conta;

        public CreditoController(IBaseService<Credito> service, IBaseService<Empresa> empresa, IBaseService<Conta> conta)
        {
            _service = service;
            _empresa = empresa;
            _conta = conta;
        }

        // GET: Controle/Credito
        public ActionResult Index(int idEmpresa = 0, int idConta = 0, int idTipoCredito = 0, DateTime? dataCredito = null, DateTime? referencia = null)
        {
            CreditoModel model = new CreditoModel();
            model.IdEmpresa = idEmpresa != 0 ? idEmpresa : _empresa.Listar().Where(x => x.Ativo == true).OrderBy(x => x.Fantasia).FirstOrDefault().Id;
            model.IdConta = idConta != 0 ? idConta : _conta.Listar().Where(x => x.IdEmpresa == model.IdEmpresa && x.Ativo == true).FirstOrDefault().Id;
            model.IdTipoCredito = idTipoCredito;
            model.DataCredito = dataCredito == null ? DateTime.Today.Date : dataCredito;
            model.Referencia = referencia == null ? DateTime.Today.Date : referencia;

            return View(model);
        }

        // GET: Controle/Credito/Details/5
        [HttpPost]
        public ActionResult Index([Bind(Include="IdEmpresa,IdConta")] CreditoModel model)
        {
            Credito credito = new Credito { IdConta = model.IdConta, IdEmpresa = model.IdEmpresa, DataCredito = DateTime.Today.Date, Referencia = DateTime.Today.Date };
            return View("Incluir", credito);
        }

        // POST: Controle/Credito/Create
        [HttpPost]
        public ActionResult Incluir(Credito credito)
        {
            try
            {
                credito.AlteradoEm = DateTime.Now;
                credito.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(credito);

                if (ModelState.IsValid)
                {
                    _service.Gravar(credito);
                    return RedirectToAction("Index", new { idEmpresa = credito.IdEmpresa, idConta = credito.IdConta });
                }

                return View(credito);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(credito);
            }
        }

        // GET: Controle/Credito/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var credito = _service.Find((int)id);

            if (credito == null)
            {
                return HttpNotFound();
            }

            return View(credito);
        }

        // POST: Controle/Credito/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include="Id,IdEmpresa,IdConta,IdTipoCredito,Referencia,DataCredito,Descricao,Valor,Ativo")] Credito credito)
        {
            try
            {
                credito.AlteradoEm = DateTime.Now;
                credito.AlteradoPor = Identification.IdUsuario;

                if (ModelState.IsValid)
                {
                    _service.Gravar(credito);
                    return RedirectToAction("Index", new { idEmpresa = credito.IdEmpresa, idConta = credito.IdConta });
                }

                return View(credito);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        // GET: Controle/Credito/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var credito = _service.Find((int)id);

            if (credito == null)
            {
                return HttpNotFound();
            }
            
            return View(credito);
        }

        // POST: Controle/Credito/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var credito = _service.Excluir(id);
                
                if (credito == null)
	            {
                    return RedirectToAction("Index");		 
	            }

                return RedirectToAction("Index", new { idEmpresa = credito.IdEmpresa, idConta = credito.IdConta });
            }
            catch
            {
                var credito = _service.Find(id);
                if (credito == null)
                {
                    return HttpNotFound();
                }
                return View(credito);
            }
        }

        public PartialViewResult Creditos(int idEmpresa, int idConta)
        {
            DateTime dataMinima;

            var conta = _conta.Find(idConta);
            if (conta == null)
            {
                dataMinima = DateTime.Today.Date;
            }
            {
                dataMinima = conta.SaldoData;
            }

            var creditos = _service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa
                && x.IdConta == idConta
                && x.DataCredito >= dataMinima)
                .OrderBy(x => x.Id)
                .ToList();

            return PartialView(creditos);
        }
    }
}
