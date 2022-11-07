using System.Text.Json.Serialization;

namespace DotCreative.Services.PaymentCenter.Paghiper.Dto.Request;

/// <summary>
/// DTO para transferência dos dados entre a aplicação e a Paghiper. Veja documentação para compreender os dados.
/// </summary>
/// <see cref="https://dev.paghiper.com/reference/especifica%C3%A7%C3%B5es-dos-campos-que-devem-ser-enviados-na-requisi%C3%A7%C3%A3o-1"/>
internal class BilletTransactionDto : TransactionDto
{
    [JsonPropertyName("payer_street")] public string PayerStreet { get; set; }

    [JsonPropertyName("payer_number")] public int PayerNumber { get; set; }

    [JsonPropertyName("payer_complement")] public string PayerComplement { get; set; }

    [JsonPropertyName("payer_district")] public string PayerDistrict { get; set; }

    [JsonPropertyName("payer_city")] public string PayerCity { get; set; }

    [JsonPropertyName("payer_state")] public string PayerState { get; set; }

    [JsonPropertyName("payer_zip_code")] public string PayerZipcode { get; set; }

    [JsonPropertyName("type_bank_slip")] public string TypeBankSlip { get; set; } = "boletoA4";
}
