using ECommerceApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceApi.Domain;

public class OrderEntity
{
    [BsonElement("_id")]


    public string Id { get; set; } = String.Empty;
    public string Customer { get; set; } = string.Empty;
    public List<Item> Items { get; set; } = new();

    public decimal OrderTotal { get; set; }
    public DateTime? ExpectedShipDate { get; set; }
    public OrderStatus Status { get; set; }
}
