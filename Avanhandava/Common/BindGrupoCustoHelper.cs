using Avanhandava.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Avanhandava.Common
{
    public static class BindGrupoCustoHelper
    {
        public static MvcHtmlString SelectGrupoCusto(this HtmlHelper html, int idGrupoCusto = 0, bool todas = false)
        {
            var grupos = new GrupoCustoService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdGrupoCusto");
            tag.MergeAttribute("name", "IdGrupoCusto");
            tag.MergeAttribute("class", "form-control");

            if (todas == true)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", "0");
                itemTag.SetInnerText("");
                tag.InnerHtml += itemTag.ToString();
            }

            foreach (var item in grupos)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idGrupoCusto)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.SetInnerText(item.Descricao);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}