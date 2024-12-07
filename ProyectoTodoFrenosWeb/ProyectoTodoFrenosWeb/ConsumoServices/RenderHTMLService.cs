using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class RenderHTMLService
    {
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        public RenderHTMLService(ICompositeViewEngine _viewEngine, ITempDataProvider _tempDataProvider)
        {
            this._viewEngine = _viewEngine;
            this._tempDataProvider = _tempDataProvider;
        }
        public async Task<string> RenderViewAsStringAsync(ControllerContext controllerContext,string viewName, object model)
        {
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
            using (var writer = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(controllerContext, viewName, false);
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"La vista '{viewName}' no fue encontrada.");
                }
                var viewContext = new ViewContext(
                    controllerContext,
                    viewResult.View,
                    viewData,
                    new TempDataDictionary(controllerContext.HttpContext, _tempDataProvider),
                    writer,
                    new HtmlHelperOptions()
                );
                await viewResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
