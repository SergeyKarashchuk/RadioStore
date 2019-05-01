using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RadioStore.WebApplication.Infrastructure.HtmlHelpers
{
    public static class HtmlHelperExtencions {
        public static MvcHtmlString ActionLinkWithImage(this HtmlHelper html, string action, 
                                                            string controllerName, object routeValues, 
                                                            string imagePath, string alt, 
                                                            object imageHtmlAttributes = null, object actionHtmlAttributes = null)                                 
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            if (imageHtmlAttributes != null)
            {
                var imgAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(imageHtmlAttributes);
                imgBuilder.MergeAttributes(imgAttributes);
            }
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            var aBuilder = new TagBuilder("a");
            aBuilder.MergeAttribute("href", url.Action(action, controllerName, routeValues));
            if (actionHtmlAttributes != null)
            {
                var actionAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(actionHtmlAttributes);
                aBuilder.MergeAttributes(actionAttributes);
            }
            aBuilder.InnerHtml = imgHtml;
            string aHtml = aBuilder.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(aHtml);
        }
    }
}