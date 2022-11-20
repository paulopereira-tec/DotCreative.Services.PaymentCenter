using DotCreative.Services.PaymentCenter.Core.Shared.Enums;

namespace DotCreative.Services.PaymentCenter.Core.Entities;

/// <summary>
/// Modelo de transação. O tipo genérico TWith pode ser boleto, cartão de crédito ou pix.
/// </summary>
public class Transaction
{
  public string TransactionId { get; set; }
  public ETransactionType TransactionType { get; set; }
  public string OrderId { get; set; }
  public Person Payer { get; set; }
  public DateTime DueDate { get; set; }
  public DateTime PaidDate { get; set; }
  public ETransactionStage Stage { get; set; }
  public ICollection<TransactionsItem> Discount { get; set; }
  public ICollection<TransactionsItem> Items { get; set; }
  public decimal Amount { get; set; }

  public TransactionResult Result { get; set; }

  public Transaction()
  {

  }

  public Transaction(ETransactionType transactionType, string orderId, Person payer, DateTime dueDate, ICollection<TransactionsItem> items, ICollection<TransactionsItem> discount)
  {
    TransactionType = transactionType;
    OrderId = orderId;
    Payer = payer;
    DueDate = dueDate;
    Discount = discount;
    Items = items;
    Stage = ETransactionStage.None;

    Amount = SumTransactionAmount();
  }

  /// <summary>
  /// Calcula o valor total da transação.
  ///   REGRA: soma o total de itens e subtrai pelo total de descontos aplicados.
  ///   IMPORTANTE: multa, juros e mora não são calculados na cobrança. Isso é feito no momento do pagamento se ultrapassada a data do vencimento.
  ///               No entanto, caso deseje acrescentar algum desses valores, adicione-os nos itens da transação.
  /// </summary>
  public decimal SumTransactionAmount()
    => GetTotals(Items) - GetTotals(Discount);

  /// <summary>
  /// Calcula o valor total dos descontos aplicados.
  /// </summary>
  public decimal TotalDiscounts()
    => GetTotals(Discount);

  /// <summary>
  /// Calcula o valor total de itens transacionais. Sejam eles descontos ou ítens.
  /// </summary>
  public decimal GetTotals(ICollection<TransactionsItem> content)
    => content.Sum(x => x.Amount);

  public void SetResutData(TransactionResult result)
  {
    Result = result;
  }
}
