using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Week3.MyHtmlComponents
{
    public static class MyHtmlHelper
    {
        public static IHtmlContent BoldText(this IHtmlHelper htmlHelper, string text)
        {
            return new HtmlString($"<strong>{text}</strong>");

        }
    }
}
