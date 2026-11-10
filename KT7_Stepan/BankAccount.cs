using System;
using System.Linq;

public class BankAccount
{
    public string AccountId { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string accountId, decimal initialBalance)
    {
        if (string.IsNullOrEmpty(accountId) || accountId.Length != 6 || !accountId.All(char.IsDigit))
        {
            throw new InvalidAccountException($"Неверный номер счета: '{accountId}'. Ожидается ровно 6 цифр.")
            {
                AccountId = accountId
            };
        }

        AccountId = accountId;
        Balance = initialBalance;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InsufficientFundsException(amount, Balance);
        }

        Balance -= amount;
        Console.WriteLine($"Снято {amount} руб. Новый баланс: {Balance} руб.");
    }
}