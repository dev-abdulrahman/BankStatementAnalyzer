using BankStatementAnalyzer.SuperAdmin.Models;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BankStatementAnalyzer.SuperAdmin.Common.HtmlHelperExtension
{
    public static class HtmlHelperProvider
    {
        public static MvcHtmlString LabelWithHelpFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("Div");

            StringBuilder innerString = new StringBuilder();
            innerString.AppendLine(html.Partial("InfoIconPV", new InfoIconModel() { LabelText = html.LabelFor(expression, htmlAttributes).ToString() }).ToString());
            tagBuilder.InnerHtml = innerString.ToString();
            return new MvcHtmlString(tagBuilder.ToString());
        }
    }
}