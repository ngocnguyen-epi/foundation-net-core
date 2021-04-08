﻿using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Features.Media.Models;
using Foundation.AspNetCore.Features.Media.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.Media.Controllers
{
    [TemplateDescriptor(TemplateTypeCategory = TemplateTypeCategories.MvcPartialComponent, Inherited = true)]
    public class MediaController : PartialContentComponent<MediaData>
    {
        private readonly UrlResolver _urlResolver;
        private readonly IContextModeResolver _contextModeResolver;

        public MediaController(UrlResolver urlResolver,
            IContextModeResolver contextModeResolver)
        {
            _urlResolver = urlResolver;
            _contextModeResolver = contextModeResolver;
        }

        public override IViewComponentResult Invoke(MediaData currentContent)
        {
            switch (currentContent)
            {
                case VideoFile videoFile:
                    var videoViewModel = new VideoFileViewModel
                    {
                        DisplayControls = videoFile.DisplayControls,
                        Autoplay = videoFile.Autoplay,
                        Copyright = videoFile.Copyright
                    };

                    if (_contextModeResolver.CurrentMode == ContextMode.Edit)
                    {
                        videoViewModel.VideoLink = _urlResolver.GetUrl(videoFile.ContentLink, null, new VirtualPathArguments { ContextMode = ContextMode.Default });
                        videoViewModel.PreviewImage = ContentReference.IsNullOrEmpty(videoFile.PreviewImage) ? string.Empty :
                           _urlResolver.GetUrl(videoFile.PreviewImage, null, new VirtualPathArguments { ContextMode = ContextMode.Default });
                    }
                    else
                    {
                        videoViewModel.VideoLink = _urlResolver.GetUrl(videoFile.ContentLink);
                        videoViewModel.PreviewImage = ContentReference.IsNullOrEmpty(videoFile.PreviewImage) ? string.Empty : _urlResolver.GetUrl(videoFile.PreviewImage);
                    }
                    return View("~/Features/Media/Views/VideoFile.cshtml", videoViewModel);
                case ImageMediaData image:
                    var imageViewModel = new ImageMediaDataViewModel
                    {
                        Name = image.Name,
                        Description = image.Description,
                        ImageAlignment = image.ImageAlignment,
                        PaddingStyles = image.PaddingStyles
                    };

                    if (_contextModeResolver.CurrentMode == ContextMode.Edit)
                    {
                        imageViewModel.ImageLink = _urlResolver.GetUrl(image.ContentLink, null, new VirtualPathArguments { ContextMode = ContextMode.Default });
                        imageViewModel.LinkToContent = ContentReference.IsNullOrEmpty(image.Link) ? string.Empty :
                           _urlResolver.GetUrl(image.Link, null, new VirtualPathArguments { ContextMode = ContextMode.Default });
                    }
                    else
                    {
                        imageViewModel.ImageLink = _urlResolver.GetUrl(image.ContentLink);
                        imageViewModel.LinkToContent = ContentReference.IsNullOrEmpty(image.Link) ? string.Empty : _urlResolver.GetUrl(image.Link);
                    }

                    return View("~/Features/Media/Views/ImageMedia.cshtml", imageViewModel);
                //case FoundationPdfFile pdfFile:
                //    var pdfViewModel = new FoundationPdfFileViewModel
                //    {
                //        Height = pdfFile.Height
                //    };

                //    if (_contextModeResolver.CurrentMode == ContextMode.Edit)
                //    {
                //        pdfViewModel.PdfLink = _urlResolver.GetUrl(pdfFile.ContentLink, null, new VirtualPathArguments { ContextMode = ContextMode.Default });
                //    }
                //    else
                //    {
                //        pdfViewModel.PdfLink = _urlResolver.GetUrl(pdfFile.ContentLink);
                //    }

                //    return PartialView("~/Features/Media/PdfFile.cshtml", pdfViewModel);
                default:
                    return View("~/Features/Media/Views/Index.cshtml", currentContent.GetType().BaseType.Name);
            }
        }
    }
}