/*
 * Sami Alzoubi
 * CPSC 23000
 * February 34, 2024
 * Storefront
 */

// BEFORE YOU START, make sure your list doesnt include the "$" and if you have somethign with two words, dont use a space. EXAMPLE : Mountain Dew should be MountainDew in the text/notepad
using static System.Console;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;



namespace AlzoubiStorefront
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteLine("**************************************************"); //welcome banner
            WriteLine("STOREFRONT V1.0");
            WriteLine("**************************************************");

            Write("Enter the name of the file for the inventory: ");
            string fileName = ReadLine();  // enter the groceries list file path (C:\Users\samia\groceries.txt for me)
            string line;
            string[] parts;
            string itemName;
            double itemPrice;
            Dictionary<string, double> inventory = new Dictionary<string, double>();
            Dictionary<string, int> shoppingCart = new Dictionary<string, int>();

            try
            {
                if (File.Exists(fileName)) // checks if the txt file exists
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        while (!sr.EndOfStream)
                        {
                            line = sr.ReadLine().Trim();
                            if (line.Length > 0)
                            {
                                parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                                itemName = parts[0];
                                itemPrice = double.Parse(parts[1]);
                                inventory[itemName] = itemPrice;
                            }
                        }

                        WriteLine("Store inventory has been loaded, start shopping");

                        do
                        {
                            WriteLine("What would you like to purchase?");
                            foreach (var item in inventory) // this will show the items from the text/notepad
                            {
                                WriteLine($"{item.Key} ${item.Value:F2}"); // this will show the items from the text/notepad
                            }

                            Write("Enter an item or quit to stop looking/shopping: ");
                            itemName = ReadLine().Trim();

                            if (itemName.ToLower() != "quit") //if they choose quit, that means they are done shopping and it will list what they purchased with the bill later on
                            {
                                if (inventory.ContainsKey(itemName))
                                {
                                    Write($"How much {itemName} would you like to purchase? ");
                                    int quantity = int.Parse(ReadLine());

                                    if (shoppingCart.ContainsKey(itemName))
                                        shoppingCart[itemName] += quantity;
                                    else
                                        shoppingCart[itemName] = quantity;
                                }
                                else
                                {
                                    WriteLine("Try again.");
                                }
                            }
                        } while (itemName.ToLower() != "quit");

                        WriteLine("Thanks for shopping at Samis store, here is what you purchased: ");
                        double totalBill = 0;

                        foreach (var item in shoppingCart)
                        {
                            string cartItemName = item.Key;
                            int cartAmount = item.Value;
                            double cartItemValue = inventory[cartItemName];

                            WriteLine($"{cartItemName} {cartAmount}");
                            totalBill += cartAmount * cartItemValue;

                        }

                        WriteLine($"Your total bill is ${totalBill:F2}."); // prints the bill after user is done shopping
                    }
                }
            }
            catch (Exception ex)
            { // displays error message 
                WriteLine("Could not read the file from beginning to end.");
                WriteLine("This is the error: ");
                WriteLine(ex.Message);
            }
        }
    }
}
