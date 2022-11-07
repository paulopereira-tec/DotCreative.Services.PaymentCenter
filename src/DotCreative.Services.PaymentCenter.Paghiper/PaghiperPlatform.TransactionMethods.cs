using DotCreative.Services.PaymentCenter.Paghiper.Dto.Request;
using DotCreative.Services.PaymentCenter.Paghiper.Dto.Result;
using System.Text.Json;

namespace DotCreative.Services.PaymentCenter.Paghiper;

public partial class PaghiperPlatform: Platform
{
  public override Transaction PrepareRequestForTransaction(dynamic response, Transaction transaction)
  {
    if (transaction.TransactionType == ETransactionType.Billet)
    {
      RootBilletDto root = JsonSerializer.Deserialize<RootBilletDto>(response);
    }

    if (transaction.TransactionType == ETransactionType.Pix)
    {
      RootPixDto root = JsonSerializer.Deserialize<RootPixDto>(response);
    }

    return transaction;
  }

  /// <summary>
  /// Prepara os dados para a criação do PIX.
  /// </summary>
  private object PrepareDataForPix(Transaction transaction)
  {
    PixTransactionDto requestData = new PixTransactionDto
    {
      ApiKey = transaction.PlatformData.Key,
      OrderId = transaction.OrderId,
      PayerEmail = transaction.Payer.Email,
      PayerName = transaction.Payer.Name,
      PayerCpfCnpj = transaction.Payer.Document.Subscription,
      NotificationUrl = "https://mysite.com/notify/paghiper/",
      DiscountCents = GetTotalInCents(transaction.Discount),
      DaysDueDate = GetTotalDays(transaction.DueDate),
      Items = PrepareItems(transaction.Items)
    };

    return requestData;
  }

  /// <summary>
  /// Prepara os dados para a criação do boleto.
  /// </summary>
  private object PrepareDataForBillet(Transaction transaction)
  {
    BilletTransactionDto requestData = new BilletTransactionDto
    {
      ApiKey = transaction.PlatformData.Key,
      OrderId = transaction.OrderId,
      
      PayerName = transaction.Payer.Name,
      PayerEmail = transaction.Payer.Email,
      PayerCpfCnpj = transaction.Payer.Document.Subscription,
      
      PayerStreet = transaction.Payer.Address.Street,
      PayerNumber = transaction.Payer.Address.Number,
      PayerComplement = transaction.Payer.Address.Complement,
      PayerDistrict = transaction.Payer.Address.Neighborhood,
      PayerCity = transaction.Payer.Address.City,
      PayerState = transaction.Payer.Address.State,
      PayerZipcode = transaction.Payer.Address.Zipcode,
      
      NotificationUrl = "https://mysite.com/notify/paghiper/",
      DiscountCents = GetTotalInCents(transaction.Discount),
      DaysDueDate = GetTotalDays(transaction.DueDate),
      Items = PrepareItems(transaction.Items)
    };

    return requestData;
  }

  /// <summary>
  /// Estrai o total de dias a partir da diferença entre duas dadas.
  /// </summary>
  private int GetTotalDays(DateTime date)
    => (int)(date - DateTime.Now).TotalDays;

  /// <summary>
  /// Recupera o valor total dos descontos já convertido para o valor em centavos.
  /// </summary>
  private int GetTotalInCents(ICollection<TransactionsItem> items)
  {
    decimal total = items.Sum(x => x.Amount);
    return ConvertDecimalToCents(total);
  }

  /// <summary>
  /// Converte o valor monetário (decimal) para o total em centavos.
  /// </summary>
  private int ConvertDecimalToCents(decimal amount)
  {
    string strAmount = amount
      .ToString()
      .Replace(",", "")
      .Replace(".", "");

    return Convert.ToInt32(strAmount);
  }

  /// <summary>
  /// Prepara um array genérico a partir dos itens da transação.
  /// </summary>
  private ICollection<ItemDto> PrepareItems(ICollection<TransactionsItem> itemsInTransaction)
  {
    ICollection<ItemDto> dto = new List<ItemDto>();

    foreach(TransactionsItem item in itemsInTransaction)
    {
      dto.Add(new ItemDto
      {
        ItemId = dto.Count() + 1,
        Description = item.Description,
        PriceCents = ConvertDecimalToCents(item.Amount),
        Quantity = item.Quantity
      });
    }

    return dto;
  }
}
