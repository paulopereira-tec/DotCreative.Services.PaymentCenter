using DotCreative.Services.PaymentCenter.Core.Abstractions;
using DotCreative.Services.PaymentCenter.Core.Enums;
using DotCreative.Services.PaymentCenter.Paghiper;
using System.ComponentModel;
using System.Diagnostics;

namespace DotCreative.Services.PaymentCenter.Tests;

[TestClass, DisplayName("Paghiper, Teste na")]
public class PaghiperTransactionTest
{
  [TestMethod]
  [DataRow(ETransactionType.Billet, DisplayName = "Gera��o de Boletos")]
  [DataRow(ETransactionType.Pix, DisplayName = "Gera��o de PIX")]
  public async Task GenerateTransaction(ETransactionType transactionType)
  {
		try
		{
      Transaction transaction = Arrange.ForTransaction(transactionType);
      PaghiperPlatform platform = new PaghiperPlatform();
      transaction = await platform.Create(transaction);

      if (transaction is null)
        Assert.Fail("Falha na cria��o da transa��o.");

      Assert.IsTrue(true, "Transa��o criada com sucesso.");
    }
		catch (Exception ex)
		{
      Assert.Fail("Falha na cria��o da transa��o. " + ex.Message);
      Debug.WriteLine(ex.StackTrace);
    }
  }
}