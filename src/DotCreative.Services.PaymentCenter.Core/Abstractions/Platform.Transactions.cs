using DotCreative.Services.PaymentCenter.Core.Entities;
using DotCreative.Services.PaymentCenter.Core.Shared.Enums;

namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

public abstract partial class Platform
{
  /// <summary>
  /// Cria uma transação (pode ser boleto, cartão, pix, etc) e aguarda o retorno dos dados transacionados.
  /// </summary>
  /// <param name="transaction">Dados da transação a ser criada.</param>
  /// <param name="notificationUrl">URL de notificação de retorno que a plataforma deverá comunicar-se quando houver alguma alteração na transação.</param>
  /// <returns></returns>
  public abstract Task<Transaction> Create(Transaction transaction, string notificationUrl = "");

  /// <summary>
  /// Lista as transações já efetuadas em um período determinado
  /// </summary>
  public abstract Task<ICollection<Transaction>> List(ETransactionType type, DateTime dateStart, DateTime dateEnd, int pageQuantity, int page);

  public abstract Task<Transaction> Get(ETransactionType type, string transactionId);

  /// <summary>
  /// Cancela uma transação existente
  /// </summary>
  /// <param name="type">Tipo de transação que deseja cancelar.</param>
  /// <param name="transactionId">Código da transação na plataforma de pagamento.</param>
  /// <returns></returns>
  public abstract Task Cancel(ETransactionType type, string transactionId);
}
