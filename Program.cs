using System;
using System.Collections.Generic;
using BankingConsoleApp.Enum;
using BankingConsoleApp.Models;

namespace BankingConsoleApp
{
    class Program
    {
        internal static List<Customer> customers = new List<Customer>();
        internal static int customerId = 0;
        internal static int accountNumberSeed = 1234567890;



        static void Main(string[] args)
        { 
            Console.WriteLine("Welcome to WellaHealth Bank");
            Console.WriteLine("How May we Help You? ");
            
            while (true) {
                
                Console.WriteLine("Pick 1 to Create an Account");
                Console.WriteLine("pick 2 to Close an Account");
                Console.WriteLine("pick 3 to Check balance");
                Console.WriteLine("pick 4 to Credit your Account");
                Console.WriteLine("pick 5 to Debit your Account");
                Console.WriteLine("pick 6 to close");

                try
                {
                    int response = int.Parse(Console.ReadLine());

                    if (response == 1) {
                        Console.Write("What is your name?: ");
                        var nameFromUser = Console.ReadLine();
                        Console.Write("What is your address?: ");
                        var addressFromUser = Console.ReadLine();
                        Console.Write("How much deposit do you want to start with?: ");
                        var depositFromUser = decimal.Parse(Console.ReadLine());
                        
                        Console.WriteLine("Create Account");
                        CreateAccount(nameFromUser, addressFromUser, depositFromUser);
                        Console.WriteLine("Account created successfuly.....");
                        Console.WriteLine("You account number is: " + accountNumberSeed);
                        continue;
                    } else if(response == 2) {
                        Console.WriteLine("What is your account Number: ");
                        var accountNumberFromUser = int.Parse(Console.ReadLine());
                        Console.WriteLine("Are you sure you wish to close this account? (yes/no)");
                        var areYouSure = Console.ReadLine().ToLower();
                        if(areYouSure == "yes") {
                            CloseAccount(accountNumberFromUser);
                        }
                        continue;
                    } else if (response == 3) {
                        Console.Write("What is your account Number: ");
                        var accountNumberFromUser = int.Parse(Console.ReadLine());
                        var balance = CheckBalance(accountNumberFromUser);
                        if(balance == null) {
                            Console.WriteLine("Account does not exist");
                        } else {
                            Console.WriteLine("===>Check Balance....");
                            Console.WriteLine("===>Your balance is: " + balance);
                        }
                        continue;
                    } else if (response == 4) {
                        
                        Console.Write("Enter your account number: ");
                        var accountNumberFromUser = int.Parse(Console.ReadLine());

                        Console.Write("Enter amount to be deposited: ");
                        string input = (Console.ReadLine());
                        decimal amountFromUser = Convert.ToDecimal(input);
                        
                        var credit = CreditAccount(accountNumberFromUser, amountFromUser);
                        if(credit == accountNumberFromUser) {
                            Console.WriteLine("Please insert an amount");
                        } else {
                            Console.WriteLine("Account number has been credited with " + credit);
                        }
                        
                        continue;
                    } else if (response == 5) {
                        Console.Write("What is your account number: ");
                        var accountNumberFromUser = int.Parse(Console.ReadLine());

                        Console.Write("How much do you want to transfer: ");
                        string input = (Console.ReadLine());
                        decimal transfer = Convert.ToDecimal(input);

                        Console.Write("What is the account number you want to send to: ");
                        var newAccountNumber = int.Parse(Console.ReadLine());

                        
                       // Console.WriteLine("Your new balance is " + balance);
                        continue;
                    } else if (response == 6) {
                        Console.WriteLine("Thanks for banking with us");
                        break;
                    } else {
                        Console.WriteLine("Wrong number inputted");
                        continue;
                    }
                }
                catch (System.Exception ex)
               {
                
                   Console.WriteLine("Please check value. Message: " + ex.Message);
               }
             
            
            }

            #region Create Account

            static void CreateAccount(string name, string address, decimal amount) {
                customerId += 1;
                accountNumberSeed += 1;

                Console.WriteLine("Mapping Customer");
                Customer customer = new Customer();
                customer.Id = customerId;
                customer.Name = name;
                customer.Address = address;
                
                Console.WriteLine("Mapping Account Details");
                AccountDetails accountDetail = new AccountDetails();
                accountDetail.AccountNumber = accountNumberSeed;
                accountDetail.Balance = amount;
                accountDetail.AccountStatus = AccountStatus.Active;
                customer.AccountDetails = accountDetail;
                
                Console.WriteLine("Mapping Transactions");
                Transactions transaction = new Transactions();
                transaction.Amount = amount;
                transaction.TransactionType = TransactionType.Credit;
                transaction.DateOfTransaction = DateTime.UtcNow;
                
                Console.WriteLine("Adding Transaction");
                customer.AccountDetails.Transactions.Add(transaction);

                Console.WriteLine("Adding to customer");
                customers.Add(customer);
            }
            #endregion

            
            #region Close Account



            static void CloseAccount(int accountNumber) {
                foreach(var customer in customers) {
                    if (customer.AccountDetails.AccountNumber == accountNumber) {
                        Console.WriteLine("Account Number Found");
                        customer.AccountDetails.AccountStatus = AccountStatus.Disabled;
                        Console.WriteLine($"Account of: {accountNumber} disabled");
                        return;
                    }
                }
                Console.WriteLine("Account number not found");
                return;
            }
            #endregion
        
            #region Check Balance

            static decimal? CheckBalance(int accountNumber) {
                foreach(var customer in customers) {
                    if (customer.AccountDetails.AccountNumber == accountNumber) {
                        Console.WriteLine("Account Number found");
                        return customer.AccountDetails.Balance;
                    }
                }
                return null;
            }
            #endregion

            #region Credit Account

             static decimal? CreditAccount(int accountNumber, decimal depositAmount) {
                foreach(var customer in customers) {
                   customers[accountNumber].AccountDetails.Balance += depositAmount; 
                        Transactions transaction = new Transactions();
                        transaction.Amount = depositAmount;
                        transaction.TransactionType = TransactionType.Credit;
                        transaction.DateOfTransaction = DateTime.UtcNow;
                        customers[accountNumber].AccountDetails.Transactions.Add(transaction);
                        
                    } 
                    return depositAmount;
                }
                
               
            

            #endregion

            #region Debit Account

            

            


            #endregion
        }
        
        
    }
}
