using DotCreative.Services.PaymentCenter.Core.Abstractions;
using DotCreative.Services.PaymentCenter.Core.Enums;

namespace DotCreative.Services.PaymentCenter.Tests;

public static class Arrange
{
  public static Transaction ForTransaction(ETransactionType transactionType)
  {
    string orderId = Guid.NewGuid().ToString();
    DateTime dueDate = DateTime.Now.AddDays(15);

    PlatformData platformData = new PlatformData(EPlatforms.PagHiper, "7GPX8CPT7KZPKGGRHTTJ2MFJNQ6O1K780X689V1HF16Z", "apk_42347047-WYsOMGJNWbHZlfagwLivvuOLkmrItDUQ");

    ICollection<TransactionsItem> items = new List<TransactionsItem>();
    items.Add(new TransactionsItem("Item 1", 149.90m));
    items.Add(new TransactionsItem("Item 2", 10.10m));

    ICollection<TransactionsItem> discounts = new List<TransactionsItem>();
    discounts.Add(new TransactionsItem("Desconto 1", 2.45m));
    discounts.Add(new TransactionsItem("Desconto 2", 1.00m));

    Address address = new Address("Av Brigadeiro Faria Lima", 12345, "Jardim Paulistano", "São Paulo", "SP", "01452002", "Torre Sul 4º Andar");
    Telephone telephone = new Telephone(11, 40638785);
    Document document = new Document(EDocumentType.CPF, "30307214001");
    Person payer = new Person("Poul Silva", "poulsilva@exemple.com", document, telephone, address);
    Transaction transaction = new Transaction(platformData, transactionType, orderId, payer, dueDate, items, discounts);

    return transaction;
  }
}
