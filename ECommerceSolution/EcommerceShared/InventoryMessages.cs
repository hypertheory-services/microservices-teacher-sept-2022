
namespace EcommerceShared.InventoryMessages;


public record Item
{
    public string Id { get; init; } = string.Empty;
    public int Qty { get; init; } 
    public decimal Price { get; init; }
}

public record InventoryCheckRequest
{
    public string OrderId { get; set; } = string.Empty;
    public List<Item> Items { get; set; } = new();
}

public record InventoryCheckResponse
{
    public string Status { get; set; } = "";
    public bool Ok { get; set; }
}


