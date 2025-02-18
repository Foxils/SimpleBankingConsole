namespace bankaccount
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class BankAccount
    {
        private List<Transaction> _transactions;
        private string _hashedPin;
        private double _balance;
        private const double MaxValue = 200000000;

        public BankAccount(string initialPin)
        {
            if (!File.Exists("pinstore.txt"))
            {
                _hashedPin = FilesStore.HashPin(initialPin);  // Hash the initial PIN
                FilesStore.SavePinToFile(_hashedPin); // If it does not exist
            }
            else
            {
                _hashedPin = FilesStore.LoadPinFromFile();
            }

            _balance = 0;
            _transactions = new List<Transaction>();
        }

        public string GetPin()
        {
            return "PIN changed.";
        }

        public bool ValidatePin(string pinInput)
        {
            string hashedInput = FilesStore.HashPin(pinInput);
            return hashedInput == _hashedPin;
        }

        public void ChangePin(string newPin)
        {
            string hashedNewPin = FilesStore.HashPin(newPin);

            if (hashedNewPin == _hashedPin)
            {
                Console.WriteLine("The new PIN cannot be the same as the old one.");
                return;
            }

            if (newPin.Length == 4 && newPin.All(char.IsDigit))
            {
                _hashedPin = hashedNewPin;
                FilesStore.SavePinToFile(_hashedPin);
                Console.WriteLine("Successfully changed your PIN.");
            }
            else
            {
                Console.WriteLine("Invalid PIN. It must be 4 digits.");
            }
        }

        public void Deposit(double amount)
        {
            if (_balance + amount > MaxValue)
            {
                Console.WriteLine("Deposit exceeds the maximum balance.");
            }
            else if (amount <= 0 || amount > 500000000)
            {
                Console.WriteLine("Deposit must be positive and no more than £5000.");
            }
            else
            {
                _balance += amount;
                Console.WriteLine($"You have successfully deposited £{amount:N0}. Your total is £{_balance:N0}");
                _transactions.Add(new Transaction("Deposit", amount, _balance));
            }
        }

        public void Withdraw(double amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Negative numbers are not allowed!");
            }
            else if (amount <= _balance)
            {
                _balance -= amount;
                Console.WriteLine($"You have successfully withdrawn £{amount:N0}. Your total balance is £{_balance:N0}");
                _transactions.Add(new Transaction("Withdrawal", amount, _balance));
            }
            else
            {
                Console.WriteLine("Insufficient funds for this transaction.");
            }
        }

        public void DisplayBalance()
        {
            Console.WriteLine($"Your current balance is: £{_balance:N0}");
        }

        public void DisplayTransactionHistory()
        {
            Console.WriteLine("\nTransaction History:");
            foreach (var transaction in _transactions)
            {
                Console.WriteLine($"{transaction.Date}: {transaction.Type} of £{transaction.Amount:N0} | Balance: £{transaction.BalanceAfterTransaction:N0}");
            }
        }

        public class Transaction
        {
            public DateTime Date { get; }  // Get date, type, amount of the transaction.
            public string? Type { get; }
            public double Amount { get; }
            public double BalanceAfterTransaction { get; }

            public Transaction(string type, double amount, double balanceAfterTransaction)
            {
                Date = DateTime.Now;
                Type = type;
                Amount = amount;
                BalanceAfterTransaction = balanceAfterTransaction;
            }
        }
    }
}
