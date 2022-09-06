using EcommerceShared.InventoryMessages;

namespace ECommerceApi.Adapters;

public class InventoryHttpAdapter
{
    private readonly HttpClient _httpClient;

    public InventoryHttpAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<InventoryCheckResponse> CheckInventoryForItemsAsync(InventoryCheckRequest request)
    {

        var response = await _httpClient.PostAsJsonAsync("/inventory-checks", request);

        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadFromJsonAsync<InventoryCheckResponse>();

        return responseData!;

    }
}
