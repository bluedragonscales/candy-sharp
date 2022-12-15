using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    class Candy
    {
        //This class Candy initiates each type of individual candy for the candy store with it's own item number, name/description, price, and the amount in stock.
        public string itemNumber;
        public string candyName;
        public double candyPrice;
        public int candyStock;

        public Candy(string candyName, double candyPrice, int candyStock)
        {
            itemNumber = itemNum++.ToString();
            this.candyName = candyName;
            this.candyPrice = candyPrice;
            this.candyStock = candyStock;
        }

        //The char will be initiated with 'A' and will go down the alphabet each time a new candy is created
        //so that each candy will have a unique item number identifier.
        public static char itemNum = 'A';

    } //END OF CLASS CANDY --------------------------



    class CandyStore
    {
        //The candy store's list of candy objects.
        private List<Candy> candyList = new List<Candy>();

        //The user's shopping cart and checkout total.
        private List<Candy> shoppingCart = new List<Candy>();
        private double checkoutTotal;

        public CandyStore()
        {
            //The store list is set and refreshed every time the program starts over.
            candyList.Add(new Candy("Chocolate Bar", 1.00, 100));
            candyList.Add(new Candy("Gumdrop", 0.50, 100));
            candyList.Add(new Candy("Lollipop", 0.75, 100));
            candyList.Add(new Candy("Giant Jawbreaker", 1.00, 100));
        }

        public void ShowInventory()
        {
            //Shows all the candy the store has in stock and their prices.
            Console.WriteLine("Item Candy            Inventory  Price ");
            Console.WriteLine("---------------------------------------");
            foreach (var c in candyList)
            {
                Console.WriteLine("{0, -5}|{1, -16}|{2, -10}|{3, -5:C2}", c.itemNumber, c.candyName, c.candyStock, c.candyPrice);
            }
        }

        public void WelcomeMessage()
        {
            //The initial welcome message for the user.
            Console.WriteLine("Welcome to Candy#, the candy store!");
            Console.WriteLine("Take a look at our stock of delicious candy.\n");
            ShowInventory();
            Console.WriteLine("\n");
        }

        public int ReturnStock(string type)
        {
            //Return the amount of stock in the candy requested by the user so the
            //user doen't try to get more than what is in stock.
            char c = char.Parse(type);
            int stockAmount = 0;
            switch (c)
            {
                case 'A':
                    stockAmount = candyList[0].candyStock;
                    break;
                case 'B':
                    stockAmount = candyList[1].candyStock;
                    break;
                case 'C':
                    stockAmount = candyList[2].candyStock;
                    break;
                case 'D':
                    stockAmount = candyList[3].candyStock;
                    break;
                default:
                    Console.WriteLine("This is not a valid type of candy.");
                    break;
            }

            return stockAmount;
        }


        public void AddToCart(string type, int amount)
        {
            //User can add stuff to their cart.
            foreach (var c in candyList)
            {
                if (type == c.itemNumber)
                {
                    c.candyStock -= amount;
                    shoppingCart.Add(new Candy(c.candyName, c.candyPrice, amount));
                }
            }
        }


        public void ChangeQuantity(string type, int amount)
        {
            //User can change the quantity of an item in their cart.
            foreach(var item in shoppingCart)
            {
                if(type == item.itemNumber)
                {
                    foreach(var store in candyList)
                    {
                        //If the shopping cart item name matches 
                        if (item.candyName == store.candyName)
                        {
                            //Adding the value of the stock back to the store.
                            store.candyStock -= amount;
                        }
                    }
                    item.candyStock += amount;
                }
            }
        }


        public void RemoveFromCart(string type)
        {
            //Remove items from user's shopping cart.
            //Going through their shopping cart list.
            foreach (var item in shoppingCart.ToList())
            {
                //Find the item they want to delete and grab the value of the stock they requested.
                if (type == item.itemNumber)
                {
                    //Now going through the store's candy list.
                    foreach (var store in candyList)
                    {
                        //If the shopping cart item name matches 
                        if (item.candyName == store.candyName)
                        {
                            //Adding the value of the stock back to the store.
                            store.candyStock += item.candyStock;
                        }
                    }

                    //Finally removing the item from the shopping cart.
                    shoppingCart.Remove(item);
                }
            }

            Console.WriteLine("\nYour new cart:");
            this.ShowCart();
            Console.WriteLine("\n");
        }


        public void ShowCart()
        {
            //User can see what they have in their cart so far. The ending statement "candyPrice * candyStock" is the 
            //total value for that item.
            foreach (var cart in shoppingCart)
            {
                Console.WriteLine("{0, -17} | {1, -4} | {2:C2} | {3}", cart.candyName, cart.candyStock, cart.candyPrice * cart.candyStock, cart.itemNumber);
            }
        }


        public void Checkout()
        {
            Console.WriteLine("\nThank you for shopping at Candy#!");
            int totalCandy = 0;

            foreach(var cart in shoppingCart)
            {
                totalCandy += cart.candyStock;
                this.checkoutTotal += cart.candyPrice * cart.candyStock;
            }

            if(totalCandy > 0)
            {
                if(totalCandy == 1)
                {
                    Console.WriteLine("Your cart contains 1 piece of candy.");
                }
                else
                {
                    Console.WriteLine("Your cart contains {0} pieces of candy. Try not to get a stomach ache!", totalCandy);
                }
                Console.WriteLine("Your checkout total is {0:C2}. Have a nice day!", this.checkoutTotal);
            }
            else
            {
                Console.WriteLine("Sorry to see you go!");
            }
        }

    }//END OF CLASS CANDYSTORE ------------------------




    internal class Program
    {
        static void Main()
        {
            //Be able to view the store's stock of candy and their prices right away.
            CandyStore store = new CandyStore();
            store.WelcomeMessage();

            bool continueShopping = true;
            while (continueShopping)
            {
                //Console.Write("Would you like to add to your shopping cart? (Y for yes, N for no) : ");
                Console.Write("(A)dd to your shopping cart;\n(D)elete from your shopping cart;\n(C)hange an item's quantity;\n(V)iew store inventory and your cart;\n(Q)uit shopping\nEnter your choice: ");
                string input = Console.ReadLine().ToUpper();


                switch (input)
                {
                    case "A":
                        //Because the user wants to add to their shopping cart, ask them what they
                        //would like to add and also show them what is still in stock.
                        Console.Write("\nWhat would you like to buy?\nEnter the item code (i.e. A, B, etc): ");
                        string type = Console.ReadLine().ToUpper();

                        //Ask the user how many of the item they would like to add.
                        Console.Write("How many? ");
                        string num = Console.ReadLine();
                        int convertNum = int.Parse(num);

                        //So long as the amount they requested is not more than what's in stock, it will be
                        //added to their cart.
                        if (convertNum <= store.ReturnStock(type) && store.ReturnStock(type) > 0)
                        {
                            //Add to cart object.
                            store.AddToCart(type, convertNum);
                            //Show what's inside their cart so far.
                            Console.WriteLine("\n");
                            store.ShowCart();
                            Console.WriteLine("\n");
                        }
                        else
                        {
                            Console.WriteLine("Stock is not sufficient.\n");
                            continue;
                        }
                        break;
                    case "D":
                        //A user can delete items from their shopping cart.
                        Console.WriteLine("\nSelect the item letter code to delete that item: ");
                        string code = Console.ReadLine().ToUpper();
                        store.RemoveFromCart(code);
                        break;
                    case "C":
                        //A user can change the quantity of an item in their cart.
                        Console.WriteLine("\nSelect the item letter code to change its quantity: ");
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
                        //Console.WriteLine("\n");
                        Console.WriteLine("--------------------------------------");

                        break;
                    case "Q":
                        //Break out of the while loop.
                        continueShopping = false;
                        break;
                    default:
                        Console.WriteLine("\n");
                        Console.WriteLine("Not a valid option. Try again!");
                        Console.WriteLine("\n");
                        break;
                } //END OF SWITCH
            } //END OF WHILE LOOP

            store.Checkout();

        } //END OF MAIN METHOD
    }
}
