using DotCreative.Services.PaymentCenter.Core.Entities;

namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

public abstract partial class Platform
{
  /// <summary>
  /// Solicita a transferencia de valor para o banco informado.
  /// </summary>
  /// <param name="bank">Banco que se deseja solicitar a transferência.</param>
  /// <param name="amount">Quantia desejada. Não é necessário para alguns bancos.</param>
  public virtual Task<bool> Transfer(Bank bank, decimal amount = 0)
  {
    throw new NotImplementedException("Opção não implementada para esta operadora.");
  }

  /// <summary>
  /// Recupera a lista de contas cadastradas na plataforma de pagamento que estão disponíveis para transferências.
  /// </summary>
  public virtual Task<ICollection<Bank>> ListAvailableBanksForTransfers()
  {
    throw new NotImplementedException("Opção não implementada para esta operadora.");
  }
}
