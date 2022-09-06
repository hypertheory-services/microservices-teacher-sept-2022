using ECommerceApi.Adapters;
using ECommerceApi.Controllers;
using ECommerceApi.Models;

namespace ECommerceApi.Domain;

public class OrderProcessor
{
    private readonly MongoDbOrdersAdapter _adapter;
   private readonly InventoryPublisherAdapter _inventoryPublisherAdapter;

    public OrderProcessor(MongoDbOrdersAdapter adapter, InventoryPublisherAdapter inventoryPublisherAdapter)
    {
        _adapter = adapter;
        _inventoryPublisherAdapter = inventoryPublisherAdapter;
    }

    public async Task<OrderResponse> ProcessOrder(OrderRequest order)
    {

        //var ok = await _inventory.AreItemsAvailable(order.Items);
        //if (!ok)
        //{
        //    // throw or whatever here.

        //}
        var response = new OrderResponse
        {
            OrderId = Guid.NewGuid().ToString(),
            Customer = order.Customer,
            ExpectedShipDate = null,
            OrderTotal = order.Items.Sum(o => o.Price * o.Qty),
            Status = OrderStatus.Processing
        };

        var orderToSave = new OrderEntity
        {
            Id = response.OrderId,
            Customer = order.Customer,
            ExpectedShipDate = response.ExpectedShipDate,
            Items = order.Items,
            OrderTotal = response.OrderTotal,
            Status = response.Status
        };
        await _adapter.Orders.InsertOneAsync(orderToSave);
        await _inventoryPublisherAdapter.PublishOrderForInventory(response, orderToSave.Items);
        return response;
    }
}
