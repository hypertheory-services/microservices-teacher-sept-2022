using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models;





public record OrderRequest
{
    [Required, EmailAddress]
    public string Customer { get; set; } = string.Empty;
    public List<Item> Items { get; set; } = new();
}

public record Item
{
    [Required]
    public string Id { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public int Qty { get; set; }
    public decimal Price { get; set; }
}


public record OrderResponse
{
    public string OrderId { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;
    public decimal OrderTotal { get; set; }
    public DateTime? ExpectedShipDate { get; set; }
    public OrderStatus Status { get; set; }
}
public enum OrderStatus {  Processing, Approved, Denied }