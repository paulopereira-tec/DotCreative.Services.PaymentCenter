using DotCreative.Services.PaymentCenter.Core.Enums;
using RestSharp;

namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

public abstract class Platform
{
  /// <summary>
  /// Configura a URL base que deverá ser usada nas requisições.
  /// </summary>
  public abstract void Configure(string urlBase, string endpoint);

  #region Transações
  /// <summary>
  /// Cria uma transação (pode ser boleto, cartão, pix, etc) e aguarda o retorno dos dados transacionados.
  /// </summary>
  public abstract Task<Transaction> Create(Transaction transaction);

  /// <summary>
  /// Lista as transações já efetuadas em um período determinado
  /// </summary>
  public abstract Task<ICollection<Transaction>> ListTransactions(DateTime dateStart, DateTime dateEnd);

  /// <summary>
  /// Recupera as informações de uma transação a partir do seu ID.
  /// </summary>
  public abstract Task<Transaction> GetTransactionInfo(string transactionid);

  /// <summary>
  /// Prepara um objeto de transação para criá-la.
  /// </summary>
  public abstract Transaction PrepareRequestForTransaction(dynamic response, Transaction transaction);
  #endregion

  #region Transferências
  public abstract Task<bool> RequestTransfer(decimal amount);
  public abstract Task<Dictionary<int, string>> ListAvailableBanks();
  #endregion

  #region Componentes de RestSharp
  protected RestClient _client { get; set; }
  protected RestRequest _request { get; set; }
  protected RestResponse _response { get; set; }
  #endregion

}
