using System;

public class InvalidAccountException : BankingException
{
    public string AccountId { get; set; }

    public InvalidAccountException() { }
    public InvalidAccountException(string message) : base(message) { }
    public InvalidAccountException(string message, Exception inner) : base(message, inner) { }
}