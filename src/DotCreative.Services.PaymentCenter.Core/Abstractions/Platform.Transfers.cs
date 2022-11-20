using DotCreative.Services.PaymentCenter.Core.Entities;

namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

public abstract partial class Platform
{
  public abstract Task<bool> RequestTransfer(decimal amount, Bank bank);
  public abstract Task<ICollection<Bank>> ListAvailableBanksForTransfers();
}
