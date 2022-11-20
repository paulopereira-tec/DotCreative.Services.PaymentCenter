using DotCreative.Services.PaymentCenter.Core.Entities;
using DotCreative.Services.PaymentCenter.Core.Shared.Enums;
using DotCreative.Services.PaymentCenter.Core.Shared.ExtensionsMethods;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace DotCreative.Services.PaymentCenter.Paghiper;

public partial class PaghiperPlatform
{
  #region Métodos abstratos subscritos
  public override async Task<Transaction> Create(Transaction transaction, string notificationUrl = "")
  {
    _transaction = transaction;

    if (transaction.TransactionType == ETransactionType.Billet)
    {
      Configure("https://api.paghiper.com", "transaction/create");
      var dataForRequest = PrepareDataForBillet(notificationUrl);
      _request.AddJsonBody(dataForRequest);
    }
    else
    {
      Configure("https://pix.paghiper.com", "invoice/create");
      var dataForRequest = PrepareDataForPix(notificationUrl);
      _request.AddJsonBody(dataForRequest);
    }

    response = await _client.PostAsync(_request);

    if (response.StatusCode == HttpStatusCode.Created)
    {
      PrepareRequestForTransaction(response.Content);

      return _transaction;
    }

    return null;
  }

  public override async Task<ICollection<Transaction>> List(ETransactionType type, DateTime dateStart, DateTime dateEnd, int pageQuantity, int numberOfPage)
  {
    if (type == ETransactionType.Billet)
    {
      Configure("https://api.paghiper.com", "transaction/list");
    }
    else
    {
      Configure("https://pix.paghiper.com", "invoice/list");
    }

    _request.AddJsonBody(new
    {
      token = _platformData.Token,
      apiKey = _platformData.Key,
      initial_date = dateStart.ToString("yyyy-MM-dd"),
      final_date = dateEnd.ToString("yyyy-MM-dd"),
      limit = pageQuantity,
      page = numberOfPage
    });
    response = await _client.PostAsync(_request);

    if (response.StatusCode == HttpStatusCode.Created)
    {
      dynamic dynamicTransactions = JsonConvert.DeserializeObject(response.Content);
      dynamicTransactions = dynamicTransactions.transaction_list_request.transaction_list;

      ICollection<Transaction> transactions = new List<Transaction>();

      foreach (dynamic _transaction in dynamicTransactions)
      {
        Transaction transaction = PrepareTransactionBasedOnRequest(_transaction);
        transactions.Add(transaction);
      }

      return transactions;
    }

    return null;
  }

  public override async Task<Transaction?> Get(ETransactionType type, string transactionId)
  {
    if (type == ETransactionType.Billet)
      Configure("https://api.paghiper.com", "transaction/status/");
    else
      Configure("https://pix.paghiper.com", "invoice/status/");

    _request.AddJsonBody(new
    {
      token = _platformData.Token,
      apiKey = _platformData.Key,
      transaction_id = transactionId
    });
    response = await _client.PostAsync(_request);

    if (response.StatusCode == HttpStatusCode.Created)
    {
      dynamic dynamicTransactions = JsonConvert.DeserializeObject(response.Content);
      return PrepareTransactionBasedOnRequest(dynamicTransactions.status_request);
    }

    return null;
  }

  public override async Task<bool> Cancel(ETransactionType type, string transactionId)
  {
    if (type == ETransactionType.Billet)
      Configure("https://api.paghiper.com", "transaction/cancel/");
    else
      Configure("https://pix.paghiper.com", "invoice/cancel/");

    _request.AddJsonBody(new
    {
      token = _platformData.Token,
      apiKey = _platformData.Key,
      status = "canceled",
      transaction_id = transactionId
    });

    response = await _client.PostAsync(_request);

    return response.StatusCode == HttpStatusCode.Created ? true : false;
  }
  #endregion

  #region Métodos locais
  /// <summary>
  /// Prepara um modelo de transação a partir dos dados recebidos da requisição feita na plataforma.
  /// </summary>
  /// <param name="response">Dados recebidos da plataforma no formato original.</param>
  private void PrepareRequestForTransaction(string response)
  {
    dynamic result = JsonConvert.DeserializeObject(response);

    dynamic createRequest;
    TransactionResult _result;

    if (_transaction.TransactionType == ETransactionType.Billet)
    {
      createRequest = result.create_request.bank_slip;

      _result = new TransactionResult(
        createRequest["digitable_line"].Value,
        createRequest["url_slip"].Value,
        createRequest["url_slip_pdf"].Value,
        createRequest["bar_code_number_to_image"].Value
        );
    }
    else
    {
      createRequest = result.pix_create_request.pix_code;

      _result = new TransactionResult(
        createRequest["emv"].Value,
        createRequest["bacen_url"].Value,
        "",
        createRequest["qrcode_image_url"].Value);
    }

    if (_transaction.TransactionType == ETransactionType.Billet)
    {
      _transaction.TransactionId = string.IsNullOrEmpty(result.create_request["transaction_id"].Value) ? "" : result.create_request["transaction_id"].Value;
      _transaction.Stage = PrepareTransactionStageBasedOnResult(result.create_request["status"].Value);
    }
    else
    {
      _transaction.TransactionId = string.IsNullOrEmpty(result.pix_create_request["transaction_id"].Value) ? "" : result.pix_create_request["transaction_id"].Value;
      _transaction.Stage = PrepareTransactionStageBasedOnResult(result.pix_create_request["status"].Value);
    }

    _transaction.SetResutData(_result);
  }

  /// <summary>
  /// Prepara os itens com base no retorno da lista de transações consultada.
  /// </summary>
  private ICollection<TransactionsItem> PrepareItemsBasedOnTransactionsList(dynamic _items)
  {
    ICollection<TransactionsItem> items = new List<TransactionsItem>();

    foreach (dynamic _item in _items)
    {
      int amountInt = Convert.ToInt32(_item["price_cents"].Value);
      decimal amount = amountInt.ConvertCentsToDecimal();
      TransactionsItem item = new(_item["description"].Value, amount);
      items.Add(item);
    }

    return items;
  }

  /// <summary>
  /// Prepara o modelo de uma transação com base nos dados recebidos pela requisição de informação na plataforma de pagamento.
  /// </summary>
  private Transaction PrepareTransactionBasedOnRequest(dynamic _transaction)
  {
    Address address = new Address();

    Telephone telephone = _transaction.ContainsKey("payer_phone")
      ? new Telephone(_transaction["payer_phone"].Value)
      : new Telephone();

    Document document = _transaction.ContainsKey("payer_cpf_cnpj")
      ? new Document(_transaction["payer_cpf_cnpj"].Value)
      : new Document();

    Person payer = _transaction.ContainsKey("payer_name") ?
      new Person(_transaction["payer_name"].Value, _transaction["payer_email"].Value, document, telephone, address) :
      new Person();

    ICollection<TransactionsItem> items = _transaction.ContainsKey("items") ?
      PrepareItemsBasedOnTransactionsList(_transaction.items) :
      new List<TransactionsItem>();

    ICollection<TransactionsItem> discounts = PrepareDiscountsBasedOnResult(_transaction);

    ETransactionType transactionType = _transaction.ContainsKey("bank_slip") ?
      ETransactionType.Billet :
      ETransactionType.Pix;

    string[] dueDateSplited = _transaction["due_date"].Value.Split('-');
    DateTime dueDate = new DateTime(Convert.ToInt32(dueDateSplited[0]), Convert.ToInt32(dueDateSplited[1]), Convert.ToInt32(dueDateSplited[2]));

    Transaction transaction = new(transactionType, _transaction["order_id"].Value, payer, dueDate, items, discounts);

    transaction.Stage = PrepareTransactionStageBasedOnResult(_transaction["status"].Value);

    TransactionResult resultData = PrepareTransactionResult(transactionType, _transaction);
    transaction.SetResutData(resultData);

    return transaction;
  }

  /// <summary>
  /// Prepara o resultado da transação com os dados de PIX ou Boleto.
  /// </summary>
  private TransactionResult PrepareTransactionResult(ETransactionType transactionType, dynamic result)
  {
    TransactionResult resultData;
    if (transactionType == ETransactionType.Billet)
    {
      resultData = new TransactionResult(
        result.bank_slip["digitable_line"].Value,
        result.bank_slip["url_slip"].Value,
        result.bank_slip["url_slip_pdf"].Value,
        ""
        );
    }
    else
    {
      resultData = new TransactionResult(
        result.pix_code["emv"].Value,
        result.pix_code["pix_url"].Value,
        "",
        result.pix_code["qrcode_image_url"].Value
        );
    }

    return resultData;
  }

  /// <summary>
  /// Prepara os itens no pagamento para ficar em conformidade com o que deve ser enviado para a Paghiper.
  /// </summary>
  private object PrepareItemsForPaghiper(ICollection<TransactionsItem> _items)
  {
    List<object> items = new List<object>();
    int itemN = 0;

    foreach (var item in _items)
    {
      items.Add(new
      {
        description = item.Description,
        quantity = item.Quantity,
        item_id = itemN,
        price_cents = item.Amount.ConvertToCents()
      });

      itemN++;
    }

    return items.ToArray();
  }

  /// <summary>
  /// Prepara os dados no formato que devem ser enviados para a Paghiper
  /// </summary>
  /// <param name="_transaction">Transação</param>
  /// <param name="notificationUrl">Url de retorno para notificação de qualquer atualização na transação. Não é obrigatório.</param>
  private object PrepareDataForBillet(string notificationUrl = "")
  {
    var dataForBillet = new
    {
      apiKey = _platformData.Key,
      order_id = _transaction.OrderId,
      payer_email = _transaction.Payer.Email,
      payer_name = _transaction.Payer.Name,
      payer_cpf_cnpj = _transaction.Payer.Document.Subscription,
      payer_phone = _transaction.Payer.Telephone.ToString(),
      payer_street = _transaction.Payer.Address.Street,
      payer_number = _transaction.Payer.Address.Number,
      payer_complement = _transaction.Payer.Address.Complement,
      payer_district = _transaction.Payer.Address.Neighborhood,
      payer_city = _transaction.Payer.Address.City,
      payer_state = _transaction.Payer.Address.State,
      payer_zip_code = _transaction.Payer.Address.Zipcode,
      notification_url = notificationUrl,
      discount_cents = _transaction.TotalDiscounts().ConvertToCents(),
      shipping_price_cents = 0,
      shipping_methods = "",
      fixed_description = true,
      days_due_date = _transaction.DueDate.GetTotalDays(DateTime.Now),
      type_bank_slip = "boletoA4",
      items = PrepareItemsForPaghiper(_transaction.Items)
    };

    return dataForBillet;
  }

  /// <summary>
  /// Prepara os dados no formato que devem ser enviados para a Paghiper
  /// </summary>
  private object PrepareDataForPix(string notificationUrl = "")
  {
    var dataForPix = new
    {
      apiKey = _platformData.Key,
      order_id = _transaction.OrderId,
      payer_email = _transaction.Payer.Email,
      payer_name = _transaction.Payer.Name,
      payer_cpf_cnpj = _transaction.Payer.Document.Subscription,
      payer_phone = _transaction.Payer.Telephone.ToString(),
      notification_url = notificationUrl,
      discount_cents = _transaction.TotalDiscounts().ConvertToCents(),
      shipping_price_cents = "0",
      shipping_methods = "",
      fixed_description = true,
      days_due_date = _transaction.DueDate.GetTotalDays(DateTime.Now),
      items = PrepareItemsForPaghiper(_transaction.Items)
    };

    return dataForPix;
  }

  /// <summary>
  /// Prepara o estágio com base nos dados recebidos pela requisição de informações sobre a transação.
  /// </summary>
  private ETransactionStage PrepareTransactionStageBasedOnResult(string stage)
  {
    return stage switch
    {
      "pending" => ETransactionStage.Pending,
      "reserved" => ETransactionStage.Reserved,
      "canceled" => ETransactionStage.Canceled,
      "completed" => ETransactionStage.Complete,
      "paid" => ETransactionStage.Paid,
      "processing" => ETransactionStage.Processing,
      "refunded" => ETransactionStage.Refunded,
      _ => ETransactionStage.None
    };
  }

  /// <summary>
  /// Prepara a lista de descontos baseado no resultado da consulta da transação.
  /// </summary>
  private ICollection<TransactionsItem> PrepareDiscountsBasedOnResult(dynamic _transaction)
  {
    ICollection<TransactionsItem> discounts = new List<TransactionsItem>();

    if (_transaction.ContainsKey("discount_cents"))
    {
      int discountInt = Convert.ToInt32(_transaction["discount_cents"].Value);

      if (discountInt > 0)
      {
        TransactionsItem discount = new TransactionsItem("Total de descontos", discountInt.ConvertCentsToDecimal());
        discounts.Add(discount);
      }
    }

    return discounts;
  }
  #endregion
}
