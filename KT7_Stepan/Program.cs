using System;
using System.Linq;

public class BankingException : Exception
{
    public BankingException() { }
    public BankingException(string message) : base(message) { }
    public BankingException(string message, Exception inner) : base(message, inner) { }
}

public class InsufficientFundsException : BankingException
{
    public decimal RequestedAmount { get; }
    public decimal AvailableBalance { get; }

    public InsufficientFundsException() { }
    public InsufficientFundsException(string message) : base(message) { }
    public InsufficientFundsException(string message, Exception inner) : base(message, inner) { }

    public InsufficientFundsException(decimal requestedAmount, decimal availableBalance)
        : base($"Недостаточно средств. Запрошено: {requestedAmount}, Доступно: {availableBalance}")
    {
        RequestedAmount = requestedAmount;
        AvailableBalance = availableBalance;
    }
}

public class InvalidAccountException : BankingException
{
    public string AccountId { get; set; }

    public InvalidAccountException() { }
    public InvalidAccountException(string message) : base(message) { }
    public InvalidAccountException(string message, Exception inner) : base(message, inner) { }
}

public class BankAccount
{
    public string AccountId { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string accountId, decimal initialBalance)
    {
        if (string.IsNullOrEmpty(accountId) || accountId.Length != 6 || !accountId.All(char.IsDigit))
        {
            throw new InvalidAccountException($"Неверный номер счета: '{accountId}'. Номер должен состоять ровно из 6 цифр.")
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
        Console.WriteLine($"Снято {amount}. Новый баланс: {Balance}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("-Попытка 1: Создание корректного счета-");
        try
        {
            BankAccount acc1 = new BankAccount("123456", 1000m);
            Console.WriteLine($"Счет {acc1.AccountId} создан. Баланс: {acc1.Balance}");

            Console.WriteLine("Пытаемся снять 1500...");
            acc1.Withdraw(1500m);
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"Ошибка снятия: {ex.Message}");
            Console.WriteLine($"Запрошено: {ex.RequestedAmount}, Доступно: {ex.AvailableBalance}");
        }
        catch (InvalidAccountException ex)
        {
            Console.WriteLine($"Ошибка счета: {ex.Message}");
        }

        Console.WriteLine("\n-Попытка 2: Создание счета с неверным номером-");
        try
        {
            BankAccount acc2 = new BankAccount("12345", 500m);
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"Ошибка снятия: {ex.Message}");
        }
        catch (InvalidAccountException ex)
        {
            Console.WriteLine($"Ошибка счета: {ex.Message}");
            Console.WriteLine($"Переданный ID: {ex.AccountId}");
        }
        catch (BankingException ex)
        {
            Console.WriteLine($"Общая банковская ошибка: {ex.Message}");
        }
    }
}