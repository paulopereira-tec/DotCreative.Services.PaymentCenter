using System.Text.Json.Serialization;

namespace DotCreative.Services.PaymentCenter.Paghiper.Dto.Result;

internal class ResponsePixDto
{
  [JsonPropertyName("pix_code.qrcode_base64")] public string QRCodeBase64 { get; set; }
  [JsonPropertyName("pix_code.qrcode_image_url")] public string QRCodeUrl { get; set; }
  [JsonPropertyName("pix_code.emv")] public string EMV { get; set; }
  [JsonPropertyName("pix_code.pix_url")] public string PixUrl { get; set; }
  [JsonPropertyName("pix_code.bacen_url")] public string BacenUrl { get; set; }
}
