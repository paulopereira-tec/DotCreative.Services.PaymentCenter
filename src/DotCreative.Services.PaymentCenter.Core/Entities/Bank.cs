using DotCreative.Services.PaymentCenter.Core.Shared.Enums;

namespace DotCreative.Services.PaymentCenter.Core.Entities;

public class Bank
{
  public string Id { get; set; }
  public string Code { get; set; }
  public string Name { get; set; }
  public EBankType Type { get; set; }

  public Bank(string id, string code, string name, EBankType type)
  {
    Id = id;
    Code = code;
    Name = name;
    Type = type;
  }
}
