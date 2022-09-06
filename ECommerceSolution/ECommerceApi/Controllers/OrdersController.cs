using ECommerceApi.Domain;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers;

[ApiController]
public class OrdersController : ControllerBase
{
    private readonly OrderProcessor _orderProcessor;

    public OrdersController(OrderProcessor orderProcessor)
    {
        _orderProcessor = orderProcessor;
    }

    [HttpPost("/orders")]
    public async Task<ActionResult> PlaceOrderAsync([FromBody] OrderRequest request)
    {
        var response = await _orderProcessor.ProcessOrder(request);
        return StatusCode(201, response);
    }
}
