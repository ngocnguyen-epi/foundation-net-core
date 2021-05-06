using EPiServer.Commerce.Order;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Order.Interfaces
{
    public interface IConfirmationService
    {

        IPurchaseOrder GetOrder(int orderNumber);

        IPurchaseOrder CreateFakePurchaseOrder();
    }
}