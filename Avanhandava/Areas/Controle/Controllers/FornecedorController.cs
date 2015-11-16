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
    public class FornecedorController : Controller
    {
        private IBaseService<Fornecedor> _service;
        private ILogin _login;

        public FornecedorController(IBaseService<Fornecedor> service, ILogin login)
        {
            _service = service;
            _login = login;
        }

        // GET: Controle/Fornecedor
        public ActionResult Index()
        {
            var fornecedores = _service.Listar()
                .OrderBy(x => x.Fantasia);

            return View(fornecedores);
        }

        // GET: Controle/Fornecedor/Details/5
        public ActionResult Detalhes(int id)
        {
            var fornecedor = _service.Find(id);

            if (fornecedor == null)
            {
                return HttpNotFound();
            }

            return View(fornecedor);
        }

        // GET: Controle/Fornecedor/Create
        public ActionResult Incluir()
        {
            return View(new Fornecedor());
        }

        // POST: Controle/Fornecedor/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include = "Fantasia,RazaoSocial,Cnpj,IE,Ccm,Endereco,Bairro,Cidade,IdEstado,Cep,Observ,Telefone,Email")] Fornecedor fornecedor)
        {
            try
            {
                fornecedor.AlteradoEm = DateTime.Now;
                fornecedor.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(fornecedor);

                if (ModelState.IsValid)
                {
                    _service.Gravar(fornecedor);
                    return RedirectToAction("Index");    
                }
                return View(fornecedor);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(fornecedor);
            }
        }

        // GET: Controle/Fornecedor/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fornecedor = _service.Find((int)id);

            if (fornecedor == null)
            {
                return HttpNotFound();
            }

            return View(fornecedor);
        }

        // POST: Controle/Fornecedor/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include = "Id,Ativo,Fantasia,RazaoSocial,Cnpj,IE,Ccm,Endereco,Bairro,Cidade,IdEstado,Cep,Observ,Telefone,Email")] Fornecedor fornecedor)
        {
            try
            {
                fornecedor.AlteradoEm = DateTime.Now;
                fornecedor.AlteradoPor = Identification.IdUsuario;
                TryUpdateModel(fornecedor);

                if (ModelState.IsValid)
                {
                    _service.Gravar(fornecedor);
                    return RedirectToAction("Index");
                }
                return View(fornecedor);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(fornecedor);
            }
        }

        // GET: Controle/Fornecedor/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fornecedor = _service.Find((int)id);

            if (fornecedor == null)
            {
                return HttpNotFound();
            }

            return View(fornecedor);
        }

        // POST: Controle/Fornecedor/Delete/5
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
                var fornecedor = _service.Find(id);
                if (fornecedor == null)
                {
                    return HttpNotFound();
                }
                return View(fornecedor);
            }
        }
    }
}
