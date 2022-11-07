using System.Text.Json.Serialization;

namespace DotCreative.Services.PaymentCenter.Paghiper.Dto.Request;

internal class ItemDto
{
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("item_id")]
    public int ItemId { get; set; }

    [JsonPropertyName("price_cents")]
    public int PriceCents { get; set; }
}
