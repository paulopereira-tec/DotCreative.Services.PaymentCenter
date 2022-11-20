namespace DotCreative.Services.PaymentCenter.Core.Entities;

public class Telephone
{
  public int DDD { get; set; }
  public int Number { get; set; }

  public override string ToString()
    => DDD.ToString() + Number.ToString();

  public Telephone(int dDD, int number)
  {
    DDD = dDD;
    Number = number;
  }

  public Telephone(string numberWithDdd)
  {
    if (!string.IsNullOrEmpty(numberWithDdd))
    {
      DDD = Convert.ToInt32(numberWithDdd.Substring(0, 2));
      Number = Convert.ToInt32(numberWithDdd.Substring(2, numberWithDdd.Length - 2));
    }
  }

  public Telephone()
  {

  }
}
