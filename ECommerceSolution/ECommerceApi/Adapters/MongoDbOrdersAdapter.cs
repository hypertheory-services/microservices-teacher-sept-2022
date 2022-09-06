using ECommerceApi.Domain;
using MongoDB.Driver;

namespace ECommerceApi.Adapters;

public class MongoDbOrdersAdapter
{
    public IMongoCollection<OrderEntity> Orders { get; private set; }

    public MongoDbOrdersAdapter(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("orders");
        Orders = database.GetCollection<OrderEntity>("orders");
    }
}
