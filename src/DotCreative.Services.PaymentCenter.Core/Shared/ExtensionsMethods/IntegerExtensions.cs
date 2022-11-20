namespace DotCreative.Services.PaymentCenter.Core.Shared.ExtensionsMethods;

public static class IntegerExtensions
{
  public static decimal ConvertCentsToDecimal(this int amount)
  {
    string strAmount = amount.ToString();
    strAmount = strAmount.Substring(0, strAmount.Length - 2) + "." + strAmount.Substring(strAmount.Length - 2, 2);
    return Convert.ToDecimal(strAmount);
  }
}
