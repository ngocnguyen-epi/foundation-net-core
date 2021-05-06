using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.NamedCarts.Wishlist.Models
{
    [ContentType(DisplayName = "Wish List Page",
        GUID = "c80ee97b-3151-4602-a447-678534e83a0b",
        Description = "Page for customers to manage their wish list.",
        GroupName = GroupNames.Commerce,
        AvailableInEditMode = false)]
    [ImageUrl("/icons/cms/pages/cms-icon-page-08.png")]
    public class WishListPage : FoundationPageData
    {
    }
}