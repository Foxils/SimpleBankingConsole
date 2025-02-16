// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace bankaccount
{
    class Program
    {
        
        const double MaxValue = 90000; // Maximum allowed in single acc
        static double bankBalance = 0; // Dynamic balance
        static string pin = "1234"; // Dynamic PIN

        static void Main(string[] args)
        {
            bool bankLocked = true;

            while (bankLocked) // While the bank is locked we need to pass security
            {
                Console.Write("Please enter your PIN now: ");
                Console.Title = "Foxy TSB";
                string? pininput = Console.ReadLine();
                if (string.IsNullOrEmpty(pininput))
                {

                    Console.WriteLine("\nInput cannot be empty.");
                }



                else if (!string.IsNullOrEmpty(pininput) && Pinsecurity(pininput))
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

          
            {

            }

            while (!bankLocked) // Unlocked welcome screen
            {
                Console.ResetColor();
                Console.Write("\nType help for a user guide or enter your request: ");
                string? commandInput = Console.ReadLine()?.ToLower();


                switch (commandInput) // Commands
                {
                    case "help":
                        Showhelp();
                        break;
                    case "balance":
                        Displayfunds();
                        break;
                    case "deposit":
                        Depositfunds();
                        break;
                    case "withdraw":
                        Withdrawfunds();
                        break;
                    case "exit":
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break;
                    case "pin":
                        Changepin();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid command. Type 'help' for a list of commands.");
                        break;
                }
                Console.ReadLine();

            }


            static void Withdrawfunds()
            {

                double removefunds;

                Console.Write("Please enter the ammount you would like to withdraw:");
                try
                {  // Risky code ahead
                    bool success = double.TryParse(Console.ReadLine(), out removefunds);

                    if (removefunds < 0)
                    {
                        Console.WriteLine("Negative numbers are not allowed!");
                        return;
                    }


                    if (success && removefunds <= bankBalance)
                    {

                        bankBalance = bankBalance - removefunds;
                        Console.WriteLine($"You have successfully withdrawn £{removefunds:F2} your total balance is £{bankBalance:F2}");


                    }

                    else
                    {

                        Console.WriteLine("Invalid input or you do not have enough funds for this transaction. ");
                    }
                }
                catch (Exception ex) // If it's got this far something has gone very wrong.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");

                }


            }
            static void Changepin()
            {
                Console.Write($"Your current PIN is {pin} please enter your new PIN number: ");
                string? newpin = Console.ReadLine();

                if (newpin == pin)
                {
                    Console.WriteLine("The new PIN cannot be the same as the old one.");
                    return;
                }


                if (newpin?.Length == 4 && newpin.All(char.IsDigit))
                {
                    Console.WriteLine("Successfully changed your PIN.");
                    pin = newpin;



                }


                else
                {
                    Console.WriteLine("Invalid PIN. The PIN must be 4 digits and contains no characters. ");

                }




            }
            static void Depositfunds()
            {

                Console.Write("Please enter how much to deposit: ");
                double deposited;
                try // Risky code ahead
                {
                    bool success = double.TryParse(Console.ReadLine(), out deposited);

                    if (bankBalance + deposited > MaxValue)
                    {
                        Console.WriteLine("Deposit exceeds the maximum allowable balance.");
                        return;
                    }

                    if (deposited <= 0 || deposited > 5000)
                    {
                        Console.WriteLine($"Deposit must be positive and no more than £5000. Your current balance is £{bankBalance:F2}");
                        return;
                    }

                    if (success)
                    {
                        bankBalance = deposited + bankBalance;
                        Console.WriteLine($"You have successfully deposited £{deposited:F2} your total is £{bankBalance:F2}");
                    }
                    else
                    {
                        Console.WriteLine("Incorrect value, please check your depositing a correct ammount. ");
                    }
                }
                catch (Exception ex) // If it's got this far something has gone very wrong.
                {

                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }

            }
            static void Displayfunds()
            {

                Console.WriteLine($"Your current balance is: £{bankBalance:F2}");

            }
            static void Showhelp()
            {

                Console.WriteLine("\nCommands: Balance, Help, Deposit, Withdraw, Pin, Exit (Enter to continue) ");

            }
            static bool Pinsecurity(string? pininput) // Check the PIN number and return if we're locked or unlocked.
            {

                return pininput == pin;


            }
        }
    }
}