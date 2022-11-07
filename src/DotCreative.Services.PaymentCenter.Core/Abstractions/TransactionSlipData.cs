namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

/// <summary>
/// Esta classe pode ser usada tanto para boleto, como para PIX.
/// </summary>
public class TransactionSlipData
{
  /// <summary>
  /// Linha digitável do boleto ou 'copia e cola' do PIX.
  /// </summary>
  public string DigitableLine { get; set; }

  /// <summary>
  /// URL para conteúdo HTML do boleto ou PIX, se houver.
  /// </summary>
  public string Url { get; set; }

  /// <summary>
  /// Download do PDF do boleto ou PIX, se houver.
  /// </summary>
  public string Pdf { get; set; }

  /// <summary>
  /// Imagem do código de barras do boleto, do boleto ou do PIX, se houver.
  /// </summary>
  public string Image { get; set; }

  public TransactionSlipData(string digitableLine, string url, string pdf, string image)
  {
    DigitableLine = digitableLine;
    Url = url;
    Pdf = pdf;
    Image = image;
  }
}
