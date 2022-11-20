using DotCreative.OpenSource.PrimitiveTypesValidations;
using DotCreative.Services.PaymentCenter.Paghiper;
using System.Diagnostics;

namespace DotCreative.Services.PaymentCenter.UnityTests;

[TestClass]
public class PaghiperTest
{
  [TestMethod("Criação de boleto")]
  [DataRow(ETransactionType.Billet, DisplayName = "Criação de transação: Boleto")]
  [DataRow(ETransactionType.Pix, DisplayName = "Criação de transação: Pix")]
  public async Task MustBeSuccess_CreateTransaction(ETransactionType type)
  {
    // Arrange
    Transaction transaction = Arrange.GetTransaction(type);
    PlatformData platform = Arrange.PaghiperPlatformData();
    PaghiperPlatform paghiper = new PaghiperPlatform(platform);

    try
    {
      // Act
      transaction = await paghiper.Create(transaction);

      // Assert
      Assert.AreEqual(ETransactionStage.Pending, transaction.Stage);
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      Debug.WriteLine(ex.StackTrace);
      Assert.Fail(ex.Message);
    }
  }

  [TestMethod]
  [DataRow(ETransactionType.Billet, DisplayName = "Listagem de transações: Boleto")]
  [DataRow(ETransactionType.Pix, DisplayName = "Listagem de transações: Pix")]
  public async Task MustBeSuccess_ListTransaction(ETransactionType type)
  {
    // Arrange
    PlatformData platform = Arrange.PaghiperPlatformData();
    PaghiperPlatform paghiper = new PaghiperPlatform(platform);
    DateTime dateStart = DateTime.Now.AddDays(-60);
    DateTime dateEnd = DateTime.Now.AddDays(+30);

    try
    {
      // Act
      ICollection<Transaction> transactions = await paghiper.List(type, dateStart, dateEnd, 100, 1);

      // Assert
      Assert.IsTrue(transactions.Count.IsGreaterThan(0));
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      Debug.WriteLine(ex.StackTrace);
      Assert.Fail(ex.Message);
    }
  }

  [TestMethod]
  [DataRow("11KHB9XEPTDQ1X22", ETransactionType.Billet, DisplayName = "Informações da transação: Boleto")]
  [DataRow("11S4SUNYH8CNMA22", ETransactionType.Pix, DisplayName = "Informações da transação: Pix")]
  public async Task MustBeSuccess_GetTransaction(string transactionId, ETransactionType type)
  {
    // Arrange
    PlatformData platform = Arrange.PaghiperPlatformData();
    PaghiperPlatform paghiper = new PaghiperPlatform(platform);

    try
    {
      // Act
      Transaction transaction = await paghiper.Get(type, transactionId);

      // Assert
      if (transaction is not null)
        Assert.AreNotEqual(transaction.Stage, ETransactionStage.None);
      else
        Assert.Inconclusive("Não foi possível localizar a transação.");
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      Debug.WriteLine(ex.StackTrace);
      Assert.Fail(ex.Message);
    }
  }

  [TestMethod]
  [DataRow("112EZYE0SJNUIT22", ETransactionType.Billet, DisplayName = "Cancelamendo da transação: Boleto")]
  [DataRow("11ZSP173816G8J22", ETransactionType.Pix, DisplayName = "Cancelamendo da transação: Pix")]
  public async Task MustBeSuccess_CancelTransaction(string transactionId, ETransactionType type)
  {
    // Arrange
    PlatformData platform = Arrange.PaghiperPlatformData();
    PaghiperPlatform paghiper = new PaghiperPlatform(platform);
    
    try
    {
      // Act
      bool isCanceled = await paghiper.Cancel(type, transactionId);

      // Assert
      Assert.IsTrue(isCanceled);
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      Debug.WriteLine(ex.StackTrace);
      Assert.Fail(ex.Message);
    }
  }

  [TestMethod("Listagem de bancos disponíveis para transferência.")]
  public async Task MustBeSuccess_ListAvailableBanksForTransfers()
  {
    // Arrange
    PlatformData platform = Arrange.PaghiperPlatformData();
    PaghiperPlatform paghiper = new PaghiperPlatform(platform);

    try
    {
      // Act
      ICollection<Bank> banks = await paghiper.ListAvailableBanksForTransfers();


      // Assert
      Assert.IsTrue(banks.Count().IsGreaterThan(0));
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      Debug.WriteLine(ex.StackTrace);
      Assert.Fail(ex.Message);
    }
  }

  [TestMethod("Solicitação de saque/transferência de valor.")]
  public async Task MustBeSuccess_Transfer()
  {
    // Arrange
    PlatformData platform = Arrange.PaghiperPlatformData();
    PaghiperPlatform paghiper = new PaghiperPlatform(platform);
    ICollection<Bank> banks = await paghiper.ListAvailableBanksForTransfers();

    try
    {
      // Act
      bool isTransfered = await paghiper.Transfer(banks.First());

      // Assert
      Assert.IsTrue(isTransfered);
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      Debug.WriteLine(ex.StackTrace);
      Assert.Fail(ex.Message);
    }
  }
}
