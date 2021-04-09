using EPiServer.Framework.Web.Resources;
using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Blocks.HealthBot
{
    public class HealthBotBlockComponent : BlockComponent<HealthBotBlock>
    {
        private readonly IRequiredClientResourceList _requiredClientResourceList;

        public HealthBotBlockComponent(IRequiredClientResourceList requiredClientResourceList)
        {
            _requiredClientResourceList = requiredClientResourceList;
        }

        public override IViewComponentResult Invoke(HealthBotBlock currentBlock)
        {
            _requiredClientResourceList.Require(HealthBotClientResourceProvider.BotJs).AtHeader();
            var model = new BlockViewModel<HealthBotBlock>(currentBlock);
            var view = View(model);
            view.ViewName = "/Features/Blocks/HealthBot/HealthChatBotBlock.cshtml";
            return view;
        }
    }

    [ClientResourceProvider]
    public class HealthBotClientResourceProvider : IClientResourceProvider
    {
        public static string BotJs = "healthbot.webchat";

        public IEnumerable<ClientResource> GetClientResources()
        {
            return new[]
            {
                new ClientResource
                {
                    Name = BotJs,
                    ResourceType = ClientResourceType.Html,
                    InlineContent = @"<script crossorigin=""anonymous"" src=""https://cdn.botframework.com/botframework-webchat/latest/webchat.js""></script>"
                }
            };
        }
    }
}
