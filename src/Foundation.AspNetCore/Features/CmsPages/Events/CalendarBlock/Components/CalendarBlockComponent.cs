using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.CmsPages.Events.CalendarBlock.Component
{
    [TemplateDescriptor(Default = true)]
    public class CalendarBlockComponent : BlockComponent<Models.CalendarBlock>
    {
        private readonly IContentLoader _contentLoader;

        public CalendarBlockComponent(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public override IViewComponentResult Invoke(Models.CalendarBlock currentBlock)
        {
            var model = new ViewModels.CalendarBlockViewModel(currentBlock);

            return View("~/Features/CmsPages/Events/CalendarBlock/Views/index.cshtml", model);
        }
    }
}