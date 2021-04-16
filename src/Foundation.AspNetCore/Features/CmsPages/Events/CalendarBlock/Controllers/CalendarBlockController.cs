using EPiServer;
using EPiServer.Core;
using Foundation.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Features.CmsPages.Events.CalendarBlock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarBlockController : ControllerBase
    {
        private readonly IContentLoader _contentLoader;
        
        public CalendarBlockController(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        private IEnumerable<CalendarEventPage.Models.CalendarEventPage> GetEvents(int blockId)
        {
            var contentRef = new ContentReference(blockId);
            var currentBlock = _contentLoader.Get<Models.CalendarBlock>(contentRef);
            IEnumerable<CalendarEventPage.Models.CalendarEventPage> events;
            
            var root = currentBlock.EventsRoot;
            if (currentBlock.Recursive)
            {
                events = root.GetAllRecursively<CalendarEventPage.Models.CalendarEventPage>();
            }
            else
            {
                events = _contentLoader.GetChildren<CalendarEventPage.Models.CalendarEventPage>(root);
            }

            if (currentBlock.CategoryFilter != null && currentBlock.CategoryFilter.Any())
            {
                events = events.Where(x => x.Category.Intersect(currentBlock.CategoryFilter).Any());
            }

            events.Take(currentBlock.Count);

            return events;
        }

        [HttpPost]
        [Route("CalendarEvents")]
        public ContentResult CalendarEvents(CalendarBlockId calendarBlockId)
        {
            var blockId = calendarBlockId.blockId;
            var events = GetEvents(blockId);
            var result = events.Select(x => new
            {
                title = x.Name,
                start = x.EventStartDate,
                end = x.EventEndDate,
                url = x.LinkURL
            });

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(result),
                ContentType = "application/json",
            };
        }

        [HttpPost]
        [Route("UpcomingEvents")]
        public ContentResult UpcomingEvents(CalendarBlockId calendarBlockId)
        {
            var blockId = calendarBlockId.blockId;
            var events = GetEvents(blockId);
            var result = events.Where(x => x.EventStartDate >= DateTime.Now)
                .OrderBy(x => x.EventStartDate)
                .Select(x => new
                {
                    x.Name,
                    x.EventStartDate,
                    x.EventEndDate,
                    x.LinkURL
                });

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(result),
                ContentType = "application/json",
            };
        }

        public class CalendarBlockId
        {
            public int blockId { get; set; }
        }
    }
}
