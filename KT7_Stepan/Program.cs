using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("-Попытка 1: Нехватка средств-");
        try
        {
            var myAccount = new BankAccount("123456", 1000m);
            Console.WriteLine($"Счет {myAccount.AccountId} создан. Баланс: {myAccount.Balance} руб.");

            myAccount.Withdraw(1500m);
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"Ошибка снятия: {ex.Message}");
            Console.WriteLine($"Свойства: Запрошено {ex.RequestedAmount}, Доступно {ex.AvailableBalance}");
        }
        catch (InvalidAccountException ex)
        {
            Console.WriteLine($"Ошибка счета: {ex.Message}");
        }
        catch (BankingException ex)
        {
            Console.WriteLine($"Общая банковская ошибка: {ex.Message}");
        }

        Console.WriteLine("\n-Попытка 2: Неверный номер счета-");
        try
        {
            var badAccount = new BankAccount("12345", 500m);
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"Ошибка снятия: {ex.Message}");
        }
        catch (InvalidAccountException ex)
        {
            Console.WriteLine($"Ошибка счета: {ex.Message}");
            Console.WriteLine($"Свойство AccountId: {ex.AccountId}");
        }
        catch (BankingException ex)
        {
            Console.WriteLine($"Общая банковская ошибка: {ex.Message}");
        }
    }
}