namespace DotCreative.Services.PaymentCenter.Core.Entities;

/// <summary>
/// Esta classe pode ser usada tanto para boleto, como para PIX.
/// </summary>
public class TransactionResult
{
    /// <summary>
    /// Linha digitável do boleto.
    /// </summary>
    public string LineCode { get; set; }

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

    public TransactionResult(string lineCode, string url, string pdf, string image)
    {
        LineCode = lineCode;
        Url = url;
        Pdf = pdf;
        Image = image;
    }
}
