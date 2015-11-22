using Avanhandava.Common;
using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Abstract.Admin;
using Avanhandava.Domain.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avanhandava.Areas.Controle.Controllers
{
    public class AgendamentoController : Controller
    {
        private IBaseService<Agendamento> _service;

        public AgendamentoController(IBaseService<Agendamento> service, ILogin login)
        {
            _service = service;
        }

        // GET: Controle/Agendamento
        public ActionResult Index(int id = 0, int idEmpresa = 0, int idGrupoCusto = 0, int idItemCusto = 0, int idFornecedor = 0, DateTime? referencia = null)
        {
            Agendamento agendamento = new Agendamento();

            if (id > 0)
            {
                agendamento = _service.Find(id);
                if (agendamento == null)
                {
                    id = 0;
                }
            }

            if (id == 0)
            {
                agendamento = new Agendamento { IdEmpresa = idEmpresa, IdGrupoCusto = idGrupoCusto, IdItemCusto = idItemCusto, IdFornecedor = idFornecedor };
                if (referencia == null)
                {
                    agendamento.Referencia = DateTime.Today.Date;
                }
                else
                {
                    agendamento.Referencia = (DateTime)referencia;
                }                
            }

            return View(agendamento);
        }

        // POST: Controle/Agendamento
        [HttpPost]
        public ActionResult Index(Agendamento agendamento, FormCollection collection)
        {
            try
            {
                agendamento.Observ = string.Empty;
                agendamento.Ativo = true;
                agendamento.AlteradoEm = DateTime.Now;
                agendamento.AlteradoPor = Identification.IdUsuario;

                if (agendamento.Id == 0)
                {
                    agendamento.CadastradoEm = DateTime.Now;
                    agendamento.CadastradoPor = Identification.IdUsuario;
                }

                if (ModelState.IsValid)
                {
                    if (agendamento.Id == 0)
                    {
                        return RedirectToAction("Incluir", "Parcela", new { idAgendamento = _service.Gravar(agendamento) });                        
                    }
                    else
                    {
                        // TODO: TEM QUE TER INTERACAO COM O USUARIO NA TELA INDEX
                        return RedirectToAction("Index", new { id = _service.Gravar(agendamento) });                        
                    }
                }

                return View(agendamento);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(agendamento);
            }
        }

        // GET: Controle/Agendamento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Controle/Agendamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Controle/Agendamento/Create
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

        // GET: Controle/Agendamento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Controle/Agendamento/Edit/5
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

        // GET: Controle/Agendamento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Controle/Agendamento/Delete/5
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
