using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Framework.Localization;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Features.Search.Models;
using Mediachase.Search;
using Mediachase.Search.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Search.ViewModels
{
    public class FilterOptionViewModelBinder : IModelBinder
    {
        private readonly IContentLoader _contentLoader;
        private readonly LocalizationService _localizationService;
        private readonly IContentLanguageAccessor _languageResolver;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FilterOptionViewModelBinder(IContentLoader contentLoader,
            LocalizationService localizationService,
            IContentLanguageAccessor languageResolver,
            IHttpContextAccessor httpContextAccessor)
        {
            _contentLoader = contentLoader;
            _localizationService = localizationService;
            _languageResolver = languageResolver;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            bindingContext.ModelName = "FilterOption";
            var model = new FilterOptionViewModel();
            var contentLink = _httpContextAccessor.HttpContext.GetContentLink();
            IContent content = null;
            if (!ContentReference.IsNullOrEmpty(contentLink))
            {
                content = _contentLoader.Get<IContent>(contentLink);
            }

            var query = _httpContextAccessor.HttpContext.Request.Query["search"];
            var sort = _httpContextAccessor.HttpContext.Request.Query["sort"];
            var facets = _httpContextAccessor.HttpContext.Request.Query["facets"];
            var section = _httpContextAccessor.HttpContext.Request.Query["t"];
            var page = _httpContextAccessor.HttpContext.Request.Query["page"];
            var pageSize = _httpContextAccessor.HttpContext.Request.Query["pageSize"];
            var confidence = _httpContextAccessor.HttpContext.Request.Query["confidence"];
            var viewMode = _httpContextAccessor.HttpContext.Request.Query["ViewSwitcher"];
            var sortDirection = _httpContextAccessor.HttpContext.Request.Query["sortDirection"];

            EnsurePage(model, page);
            EnsurePageSize(model, pageSize);
            EnsureViewMode(model, viewMode);
            EnsureQ(model, query);
            EnsureSort(model, sort);
            EnsureSortDirection(model, sortDirection);
            EnsureSection(model, section);
            EnsureFacets(model, facets, content);
            model.Confidence = decimal.TryParse(confidence, out var confidencePercentage) ? confidencePercentage : 0;
            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }

        protected virtual void EnsurePage(FilterOptionViewModel model, string page)
        {
            if (model.Page < 1)
            {
                if (!string.IsNullOrEmpty(page))
                {
                    model.Page = int.Parse(page);
                }
                else
                {
                    model.Page = 1;
                }
            }
        }

        protected virtual void EnsurePageSize(FilterOptionViewModel model, string pageSize)
        {
            if (!string.IsNullOrEmpty(pageSize))
            {
                model.PageSize = int.Parse(pageSize);
            }
            else
            {
                model.PageSize = 10;
            }
        }

        protected virtual void EnsureViewMode(FilterOptionViewModel model, string viewMode)
        {
            if (!string.IsNullOrEmpty(viewMode))
            {
                model.ViewSwitcher = viewMode;
            }
            else
            {
                model.ViewSwitcher = "";
            }
        }

        protected virtual void EnsureQ(FilterOptionViewModel model, string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                model.Q = q;
            }
        }

        protected virtual void EnsureSection(FilterOptionViewModel model, string section)
        {
            if (!string.IsNullOrEmpty(section))
            {
                model.SectionFilter = section;
            }
        }

        protected virtual void EnsureSort(FilterOptionViewModel model, string sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                model.Sort = sort;
            }
        }
        protected virtual void EnsureSortDirection(FilterOptionViewModel model, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortDirection))
            {
                model.SortDirection = sortDirection;
            }
        }

        protected virtual void EnsureFacets(FilterOptionViewModel model, string facets, IContent content)
        {
            if (model.FacetGroups == null)
            {
                model.FacetGroups = CreateFacetGroups(facets, content);
            }
        }

        private List<FacetGroupOption> CreateFacetGroups(string facets, IContent content)
        {
            var facetGroups = new List<FacetGroupOption>();
            if (string.IsNullOrEmpty(facets))
            {
                return facetGroups;
            }

            var nodeContent = content as NodeContent;
            if (nodeContent == null)
            {
                return facetGroups;
            }
            var filter = GetSearchFilterForNode(nodeContent);
            var selectedFilters = facets.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            var nodeFacetValues = filter.Values.SimpleValue.Where(x => selectedFilters.Any(y => y.ToLower().Equals(x.key.ToLower())));
            if (!nodeFacetValues.Any())
            {
                return facetGroups;
            }
            var nodeFacet = CreateFacetGroup(filter);

            foreach (var nodeFacetValue in nodeFacetValues)
            {
                nodeFacet.Facets.Add(CreateFacetOption(nodeFacetValue.value));
            }

            facetGroups.Add(nodeFacet);
            return facetGroups;
        }

        private FacetOption CreateFacetOption(string facet)
        {
            return new FacetOption { Name = facet, Key = facet, Selected = true };
        }

        private FacetGroupOption CreateFacetGroup(SearchFilter searchFilter)
        {
            return new FacetGroupOption
            {
                GroupFieldName = searchFilter.field,
                Facets = new List<FacetOption>()
            };
        }

        private static FacetOption CreateFacetOption(string name, string key)
        {
            return new FacetOption { Name = name, Key = key, Selected = true };
        }

        public SearchFilter GetSearchFilterForNode(NodeContent nodeContent)
        {
            var configFilter = new SearchFilter
            {
                field = BaseCatalogIndexBuilder.FieldConstants.Node,
                Descriptions = new Descriptions
                {
                    defaultLocale = _languageResolver.Language.Name
                },
                Values = new SearchFilterValues()
            };

            var desc = new Description
            {
                locale = "en",
                Value = _localizationService.GetString("/Facet/Category")
            };
            configFilter.Descriptions.Description = new[] { desc };

            var nodes = _contentLoader.GetChildren<NodeContent>(nodeContent.ContentLink).ToList();
            var nodeValues = new SimpleValue[nodes.Count];
            var index = 0;
            var preferredCultureName = _languageResolver.Language.Name;
            foreach (var node in nodes)
            {
                var val = new SimpleValue
                {
                    key = node.Code,
                    value = node.Code,
                    Descriptions = new Descriptions
                    {
                        defaultLocale = preferredCultureName
                    }
                };
                var desc2 = new Description
                {
                    locale = preferredCultureName,
                    Value = node.DisplayName
                };
                val.Descriptions.Description = new[] { desc2 };

                nodeValues[index] = val;
                index++;
            }
            configFilter.Values.SimpleValue = nodeValues;
            return configFilter;
        }
    }
}
