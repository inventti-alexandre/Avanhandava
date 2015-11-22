using Avanhandava.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avanhandava.Common
{
    public static class BindPgtoHelper
    {
        public static MvcHtmlString SelectPgto(this HtmlHelper html, int idPgto = 0, bool todas = false)
        {
            var pgtos = new PgtoService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdPgto");
            tag.MergeAttribute("name", "IdPgto");
            tag.MergeAttribute("class", "form-control");

            if (todas == true)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", "0");
                itemTag.SetInnerText("");
                tag.InnerHtml += itemTag.ToString();
            }

            foreach (var item in pgtos)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idPgto)
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