using System.Text.Json.Serialization;

namespace DotCreative.Services.PaymentCenter.Paghiper.Dto.Request;

internal class TransactionDto
{
    [JsonPropertyName("apiKey")] public string ApiKey { get; set; }

    [JsonPropertyName("order_id")] public string OrderId { get; set; }

    [JsonPropertyName("payer_name")] public string PayerName { get; set; }

    [JsonPropertyName("payer_email")] public string PayerEmail { get; set; }

    [JsonPropertyName("payer_cpf_cnpj")] public string PayerCpfCnpj { get; set; }

    [JsonPropertyName("notification_url")] public string NotificationUrl { get; set; }

    [JsonPropertyName("discount_cents")] public int DiscountCents { get; set; } = 0;

    [JsonPropertyName("days_due_date")] public int DaysDueDate { get; set; }

    [JsonPropertyName("items")] public ICollection<ItemDto> Items { get; set; }

}
