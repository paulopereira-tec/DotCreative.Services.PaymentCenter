namespace DotCreative.Services.PaymentCenter.Core.Entities;

public class TransactionsItem
{
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }

    public TransactionsItem(string description, int quantity, decimal amount)
    {
        Description = description;
        Quantity = quantity;
        Amount = amount;
    }

    public TransactionsItem(string description, decimal amount)
    {
        Description = description;
        Quantity = 1;
        Amount = amount;
    }

}
