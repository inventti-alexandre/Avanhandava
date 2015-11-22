using Avanhandava.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Avanhandava.Common
{
    public static class BindTipoCreditoHelper
    {
        public static MvcHtmlString SelectTipoCredito(this HtmlHelper html, int idTipoCredito = 0, bool todas = false)
        {
            var tipos = new TipoCreditoService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdTipoCredito");
            tag.MergeAttribute("name", "IdTipoCredito");
            tag.MergeAttribute("class", "form-control");

            if (todas == true)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", "0");
                itemTag.SetInnerText("");
                tag.InnerHtml += itemTag.ToString();
            }

            foreach (var item in tipos)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idTipoCredito)
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