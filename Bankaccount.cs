namespace bankaccount
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public class BankAccount
    {
        private List<Transaction> _transactions;
        private string _hashedPin;
        private double _balance;
        private const double MaxValue = 200000000;
        private const string TransactionsFilePath = "transactions.json";

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
            LoadTransactions();
        }

        public void LoadTransactions() // Load saved transactions

        {
            try
            {
                if (File.Exists(TransactionsFilePath))
                {
                    string json = File.ReadAllText(TransactionsFilePath);
                    _transactions = JsonConvert.DeserializeObject<List<Transaction>>(json) ?? new List<Transaction>();
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Something went wrong loading transactions file: {ex.Message}");
            }
        }

        public void SaveTransactionsToFile() // Save transactions to a Json file
        {
            try
            {


                string json = JsonConvert.SerializeObject(_transactions, Formatting.Indented);
                File.WriteAllText(TransactionsFilePath, json);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong saving the transaction file: {ex.Message}");
            }
        }


        public string GetPin() // Get the users PIN
        {
            return "PIN changed.";
        }

        public bool ValidatePin(string pinInput)
        {
            string hashedInput = FilesStore.HashPin(pinInput);
            return hashedInput == _hashedPin;
        }

        public void ChangePin(string newPin) // Change the users PIN via hash
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

        public void Deposit(double amount) // Deposit funds and check it does not exceed
        {
            if (_balance + amount > MaxValue)
            {
                Console.WriteLine("Deposit exceeds the maximum balance.");
            }
            else if (amount is <= 0 or > 500000000)
            {
                Console.WriteLine("Deposit must be positive and no more than £5000.");
            }
            else
            {
                _balance += amount;
                Console.WriteLine($"You have successfully deposited £{amount:N0}. Your total is £{_balance:N0}");
                _transactions.Add(new Transaction("Deposit", amount, _balance));
                SaveTransactionsToFile();
            }
        }

        public void Withdraw(double amount) // Checks if the user attempts to go into the negative and withdraws.
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
                SaveTransactionsToFile();
            }
            else
            {
                Console.WriteLine("Insufficient funds for this transaction.");
            }
        }

        public void DisplayBalance() // Displays current balance from the saved Json file and checks for IO errors.
        {
            Console.WriteLine($"Your current balance is: £{_balance:N0}");
        }

        public void DisplayTransactionHistory()
        {
            Console.WriteLine("\nTransaction History:");

            try
            {
                foreach (Transaction transaction in _transactions)
                {
                    string formattedAmount = transaction.Amount.ToString("N2", new CultureInfo("en-GB"));
                    string formattedBalance = transaction.BalanceAfterTransaction.ToString("N2", new CultureInfo("en-GB"));

                    Console.WriteLine($"{transaction.Date}: {transaction.Type} of £{transaction.Amount:N0} | Balance: £{transaction.BalanceAfterTransaction:N0}");
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Something went wrong showing transaction history: {ex.Message}");
            }


        }


    }
}
