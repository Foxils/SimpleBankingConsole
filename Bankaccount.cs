using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankaccount
{
    public class BankAccount
    {
        private double _balance;
        private string _pin;
        private const double MaxValue = 90000;

        public BankAccount(string initialPin)
        {
            _pin = initialPin;
            _balance = 0;
        }

        public string GetPin()
        {
            return _pin;
        }

        public bool ValidatePin(string pinInput)
        {
            return pinInput == _pin;
        }

        public void Deposit(double amount)
        {
            if (_balance + amount > MaxValue)
            {
                Console.WriteLine("Deposit exceeds the maximum balance.");
            }
            else if (amount <= 0 || amount > 5000)
            {
                Console.WriteLine("Deposit must be positive and no more than £5000.");
            }
            else
            {
                _balance += amount;
                Console.WriteLine($"You have successfully deposited £{amount:F2}. Your total is £{_balance:F2}");
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
                Console.WriteLine($"You have successfully withdrawn £{amount:F2}. Your total balance is £{_balance:F2}");
            }
            else
            {
                Console.WriteLine("Insufficient funds for this transaction.");
            }
        }

        public void ChangePin(string newPin)
        {
            if (newPin == _pin)
            {
                Console.WriteLine("The new PIN cannot be the same as the old one.");
            }
            else if (newPin.Length == 4 && newPin.All(char.IsDigit))
            {
                _pin = newPin;
                Console.WriteLine("Successfully changed your PIN.");
            }
            else
            {
                Console.WriteLine("Invalid PIN. It must be 4 digits.");
            }
        }

        public void DisplayBalance()
        {
            Console.WriteLine($"Your current balance is: £{_balance:F2}");
        }
    }
    public class BankAccountManager
    {
        private BankAccount _account;

        public BankAccountManager(BankAccount account)
        {
            _account = account;
        }

        public void ShowMenu()
        {
            Console.WriteLine("\nCommands: Balance, Help, Deposit, Withdraw, Pin, Exit");
        }

        public void HandleDeposit()
        {
            Console.Write("Please enter how much to deposit: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                _account.Deposit(amount);
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a valid number.");
            }
        }

        public void HandleWithdraw()
        {
            Console.Write("Please enter the amount you would like to withdraw: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                _account.Withdraw(amount);
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a valid number.");
            }
        }

        public void HandlePinChange()
        {
            Console.Write($"Your current PIN is {_account.GetPin()}, please enter your new PIN: ");
            string? newPin = Console.ReadLine();

            if (string.IsNullOrEmpty(newPin))
            {
                Console.WriteLine("Invalid PIN. Please enter a valid PIN.");
            }
            else
            {
                _account.ChangePin(newPin);
            }
        }
    }

}
