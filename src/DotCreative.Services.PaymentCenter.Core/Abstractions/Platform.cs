using DotCreative.Services.PaymentCenter.Core.Entities;
using DotCreative.Services.PaymentCenter.Core.Shared.Enums;
using RestSharp;

namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

public abstract partial class Platform
{
  protected EPlatforms _plaformType { get; set; }
  protected PlatformData _platformData { get; set; }
  protected Transaction _transaction { get; set; }

  public Platform(PlatformData platformData, EPlatforms platformType)
  {
    _platformData = platformData;
    _plaformType = platformType;
  }

  /// <summary>
  /// Configura a URL base que deverá ser usada nas requisições.
  /// </summary>
  public virtual Platform Configure(string urlBase, string endpoint)
  {
    _client = new RestClient(urlBase);

    _request = new RestRequest(endpoint);
    _request.AddHeader("accept", "application/json");
    _request.AddHeader("content-type", "application/json");

    return this;
  }

  #region Componentes de RestSharp
  protected RestClient _client { get; set; }
  protected RestRequest _request { get; set; }
  protected RestResponse response { get; set; }
  #endregion

}
