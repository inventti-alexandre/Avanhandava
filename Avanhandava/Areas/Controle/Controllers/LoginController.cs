using Avanhandava.Areas.Controle.Models;
using Avanhandava.Domain.Abstract.Admin;
using System.Web.Mvc;
using System.Web.Security;

namespace Avanhandava.Areas.Controle.Controllers
{
    public class LoginController : Controller
    {
        private ILogin _login;

        public LoginController(ILogin login)
        {
            _login = login;
        }

        // GET: Controle/Login
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUtl = returnUrl;
            return View(new LoginUsuario());
        }

        [HttpPost]
        public ActionResult Index(LoginUsuario loginUsuario, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var usuario = _login.ValidaLogin(loginUsuario.Login, loginUsuario.Senha);

                if (usuario != null)
                {
                    FormsAuthentication.SetAuthCookie(usuario.Login, false);
                    Session["IdUsuario"] = usuario.Id;
                    if (Url.IsLocalUrl(returnUrl)
                        && returnUrl.Length > 1
                        && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//")
                        && !returnUrl.StartsWith(@"\//"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário inválido");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(loginUsuario);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}