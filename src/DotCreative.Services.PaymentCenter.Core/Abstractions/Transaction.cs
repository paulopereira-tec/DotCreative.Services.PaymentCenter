using DotCreative.Services.PaymentCenter.Core.Enums;
using System.Collections.Generic;

namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

/// <summary>
/// Modelo de transação. O tipo genérico TWith pode ser boleto, cartão de crédito ou pix.
/// </summary>
public class Transaction
{
  public PlatformData PlatformData { get; set; }
  public ETransactionType TransactionType { get; set; }
  public string OrderId { get; set; }
  public Person Payer { get; set; }
  public DateTime DueDate { get; set; }
  public ETransactionStage Stage { get; set; }
  public ICollection<TransactionsItem> Discount { get; set; }
  public ICollection<TransactionsItem> Items { get; set; }
  public decimal Amount { get; set; }

  public TransactionSlipData SlipData {get;set;}

  public Transaction(PlatformData platformData, ETransactionType transactionType, string orderId, Person payer, DateTime dueDate, ICollection<TransactionsItem> items, ICollection<TransactionsItem> discount)
  {
    TransactionType = transactionType;
    OrderId = orderId;
    Payer = payer;
    DueDate = dueDate;
    Discount = discount;
    Items = items;
    Stage = ETransactionStage.Pending;
    PlatformData = platformData;

    Amount = SumTransactionAmount(items, discount);
  }

  public void SetSlipData(TransactionSlipData slipData)
  {
    SlipData = slipData;
  }

  /// <summary>
  /// Calcula o valor total da transação.
  ///   REGRA: soma o total de itens e subtrai pelo total de descontos aplicados.
  ///   IMPORTANTE: multa, juros e mora não são calculados na cobrança. Isso é feito no momento do pagamento se ultrapassada a data do vencimento.
  ///               No entanto, caso deseje acrescentar algum desses valores, adicione-os nos itens da transação.
  /// </summary>
  private decimal SumTransactionAmount(ICollection<TransactionsItem> items, ICollection<TransactionsItem> discount)
  {
    decimal totalItems = items.Sum(x => x.Amount);
    decimal totalDiscounts = items.Sum(x => x.Amount);

    return totalItems - totalDiscounts;
  }
}
