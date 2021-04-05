using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Blocks
{
    [TemplateDescriptor(Inherited = true)]
    public class DefaultBlockController : BlockComponent<FoundationBlockData>
    {
        public override IViewComponentResult Invoke(FoundationBlockData currentBlock)
        {
            var model = CreateModel(currentBlock);
            var blockName = currentBlock.GetOriginalType().Name;
            return View(string.Format("~/Features/Blocks/{0}/{1}.cshtml", blockName, blockName), model);
        }

        private static IBlockViewModel<BlockData> CreateModel(BlockData currentBlock)
        {
            var type = typeof(BlockViewModel<>).MakeGenericType(currentBlock.GetOriginalType());
            return Activator.CreateInstance(type, currentBlock) as IBlockViewModel<BlockData>;
        }
    }
}
