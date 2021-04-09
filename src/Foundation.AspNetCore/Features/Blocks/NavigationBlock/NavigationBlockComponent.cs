using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Blocks.NavigationBlock
{
    public class NavigationBlockComponent : BlockComponent<NavigationBlock>
    {
        private readonly IContentLoader _contentLoader;
        private readonly IPageRouteHelper _pageRouteHelper;

        public NavigationBlockComponent(IContentLoader contentLoader, IPageRouteHelper pageRouteHelper)
        {
            _contentLoader = contentLoader;
            _pageRouteHelper = pageRouteHelper;
        }

        public override IViewComponentResult Invoke(NavigationBlock currentBlock)
        {
            var rootNavigation = currentBlock.RootPage as ContentReference;
            if (ContentReference.IsNullOrEmpty(currentBlock.RootPage))
            {
                rootNavigation = _pageRouteHelper.ContentLink;
            }

            var childPages = _contentLoader.GetChildren<PageData>(rootNavigation);
            var model = new NavigationBlockViewModel(currentBlock);
            if (childPages != null && childPages.Count() > 0)
            {
                var linkCollection = new List<NavigationItem>();
                foreach (var page in childPages)
                {
                    if (page.VisibleInMenu)
                    {
                        linkCollection.Add(new NavigationItem(page, Url));
                    }
                }

                model.Items.AddRange(linkCollection.Where(x => !string.IsNullOrEmpty(x.Url)));
            }

            if (string.IsNullOrEmpty(currentBlock.Heading))
            {
                model.Heading = _pageRouteHelper.Page.Name;
            }

            var view = View(model);
            view.ViewName = "~/Features/Blocks/NavigationBlock/NavigationBlock.cshtml";
            return view;
        }
    }
}
