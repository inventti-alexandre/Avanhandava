using Avanhandava.Common;
using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Abstract.Admin;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Avanhandava.Areas.Controle.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private IBaseService<Usuario> _service;
        private ITrocaSenha _trocaSenha;

        public UsuarioController(IBaseService<Usuario> service, ITrocaSenha trocaSenha)
        {
            _service = service;
            _trocaSenha = trocaSenha;
        }

        // GET: Controle/Usuario
        public ActionResult Index()
        {
            var usuarios = _service.Listar()
                .OrderBy(x => x.Nome)
                .ToList();

            return View(usuarios);
        }

        // GET: Controle/Usuario/Details/5
        public ActionResult Detalhes(int id)
        {
            var usuario = _service.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // GET: Controle/Usuario/Create
        public ActionResult Incluir()
        {
            return View(new Usuario());
        }

        // POST: Controle/Usuario/Create
        [HttpPost]
        public ActionResult Incluir([Bind(Include = "Nome,Email,Login,Senha,Telefone,Ramal")] Usuario usuario)
        {
            try
            {
                usuario.Roles = string.Empty;
                usuario.CadastradoEm = DateTime.Now;
                TryUpdateModel(usuario);

                if (ModelState.IsValid)
                {
                    _service.Gravar(usuario);
                    return RedirectToAction("Index");    
                }
                return View(usuario);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(usuario);
            }
        }

        // GET: Controle/Usuario/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = _service.Find((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            usuario.Senha = string.Empty;
            return View(usuario);
        }

        // POST: Controle/Usuario/Edit/5
        [HttpPost]
        public ActionResult Editar([Bind(Include = "Id,Nome,Email,Login,Senha,Ativo,CadastradoEm,ExcluidoEm,Telefone,Ramal")] Usuario usuario)
        {
            try
            {
                usuario.Roles = string.Empty;
                TryUpdateModel(usuario);

                if (ModelState.IsValid)
                {
                    _service.Gravar(usuario);
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(usuario);
            }
        }

        // GET: Controle/Usuario/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = _service.Find((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Controle/Usuario/Delete/5
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
                var usuario = _service.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
        }

        // GET: Controle/Usuario/TrocarSenha
        public ActionResult TrocarSenha()
        {
            var usuario = _service.Find(Identification.IdUsuario);

            if (usuario == null)
            {
                return HttpNotFound();   
            }

            var troca = new TrocaSenha { IdUsuario = usuario.Id };

            return View(troca);
        }

        // POST: Controle/Usuario/TrocarSenha
        [HttpPost]
        public ActionResult TrocarSenha(TrocaSenha troca)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _trocaSenha.TrocarSenha(troca.IdUsuario, troca.SenhaAtual, troca.NovaSenha, false);
                    return View("SenhaAlterada");
                }

                return View(troca);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(troca);
            }
        }

        // GET: Controle/Usuario/RedefinirSenha/5
        public ActionResult RedefinirSenha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = _service.Find((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Controle/Usuario/RedefinirSenha/5
        [HttpPost]
        public ActionResult RedefinirSenha(int id)
        {
            var usuario = _service.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            try
            {
                _trocaSenha.RedefinirSenha(id);

                ViewBag.Nome = usuario.Nome;
                ViewBag.Email = usuario.Email;
                return View("SenhaRedefinida");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(usuario);
            }
        }
    }
}
