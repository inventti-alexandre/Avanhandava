using Avanhandava.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Avanhandava.Common
{
    public static class BindItemCustoHelper
    {
        public static MvcHtmlString SelectItemCusto(this HtmlHelper html, int idItemCusto = 0, int idGrupoCusto = 0, bool todas = false)
        {
            var itens = new ItemCustoService().Listar()
                .Where(x => x.Ativo == true
                    && (idGrupoCusto == 0 || x.IdGrupoCusto == idGrupoCusto))
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdItemCusto");
            tag.MergeAttribute("name", "IdItemCusto");
            tag.MergeAttribute("class", "form-control");

            if (todas == true)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", "0");
                itemTag.SetInnerText("");
                tag.InnerHtml += itemTag.ToString();
            }

            foreach (var item in itens)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idItemCusto)
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