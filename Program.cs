// See https://aka.ms/new-console-template for more information

namespace bankaccount
{
    public class Program
    {
        static void Main(string[] args)
        {
            string initialPin = "1234"; // Default
            BankAccount account = new BankAccount(initialPin);  // New instance
            BankAccountManager manager = new BankAccountManager(account);  

            bool bankLocked = true;

            while (bankLocked)
            {
                Console.Write("Please enter your PIN now: ");
                Console.Title = "Foxy TSB";
                string? pininput = Console.ReadLine();

                if (string.IsNullOrEmpty(pininput))
                {
                    Console.WriteLine("\nInput cannot be empty.");
                }
                else if (account.ValidatePin(pininput))
                {
                    bankLocked = false;
                    Console.Clear();
                    Console.Write("Welcome to Foxy Bank!\n\nWhat would you like to do today?");
                }
                else
                {
                    Console.WriteLine("Invalid PIN, please try again.");
                }
            }

            while (!bankLocked)
            {
                Console.ResetColor();
                Console.Write("\nType help for a user guide or enter your request: ");
                string? commandInput = Console.ReadLine()?.ToLower();

                switch (commandInput)
                {
                    case "clear":
                        Console.Clear();
                        break;
                    case "help":
                        manager.ShowMenu();
                        break;
                    case "balance":
                        account.DisplayBalance();
                        break;
                    case "deposit":
                        manager.HandleDeposit();
                        break;
                    case "withdraw":
                        manager.HandleWithdraw();
                        break;
                    case "exit":
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break;
                    case "pin":
                        manager.HandlePinChange();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid command. Type 'help' for a list of commands.");
                        break;
                }
            }
        }
    }
}
