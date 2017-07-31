using System.IO;
using System.Web.Mvc;

namespace SmartShop.Utilities.Helper
{
    public static class MvcHelper
    {
        public static MvcHtmlString PartialIfExist(this HtmlHelper html, string name, object model = null)
        {
            var controllerContext = html.ViewContext.Controller.ControllerContext;

            ViewDataDictionary viewData = new ViewDataDictionary();

            if (model != null)
                viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, name);
    
                if (viewResult.View == null)
                    return MvcHtmlString.Create(string.Empty);

                var viewContext = new ViewContext(controllerContext, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return MvcHtmlString.Create(sw.GetStringBuilder().ToString());
            }
        }
    }
}
