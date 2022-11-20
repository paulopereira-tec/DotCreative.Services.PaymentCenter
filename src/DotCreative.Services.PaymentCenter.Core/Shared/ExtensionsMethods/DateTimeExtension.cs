namespace DotCreative.Services.PaymentCenter.Core.Shared.ExtensionsMethods;

public static class DateTimeExtension
{
  /// <summary>
  /// Estrai o total de dias a partir da diferença a data informada e a atual.
  /// </summary>
  public static int GetTotalDays(this DateTime dateOne, DateTime dateCompare)
    => (int)(dateOne - dateCompare).TotalDays;
}
