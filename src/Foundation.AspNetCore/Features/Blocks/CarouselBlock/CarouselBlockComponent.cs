using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.Media;
using Foundation.AspNetCore.Features.Media.Models;
using Foundation.AspNetCore.Features.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Blocks.CarouselBlock
{
    public class CarouselBlockComponent : BlockComponent<CarouselBlock>
    {
        private readonly IContentLoader _contentLoader;

        public CarouselBlockComponent(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public override IViewComponentResult Invoke(CarouselBlock currentBlock)
        {
            var model = new CarouselBlockViewModel(currentBlock);

            if (currentBlock.CarouselItems != null)
            {
                foreach (var contentAreaItem in currentBlock.CarouselItems.FilteredItems)
                {
                    var carouselItem = _contentLoader.Get<IContentData>(contentAreaItem.ContentLink);

                    if (carouselItem is ImageMediaData)
                    {
                        var carouselImage = new CarouselImage()
                        {
                            Heading = ((ImageMediaData)carouselItem).Title,
                            Description = ((ImageMediaData)carouselItem).Description,
                            Image = ((ImageMediaData)carouselItem).ContentLink
                        };

                        model.Items.Add(new CarouselItem() { CarouselImage = carouselImage });
                    }
                    else if (carouselItem is HeroBlock.HeroBlock)
                    {
                        model.Items.Add(new CarouselItem() { HeroBlock = new BlockViewModel<HeroBlock.HeroBlock>((HeroBlock.HeroBlock)carouselItem) });
                    }
                }
            }

            var view = View(model);
            view.ViewName = "~/Features/Blocks/CarouselBlock/CarouselBlock.cshtml";
            return view;
        }
    }
}
