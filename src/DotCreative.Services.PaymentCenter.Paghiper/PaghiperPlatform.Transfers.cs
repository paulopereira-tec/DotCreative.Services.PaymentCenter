using DotCreative.Services.PaymentCenter.Core.Entities;

namespace DotCreative.Services.PaymentCenter.Paghiper;

public partial class PaghiperPlatform
{
  public override Task<ICollection<Bank>> ListAvailableBanksForTransfers()
  {
    throw new NotImplementedException();
  }

  public override Task<bool> RequestTransfer(decimal amount, Bank bank)
  {
    throw new NotImplementedException();
  }
}
