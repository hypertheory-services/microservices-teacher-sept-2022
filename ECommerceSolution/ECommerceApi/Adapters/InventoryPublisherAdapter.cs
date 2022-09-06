using Dapr.Client;
using ECommerceApi.Models;
using EcommerceShared.InventoryMessages;

namespace ECommerceApi.Adapters;

public class InventoryPublisherAdapter
{
    private readonly DaprClient _dapr;

    public InventoryPublisherAdapter(DaprClient dapr)
    {
        _dapr = dapr;
    }

    public async Task PublishOrderForInventory(OrderResponse request, List<Models.Item> items)
    {
        var message = new InventoryCheckRequest
        {
            OrderId = request.OrderId,
            Items = items.Select(i => new EcommerceShared.InventoryMessages.Item { Id = i.Id, Price = i.Price, Qty = i.Qty }).ToList()
        };


        await _dapr.PublishEventAsync("ecommercepubsub", "inventory-request", message);
    }
}

