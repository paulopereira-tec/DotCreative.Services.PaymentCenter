using System.Text.Json.Serialization;

namespace DotCreative.Services.PaymentCenter.Paghiper.Dto.Result;

internal class RootPixDto
{
  [JsonPropertyName("pix_create_request")]
  public RootCreateRequestPixDto RootCreateRequestPixDto { get; set; }
}

internal class RootBilletDto
{
  [JsonPropertyName("create_request")]
  public RootCreateRequestBilletDto RootCreateRequestBilletDto { get; set; }
}

internal class RootCreateRequestPixDto: ResponseDto
{
  [JsonPropertyName("pix_code")]
  public ResponsePixDto ResponsePix { get; set; }
}

internal class RootCreateRequestBilletDto: ResponseDto
{
  [JsonPropertyName("bank_slip")]
  public ResponsePixDto ResponsePix { get; set; }
}
