using System;

public class InsufficientFundsException : BankingException
{
    public decimal RequestedAmount { get; }
    public decimal AvailableBalance { get; }

    public InsufficientFundsException() { }
    public InsufficientFundsException(string message) : base(message) { }
    public InsufficientFundsException(string message, Exception inner) : base(message, inner) { }

    public InsufficientFundsException(decimal requested, decimal available)
        : base($"Недостаточно средств. Запрошено: {requested}, Доступно: {available}")
    {
        RequestedAmount = requested;
        AvailableBalance = available;
    }
}