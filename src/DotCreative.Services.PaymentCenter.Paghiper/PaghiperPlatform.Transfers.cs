using DotCreative.Services.PaymentCenter.Core.Entities;
using DotCreative.Services.PaymentCenter.Core.Shared.Enums;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace DotCreative.Services.PaymentCenter.Paghiper;

public partial class PaghiperPlatform
{
  public override async Task<ICollection<Bank>> ListAvailableBanksForTransfers()
  {
    ICollection<Bank> banks = new List<Bank>();

    Configure("https://api.paghiper.com", "bank_accounts/list");

    _request.AddJsonBody(new
    {
      token = _platformData.Token,
      apiKey = _platformData.Key
    });
    response = await _client.PostAsync(_request);

    if (response.StatusCode == HttpStatusCode.Created)
    {
      dynamic result = JsonConvert.DeserializeObject(response.Content);

      foreach (dynamic bank in result.bank_account_list_request.bank_account_list)
      {
        EBankType type = PrepareBankType(bank["account_type"].Value);
        banks.Add(new Bank(bank["bank_account_id"].Value.ToString(), bank["bank_code"].Value, bank["bank_name"].Value, type));
      }
    }

    return banks;
  }

  public override async Task<bool> Transfer(Bank bank, decimal amount = 0)
  {
    Configure("https://api.paghiper.com", "bank_accounts/cash_out");

    _request.AddJsonBody(new
    {
      token = _platformData.Token,
      apiKey = _platformData.Key,
      bank_account_id = bank.Id
    });
    response = await _client.PostAsync(_request);

    return response.StatusCode == HttpStatusCode.Created? true : false;
  }

  #region Métodos privados locais
  /// <summary>
  /// Retorna o tipo de conta (corrente, poupança, etc)
  /// </summary>
  public EBankType PrepareBankType(string type)
  {
    return type switch
    {
      "Corrente" => EBankType.Current,
      "Poupança" => EBankType.Savings,
      _ => EBankType.None
    };
  }
  #endregion
}
