namespace DotCreative.Services.PaymentCenter.Core.Shared.ExtensionsMethods;

public static class DecimalExtension
{
  /// <summary>
  /// Converte o valor em decimal para valor em centavos.
  /// 
  /// Exemplo: converte de 99.99 para 9999.
  /// </summary>
  /// <param name="amount"></param>
  /// <returns></returns>
  public static int ConvertToCents(this decimal amount)
  {
    string strAmount = amount
      .ToString()
      .Replace(",", "")
      .Replace(".", "");

    return Convert.ToInt32(strAmount);
  }

}
