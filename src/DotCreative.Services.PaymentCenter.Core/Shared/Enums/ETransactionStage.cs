namespace DotCreative.Services.PaymentCenter.Core.Shared.Enums;

public enum ETransactionStage
{
    None,
    Pending,
    Reserved,
    Canceled,
    Complete,
    Paid,
    Processing,
    Refunded
}
