using ECommerceApi.Adapters;
using ECommerceApi.Models;
using messages = EcommerceShared.InventoryMessages;
namespace ECommerceApi.Domain;

public class InventoryAdjustments
{

    private readonly InventoryHttpAdapter _httpAdapter;

    public InventoryAdjustments(InventoryHttpAdapter httpAdapter)
    {
        _httpAdapter = httpAdapter;
    }

    public async Task<bool> AreItemsAvailable(List<Item> items)
    {
        var requestItems = items.Select(i => new messages.Item { Id = i.Id, Price = i.Price, Qty = i.Qty }).ToList();
        var request = new messages.InventoryCheckRequest
        {
            Items = requestItems
        };

        var response = await _httpAdapter.CheckInventoryForItemsAsync(request);

        return response.Ok;

    }

    public async Task<bool> IsItemAvailable(Item item)
    {
        // check the inventory, check the price, etc.

        await Task.Delay(500);
        return true;
    }
}
