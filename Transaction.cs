namespace bankaccount
{
    using Newtonsoft.Json;
    using System;

    public class Transaction
    {
        [JsonConstructor]
        public Transaction(string type, double amount, double balanceAfterTransaction)
        {
            Date = DateTime.Now;
            Type = type;
            Amount = amount;
            BalanceAfterTransaction = balanceAfterTransaction;
        }

        public Transaction(string type, double amount, double balanceAfterTransaction, DateTime date)
        {
            Date = date;
            Type = type;
            Amount = amount;
            BalanceAfterTransaction = balanceAfterTransaction;
        }

        public DateTime Date { get; }
        public string Type { get; }
        public double Amount { get; }
        public double BalanceAfterTransaction { get; }
    }
}
