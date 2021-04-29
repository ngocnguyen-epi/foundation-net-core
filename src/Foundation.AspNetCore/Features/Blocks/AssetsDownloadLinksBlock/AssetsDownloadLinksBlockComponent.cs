﻿using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Framework.Blobs;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Foundation.Commerce.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Foundation.Features.Blocks.AssetsDownloadLinksBlock.Component
{
    [TemplateDescriptor(Default = true)]
    public class AssetsDownloadLinksBlockComponent : BlockComponent<AssetsDownloadLinksBlock>
    {
        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;

        public AssetsDownloadLinksBlockComponent(IContentLoader contentLoader, UrlResolver urlResolver)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
        }
        public override IViewComponentResult Invoke(AssetsDownloadLinksBlock currentBlock)
        {
            var model = new AssetsDownloadLinksBlockViewModel(currentBlock);
            var rootContent = _contentLoader.Get<IContent>(currentBlock.RootContent);
            if (rootContent != null)
            {
                var assets = new List<MediaData>();
                if (rootContent is ContentFolder)
                {
                    assets = _contentLoader.GetChildren<MediaData>(rootContent.ContentLink).OrderByDescending(x => x.StartPublish).ToList();
                }

                if (rootContent is IAssetContainer assetContainer)
                {
                    assets = assetContainer.GetAssetsMediaData(_contentLoader, currentBlock.GroupName)
                        .OrderByDescending(x => x.StartPublish).ToList();
                }

                if (currentBlock.Count > 0)
                {
                    assets = assets.Take(currentBlock.Count).ToList();
                }

                model.Assets = assets;
            }

            return View("~/Features/Blocks/AssetsDownloadLinksBlock/AssetsDownloadLinksBlock.cshtml", model);
        }
    }
}
