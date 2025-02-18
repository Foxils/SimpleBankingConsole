namespace bankaccount
{
    public class BankAccountManager
    {
        private BankAccount _account;

        public BankAccountManager(BankAccount account)
        {
            _account = account;
        }

        public void ShowMenu()
        {
            Console.WriteLine("\nCommands: Balance, Help, Deposit, Withdraw, Pin, History, Exit.");
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
            Console.Write($"Please enter your new PIN: ");
            string? newPin = Console.ReadLine();
            try
            {
                if (string.IsNullOrEmpty(newPin))
                {
                    Console.WriteLine("Invalid PIN. Please enter a valid PIN.");
                }
                else
                {
                    _account.ChangePin(newPin);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong whilst changing the PIN: {ex.Message}");
            }

        }

        public void HandleTransactionHistory()
        {
            _account.DisplayTransactionHistory();
        }

    }
}
