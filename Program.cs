using System;

namespace FinalProject
{
    internal class Program
    {
        static void Main()
        {
            //View the initial store's stock of candy and their prices.
            CandyStore store = new CandyStore();
            store.WelcomeMessage();

            //A boolean to exit the program when needed.
            bool continueShopping = true;
            while (continueShopping)
            {
                //Present options to the user.
                Console.Write("(A)dd to your shopping cart;\n(D)elete from your shopping cart;\n(C)hange an item's quantity;\n(V)iew store inventory and your cart;\n(Q)uit shopping\nEnter your choice: ");
                string input = Console.ReadLine().ToUpper();


                switch (input)
                {
                    case "A":
                        //Because the user wants to add to their shopping cart, ask them what they
                        //would like to add.
                        Console.Write("\nWhat would you like to buy?\nEnter the item code (A, B, C, D): ");
                        string type = Console.ReadLine().ToUpper();

                        //Ask the user how many of the item they would like to add and parse their answer
                        //into an integer.
                        Console.Write("How many? ");
                        string num = Console.ReadLine();
                        int convertNum = int.Parse(num);

                        //So long as the amount they requested is not more than what's in stock, it will be
                        //added to their cart.
                        if (convertNum <= store.ReturnStock(type) && store.ReturnStock(type) > 0)
                        {
                            //Add to cart object List.
                            store.AddToCart(type, convertNum);
                            //Show what user added and what's inside their cart so far.
                            Console.WriteLine("\n");
                            store.ShowCart();
                            Console.WriteLine("\n");
                        }
                        else
                        {
                            //If stock is insufficient for the amount they're requesting, show this message
                            //and go back to the options screen.
                            Console.WriteLine("Stock is not sufficient.\n");
                            continue;
                        }
                        break;
                    case "D":
                        //A user can delete items from their shopping cart.
                        Console.WriteLine("\nSelect the item letter code in your cart to delete that item: ");
                        string code = Console.ReadLine().ToUpper();
                        store.RemoveFromCart(code);
                        break;
                    case "C":
                        //A user can change the quantity of an item in their cart. They first choose the item
                        //they want to change and then the amount they want to change it by.
                        Console.WriteLine("\nSelect the item letter code in your cart to change its quantity: ");
                        string code2 = Console.ReadLine().ToUpper();

                        Console.WriteLine("Enter negative amount to subtract from and a positive amount to add to: ");
                        int change = int.Parse(Console.ReadLine());
                        store.ChangeQuantity(code2, change);
                        break;
                    case "V":
                        //A view of all the store's remaining inventory.
                        Console.WriteLine("\n--------------------------------------");
                        Console.WriteLine("Store inventory:");
                        store.ShowInventory();

                        //A view of the user's current shopping cart.
                        Console.WriteLine("\nYour cart:");
                        store.ShowCart();
                        Console.WriteLine("--------------------------------------");
                        break;
                    case "Q":
                        //Break out of the while loop to finish shopping.
                        continueShopping = false;
                        break;
                    default:
                        //In case the user enters an option not available.
                        Console.WriteLine("\n");
                        Console.WriteLine("Not a valid option. Try again!");
                        Console.WriteLine("\n");
                        break;
                } //END OF SWITCH
            } //END OF WHILE LOOP

            //This store object's checkout function will give the user a goodbye message.
            store.Checkout();

        } //END OF MAIN METHOD
    }
}
