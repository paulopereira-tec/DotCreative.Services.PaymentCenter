namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

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
}
