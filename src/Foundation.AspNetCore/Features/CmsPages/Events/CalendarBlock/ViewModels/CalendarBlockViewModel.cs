using EPiServer.Core;

namespace Foundation.AspNetCore.Features.CmsPages.Events.CalendarBlock.ViewModels
{
    public class CalendarBlockViewModel
    {
        public CalendarBlockViewModel(Models.CalendarBlock block)
        {
            ViewMode = block.ViewMode;
            BlockId = ((IContent)block).ContentLink.ID;
            CurrentBlock = block;
        }

        public string ViewMode { get; set; }
        public int BlockId { get; set; }
        public Models.CalendarBlock CurrentBlock { get; set; }
    }
}
