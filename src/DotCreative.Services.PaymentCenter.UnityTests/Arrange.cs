namespace DotCreative.Services.PaymentCenter.UnityTests;


/// <summary>
/// Arranja os dados para efetuar os testes
/// </summary>
public static class Arrange
{
  /// <summary>
  /// Configura os dados para executar as transações.
  /// </summary>
  public static Transaction GetTransaction(ETransactionType transactionType)
  {
    Address address = new("Rua A", 123, "Jardim Paulista", "Rio de Janeiro", "RJ", "21523661");
    Document document = new("27510636086");
    Telephone telephone = new(21, 973773451);
    Person payer = new("João da Silva", "joaodasilva@dominio.tld", document, telephone, address);

    ICollection<TransactionsItem> items = new List<TransactionsItem>();
    ICollection<TransactionsItem> discount = new List<TransactionsItem>();

    items.Add(new ("Teste item 1", 149.12m));
    items.Add(new ("Teste item 2", 155.05m));
    items.Add(new ("Teste item 3", 15122));

    discount.Add(new ("Teste desconto 1", 42.54m));
    discount.Add(new ("Teste desconto 2", 15.01m));

    Transaction transaction = new (
      transactionType, 
      Guid.NewGuid().ToString(),
      payer,
      DateTime.Now.AddDays(15),
      items, 
      discount);

    return transaction;
  }

  /// <summary>
  /// Configura os dados necessários para contectar-se a plataforma de pagamento
  /// </summary>
  public static PlatformData PaghiperPlatformData()
  => new PlatformData("7GPX8CPT7KZPKGGRHTTJ2MFJNQ6O1K780X689V1HF16Z", "apk_42347047-WYsOMGJNWbHZlfagwLivvuOLkmrItDUQ");
}