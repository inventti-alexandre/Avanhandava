using Avanhandava.Domain.Models.Admin;
using System;
using System.Web.Mvc;

namespace Avanhandava.Common
{
    public static class BindSituacaoParcelaHelper
    {
        public static MvcHtmlString SelectSituacao(this HtmlHelper html, Situacao situacao)
        {
            string[] names = Enum.GetNames(typeof(Situacao));
            Array values = Enum.GetValues(typeof(Situacao));

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdSituacao");
            tag.MergeAttribute("name", "IdSituacao");
            tag.MergeAttribute("class", "form-control");

            foreach (var item in names)
            {
                int value = (int)Enum.Parse(typeof(Situacao), item);

                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", value.ToString());
                if (value == (int)Convert.ChangeType(situacao, situacao.GetTypeCode()))
                {
                    itemTag.MergeAttribute("selected", "selected");
                }

                if (value == (int)Situacao.EmAberto)
                {
                    itemTag.SetInnerText("EM ABERTO");                    
                }
                else if (value == (int)Situacao.Pagos)
                {
                    itemTag.SetInnerText("PAGOS");
                }
                else
                {
                    itemTag.SetInnerText("");
                }
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}