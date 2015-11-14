using System.Web.Mvc;

namespace Avanhandava.Areas.Controle
{
    public class ControleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Controle";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Controle",
                "Controle",
                new { Controller = "Home", action = "Index" }
            );

            context.MapRoute(
                "TrocarSenha",
                "TrocarSenha",
                new { Controller = "Usuario", action = "TrocarSenha" }
            );

            context.MapRoute(
                "Controle_default",
                "Controle/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}