using System.Text.Json.Serialization;

namespace DotCreative.Services.PaymentCenter.Paghiper.Dto.Result;

public class ResponseDto
{
  [JsonPropertyName("result")] public string Result { get; set; }
  [JsonPropertyName("response_message")] public string ResponseMessage { get; set; }
  [JsonPropertyName("transaction_id")] public string TransactionId { get; set; }
  [JsonPropertyName("created_date")] public string CreatedDate { get; set; }
  [JsonPropertyName("value_cents")] public string ValueCents { get; set; }
  [JsonPropertyName("status")] public string Status { get; set; }
  [JsonPropertyName("order_id")] public string OrderId { get; set; }
  [JsonPropertyName("due_date")] public string DueDate { get; set; }
  [JsonPropertyName("http_code")] public int HttpCode { get; set; }
}