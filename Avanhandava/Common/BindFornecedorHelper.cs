using Avanhandava.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Avanhandava.Common
{
    public static class BindFornecedorHelper
    {
        public static MvcHtmlString SelectFornecedor(this HtmlHelper html, int idFornecedor, bool todas = false)
        {
            var fornecedores = new FornecedorService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Fantasia)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdFornecedor");
            tag.MergeAttribute("name", "IdFornecedor");
            tag.MergeAttribute("class", "form-control");

            if (todas == true)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", "0");
                itemTag.SetInnerText("");
                tag.InnerHtml += itemTag.ToString();
            }
            
            foreach (var item in fornecedores)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idFornecedor)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.SetInnerText(item.Fantasia);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());

        }
    }
}