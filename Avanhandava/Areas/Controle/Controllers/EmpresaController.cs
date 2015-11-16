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
    public class EmpresaController : Controller
    {
        private IBaseService<Empresa> _service;
        private ILogin _login;

        public EmpresaController(IBaseService<Empresa> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        // GET: Controle/Empresa
        public ActionResult Index()
        {
            var empresas = _service.Listar()
                .OrderBy(x => x.Fantasia);

            return View(empresas);
        }

        // GET: Controle/Empresa/Details/5
        public ActionResult Detalhes(int id)
        {
            var empresa = _service.Find(id);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            return View(empresa);
        }

        // GET: Controle/Empresa/Create
        public ActionResult Incluir()
        {
            return View(new Empresa());
        }

        // POST: Controle/Empresa/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include="Fantasia,RazaoSocial,Cnpj,IE,Ccm,Endereco,Bairro,Cidade,IdEstado,Cep,Observ,Telefone,Email")] Empresa empresa)
        {
            try
            {
                empresa.AlteradoEm = DateTime.Now;
                empresa.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(empresa);

                if (ModelState.IsValid)
                {
                    _service.Gravar(empresa);
                    return RedirectToAction("Index");    
                }
                return View(empresa);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(empresa);
            }
        }

        // GET: Controle/Empresa/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empresa = _service.Find((int)id);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            return View(empresa);
        }

        // POST: Controle/Empresa/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include = "Id,Ativo,Fantasia,RazaoSocial,Cnpj,IE,Ccm,Endereco,Bairro,Cidade,IdEstado,Cep,Observ,Telefone,Email")] Empresa empresa)
        {
            try
            {
                empresa.AlteradoEm = DateTime.Now;
                empresa.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(empresa);

                if (ModelState.IsValid)
                {
                    _service.Gravar(empresa);
                    return RedirectToAction("Index");
                }
                return View(empresa);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(empresa);
            }
        }

        // GET: Controle/Empresa/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empresa = _service.Find((int)id);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            return View(empresa);
        }

        // POST: Controle/Empresa/Delete/5
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
                var empresa = _service.Find(id);
                if (empresa == null)
                {
                    return HttpNotFound();
                }
                return View(empresa);
            }
        }
    }
}
