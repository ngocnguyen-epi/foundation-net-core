using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Foundation.AspNetCore.Features.Blocks.RssReaderBlock
{
    public class RssReaderBlockComponent : BlockComponent<RssReaderBlock>
    {
        public override IViewComponentResult Invoke(RssReaderBlock currentBlock)
        {
            var viewModel = new RssReaderBlockViewModel
            {
                RssList = new List<RssReaderBlockViewModel.RssItem>(),
                CurrentBlock = currentBlock
            };

            try
            {
                if ((currentBlock.RssUrl != null) && (!currentBlock.RssUrl.IsEmpty()))
                {
                    var rssDocument = XDocument.Load(Convert.ToString(currentBlock.RssUrl));

                    var posts = from item in rssDocument.Descendants("item").Take(currentBlock.MaxCount)
                                select new RssReaderBlockViewModel.RssItem
                                {
                                    Title = item.Element("title").Value,
                                    Url = item.Element("link").Value,
                                    PublishDate = item.Element("pubDate").Value,
                                };

                    viewModel.RssList = posts.ToList();
                    viewModel.HasHeadingText = HasHeadingText(currentBlock);
                    viewModel.Heading = currentBlock.Heading;
                    viewModel.DescriptiveText = currentBlock.MainBody;
                }
            }
            catch (Exception)
            {
                viewModel.HasHeadingText = true;
                viewModel.Heading = "Invalid RSS Feed URL.";
            }
            var view = View(viewModel);
            view.ViewName = "~/Features/Blocks/RssReaderBlock/RssReaderBlock.cshtml";
            return view;
        }

        private bool HasHeadingText(RssReaderBlock currentBlock) => ((!string.IsNullOrEmpty(currentBlock.Heading)) || ((currentBlock.MainBody != null) && (!currentBlock.MainBody.IsEmpty)));
    }
}
