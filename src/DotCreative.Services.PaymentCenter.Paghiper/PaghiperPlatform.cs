namespace DotCreative.Services.PaymentCenter.Paghiper;

public partial class PaghiperPlatform : Platform
{
  public override void Configure(string urlBase, string endpoint)
  {
    _client = new RestClient(urlBase);

    _request = new RestRequest(endpoint);
    _request.AddHeader("accept", "application/json");
    _request.AddHeader("content-type", "application/json");
  }

  public override async Task<Transaction> Create(Transaction transaction)
  {
    if (transaction.TransactionType == ETransactionType.Billet)
    {
      Configure("https://api.paghiper.com/", "transaction/create");
      var dataForRequest = PrepareDataForBillet(transaction);
      _request.AddJsonBody(dataForRequest);
    }
    else
    {
      Configure("https://pix.paghiper.com", "invoice/create");
      var dataForRequest = PrepareDataForPix(transaction);
      _request.AddJsonBody(dataForRequest);
    }
    
    _response = await _client.PostAsync(_request);

    if (_response.StatusCode == HttpStatusCode.Created)
    {
      transaction = PrepareRequestForTransaction(_response.Content, transaction);
      return transaction;
    }

    return null;
  }

  public override Task<Transaction> GetTransactionInfo(string transactionid)
  {
    throw new NotImplementedException();
  }

  public override Task<Dictionary<int, string>> ListAvailableBanks()
  {
    Configure
  }

  public override Task<ICollection<Transaction>> ListTransactions(DateTime dateStart, DateTime dateEnd)
  {
    throw new NotImplementedException();
  }

  public override Task<bool> RequestTransfer(decimal amount)
  {
    throw new NotImplementedException();
  }
}
