﻿using DotCreative.Services.PaymentCenter.Core.Enums;

namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

public class Document
{
  public EDocumentType Type { get; set; }
  public string Subscription { get; set; }

  public Document(EDocumentType type, string subscription)
  {
    Type = type;
    Subscription = ReplaceSpecialCharacters(subscription);
  }

  public Document(string subscription)
  {
    Subscription = ReplaceSpecialCharacters(subscription);
    EDocumentType type = Subscription.Length == 11 ? EDocumentType.CPF : EDocumentType.CNPJ;
  }

  private string ReplaceSpecialCharacters(string subscription)
  {
    return subscription
      .Replace(".", "")
      .Replace("-", "")
      .Replace("/", "");
  }
}
