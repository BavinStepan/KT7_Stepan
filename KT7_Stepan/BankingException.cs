using System;

public class BankingException : Exception
{
    public BankingException() { }
    public BankingException(string message) : base(message) { }
    public BankingException(string message, Exception inner) : base(message, inner) { }
}