using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.AspNetCore.Infrastructure
{
    [ViewComponent]
    public abstract class FoundationViewComponent
    {
        private IUrlHelper _url;
        private dynamic _viewBag;
        private ViewComponentContext _viewComponentContext;
        private ICompositeViewEngine _viewEngine;

        /// <summary>
        /// Gets the <see cref="Http.HttpContext"/>.
        /// </summary>
        public HttpContext HttpContext => ViewContext?.HttpContext;

        /// <summary>
        /// Gets the <see cref="HttpRequest"/>.
        /// </summary>
        public HttpRequest Request => ViewContext?.HttpContext?.Request;

        /// <summary>
        /// Gets the <see cref="IPrincipal"/> for the current user.
        /// </summary>
        public IPrincipal User => ViewContext?.HttpContext?.User;

        /// <summary>
        /// Gets the <see cref="ClaimsPrincipal"/> for the current user.
        /// </summary>
        public ClaimsPrincipal UserClaimsPrincipal => ViewContext?.HttpContext?.User;

        /// <summary>
        /// Gets the <see cref="RouteData"/> for the current request.
        /// </summary>
        public RouteData RouteData => ViewContext?.RouteData;

        /// <summary>
        /// Gets the view bag.
        /// </summary>
        public dynamic ViewBag
        {
            get
            {
                //if (_viewBag == null)
                //{
                //    _viewBag = new DynamicViewData(() => ViewData);
                //}

                return _viewBag;
            }
        }

        /// <summary>
        /// Gets the <see cref="ModelStateDictionary"/>.
        /// </summary>
        public ModelStateDictionary ModelState => ViewData?.ModelState;

        /// <summary>
        /// Gets or sets the <see cref="IUrlHelper"/>.
        /// </summary>
        public IUrlHelper Url
        {
            get
            {
                if (_url == null)
                {
                    // May be null in unit-testing scenarios.
                    var services = ViewComponentContext.ViewContext?.HttpContext?.RequestServices;
                    var factory = services?.GetRequiredService<IUrlHelperFactory>();
                    _url = factory?.GetUrlHelper(ViewComponentContext.ViewContext);
                }

                return _url;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _url = value;
            }
        }

        [ViewComponentContext]
        public ViewComponentContext ViewComponentContext
        {
            get
            {
                // This should run only for the ViewComponent unit test scenarios.
                if (_viewComponentContext == null)
                {
                    _viewComponentContext = new ViewComponentContext();
                }

                return _viewComponentContext;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _viewComponentContext = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="ViewContext"/>.
        /// </summary>
        public ViewContext ViewContext => ViewComponentContext.ViewContext;

        /// <summary>
        /// Gets the <see cref="ViewDataDictionary"/>.
        /// </summary>
        public ViewDataDictionary ViewData => ViewComponentContext.ViewData;

        /// <summary>
        /// Gets the <see cref="ITempDataDictionary"/>.
        /// </summary>
        public ITempDataDictionary TempData => ViewComponentContext.TempData;

        /// <summary>
        /// Gets or sets the <see cref="ICompositeViewEngine"/>.
        /// </summary>
        public ICompositeViewEngine ViewEngine
        {
            get
            {
                if (_viewEngine == null)
                {
                    // May be null in unit-testing scenarios.
                    var services = ViewComponentContext.ViewContext?.HttpContext?.RequestServices;
                    _viewEngine = services?.GetRequiredService<ICompositeViewEngine>();
                }

                return _viewEngine;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _viewEngine = value;
            }
        }

        /// <summary>
        /// Returns a result which will render HTML encoded text.
        /// </summary>
        /// <param name="content">The content, will be HTML encoded before output.</param>
        /// <returns>A <see cref="ContentViewComponentResult"/>.</returns>
        public ContentViewComponentResult Content(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return new ContentViewComponentResult(content);
        }

        /// <summary>
        /// Returns a result which will render the partial view with name <c>&quot;Default&quot;</c>.
        /// </summary>
        /// <returns>A <see cref="ViewViewComponentResult"/>.</returns>
        public CustomViewComponentResult View()
        {
            return View(viewName: null);
        }

        /// <summary>
        /// Returns a result which will render the partial view with name <paramref name="viewName"/>.
        /// </summary>
        /// <param name="viewName">The name of the partial view to render.</param>
        /// <returns>A <see cref="ViewViewComponentResult"/>.</returns>
        public CustomViewComponentResult View(string viewName)
        {
            return View(viewName, ViewData.Model);
        }

        /// <summary>
        /// Returns a result which will render the partial view with name <c>&quot;Default&quot;</c>.
        /// </summary>
        /// <param name="model">The model object for the view.</param>
        /// <returns>A <see cref="ViewViewComponentResult"/>.</returns>
        public CustomViewComponentResult View<TModel>(TModel model)
        {
            return View(viewName: null, model: model);
        }

        /// <summary>
        /// Returns a result which will render the partial view with name <paramref name="viewName"/>.
        /// </summary>
        /// <param name="viewName">The name of the partial view to render.</param>
        /// <param name="model">The model object for the view.</param>
        /// <returns>A <see cref="ViewViewComponentResult"/>.</returns>
        public CustomViewComponentResult View<TModel>(string viewName, TModel model)
        {
            var viewData = new ViewDataDictionary<TModel>(ViewData, model);
            return new CustomViewComponentResult
            {
                ViewEngine = ViewEngine,
                ViewName = viewName,
                ViewData = viewData
            };
        }
    }

    public class CustomViewComponentResult : IViewComponentResult
    {
        // {0} is the component name, {1} is the view name.
        private const string ViewPathFormat = "{1}";
        private const string DefaultViewName = "Default";

        private DiagnosticListener _diagnosticListener;

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ViewDataDictionary"/>.
        /// </summary>
        public ViewDataDictionary ViewData { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ITempDataDictionary"/> instance.
        /// </summary>
        public ITempDataDictionary TempData { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ViewEngine"/>.
        /// </summary>
        public IViewEngine ViewEngine { get; set; }

        /// <summary>
        /// Locates and renders a view specified by <see cref="ViewName"/>. If <see cref="ViewName"/> is <c>null</c>,
        /// then the view name searched for is<c>&quot;Default&quot;</c>.
        /// </summary>
        /// <param name="context">The <see cref="ViewComponentContext"/> for the current component execution.</param>
        /// <remarks>
        /// This method synchronously calls and blocks on <see cref="ExecuteAsync(ViewComponentContext)"/>.
        /// </remarks>
        public void Execute(ViewComponentContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var task = ExecuteAsync(context);
            task.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Locates and renders a view specified by <see cref="ViewName"/>. If <see cref="ViewName"/> is <c>null</c>,
        /// then the view name searched for is<c>&quot;Default&quot;</c>.
        /// </summary>
        /// <param name="context">The <see cref="ViewComponentContext"/> for the current component execution.</param>
        /// <returns>A <see cref="Task"/> which will complete when view rendering is completed.</returns>
        public async Task ExecuteAsync(ViewComponentContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var viewEngine = ViewEngine ?? ResolveViewEngine(context);
            var viewContext = context.ViewContext;
            var isNullOrEmptyViewName = string.IsNullOrEmpty(ViewName);

            ViewEngineResult result = null;
            IEnumerable<string> originalLocations = null;
            if (!isNullOrEmptyViewName)
            {
                // If view name was passed in is already a path, the view engine will handle this.
                result = viewEngine.GetView(viewContext.ExecutingFilePath, ViewName, isMainPage: false);
                originalLocations = result.SearchedLocations;
            }

            if (result == null || !result.Success)
            {
                // This will produce a string like:
                //
                //  Components/Cart/Default
                //
                // The view engine will combine this with other path info to search paths like:
                //
                //  Views/Shared/Components/Cart/Default.cshtml
                //  Views/Home/Components/Cart/Default.cshtml
                //  Areas/Blog/Views/Shared/Components/Cart/Default.cshtml
                //
                // This supports a controller or area providing an override for component views.
                var viewName = isNullOrEmptyViewName ? DefaultViewName : ViewName;
                var qualifiedViewName = string.Format(
                    CultureInfo.InvariantCulture,
                    ViewPathFormat,
                    context.ViewComponentDescriptor.ShortName,
                    viewName);

                result = viewEngine.FindView(viewContext, qualifiedViewName, isMainPage: false);
            }

            var view = result.EnsureSuccessful(originalLocations).View;
            using (view as IDisposable)
            {
                if (_diagnosticListener == null)
                {
                    _diagnosticListener = viewContext.HttpContext.RequestServices.GetRequiredService<DiagnosticListener>();
                }

                _diagnosticListener.ViewComponentBeforeViewExecute(context, view);

                var childViewContext = new ViewContext(
                    viewContext,
                    view,
                    ViewData ?? context.ViewData,
                    context.Writer);
                await view.RenderAsync(childViewContext);

                _diagnosticListener.ViewComponentAfterViewExecute(context, view);
            }
        }

        private static IViewEngine ResolveViewEngine(ViewComponentContext context)
        {
            return context.ViewContext.HttpContext.RequestServices.GetRequiredService<ICompositeViewEngine>();
        }
    }

    public static class ViewFeatureExtensions
    {
        public static void ViewComponentBeforeViewExecute(
            this DiagnosticListener diagnosticListener,
            ViewComponentContext context,
            IView view)
        {
            // Inlinable fast-path check if Diagnositcs is enabled
            if (diagnosticListener.IsEnabled())
            {
                ViewComponentBeforeViewExecuteImpl(diagnosticListener, context, view);
            }
        }

        private static void ViewComponentBeforeViewExecuteImpl(DiagnosticListener diagnosticListener, ViewComponentContext context, IView view)
        {
            if (diagnosticListener.IsEnabled("Microsoft.AspNetCore.Mvc.ViewComponentBeforeViewExecute"))
            {
                diagnosticListener.Write(
                    "Microsoft.AspNetCore.Mvc.ViewComponentBeforeViewExecute",
                    new
                    {
                        actionDescriptor = context.ViewContext.ActionDescriptor,
                        viewComponentContext = context,
                        view = view
                    });
            }
        }
        public static void ViewComponentAfterViewExecute(
           this DiagnosticListener diagnosticListener,
           ViewComponentContext context,
           IView view)
        {
            // Inlinable fast-path check if Diagnositcs is enabled
            if (diagnosticListener.IsEnabled())
            {
                ViewComponentAfterViewExecuteImpl(diagnosticListener, context, view);
            }
        }

        private static void ViewComponentAfterViewExecuteImpl(DiagnosticListener diagnosticListener, ViewComponentContext context, IView view)
        {
            if (diagnosticListener.IsEnabled("Microsoft.AspNetCore.Mvc.ViewComponentAfterViewExecute"))
            {
                diagnosticListener.Write(
                    "Microsoft.AspNetCore.Mvc.ViewComponentAfterViewExecute",
                    new
                    {
                        actionDescriptor = context.ViewContext.ActionDescriptor,
                        viewComponentContext = context,
                        view = view
                    });
            }
        }

    }
}
