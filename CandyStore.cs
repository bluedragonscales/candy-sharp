using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    internal class CandyStore
    {
        //Class CandyStore contains all the functionality for the user to go shopping for candy, fill their
        //cart with candy, and checkout with a dollar total at the end.

        //The candy store's list of candy objects. The items the user can shop for.
        private List<Candy> candyList = new List<Candy>();

        //The user's shopping cart and checkout total.
        private List<Candy> shoppingCart = new List<Candy>();
        private double checkoutTotal;

        //The store list is set and refreshed with these candies every time the program starts over.
        public CandyStore()
        {
            candyList.Add(new Candy("Chocolate Bar", 1.00, 100));
            candyList.Add(new Candy("Gumdrop", 0.50, 100));
            candyList.Add(new Candy("Lollipop", 0.75, 100));
            candyList.Add(new Candy("Giant Jawbreaker", 1.00, 100));
        }

        //Shows all the candy the store has in stock and their prices when the user requests to see it.
        //Uses a foreach loop to run through the List called candyList and formats it for better viewing
        //for the user.
        public void ShowInventory()
        {
            Console.WriteLine("Item Candy            Inventory  Price ");
            Console.WriteLine("---------------------------------------");
            foreach (var c in candyList)
            {
                Console.WriteLine("{0, -5}|{1, -16}|{2, -10}|{3, -5:C2}", c.itemNumber, c.candyName, c.candyStock, c.candyPrice);
            }
        }

        //The initial welcome message for the user including the stocked candy options.
        public void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Candy#, the candy store!");
            Console.WriteLine("Take a look at our stock of delicious candy.\n");
            ShowInventory();
            Console.WriteLine("\n");
        }

        //Return the amount of stock in the candy requested by the user so the user doen't
        //try to get more than what's in stock. This function will return the stock integer
        //of the type of candy requested via a switch case.
        public int ReturnStock(string type)
        {
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


        //User can add stuff to their cart. This function uses a foreach loop to go through the
        //candy in the candyList of the store and so long as the candy is a valid type requested 
        //by the user, it will add that candy object and its values to the user's shopping cart.
        public void AddToCart(string type, int amount)
        {
            foreach (var c in candyList)
            {
                if (type == c.itemNumber)
                {
                    c.candyStock -= amount;
                    shoppingCart.Add(new Candy(c.candyName, c.candyPrice, amount));
                }
            }
        }


        //User can change the quantity of an item in their cart. The first foreach loop looks through all
        //the candy in the user's shopping cart. If the type they chose, matches the item letter code in their
        //cart then the function runs through another foreach loop to find the matching candy of the store's
        //List of candy objects. Then it can subtract or add the amount the user wants to change to/from the store's
        //inventory. It then exits the inner foreach loop and changes the amount in the user's cart.
        public void ChangeQuantity(string type, int amount)
        {
            foreach (var item in shoppingCart)
            {
                if (type == item.itemNumber)
                {
                    foreach (var store in candyList)
                    {
                        if (item.candyName == store.candyName)
                        {
                            store.candyStock -= amount;
                        }
                    }
                    item.candyStock += amount;
                }
            }
        }


        //Remove items from user's shopping cart. The first foreach loop, if statement, and inner foreach loop 
        //looks for the correct candy, matches it to the candy in the store's candy list, adds the quantity in the
        //user's cart back to the store's stock. Then after exiting the inner foreach loop, the item will be 
        //removed from the user's shopping cart.
        public void RemoveFromCart(string type)
        {
            foreach (var item in shoppingCart.ToList())
            {
                if (type == item.itemNumber)
                {
                    foreach (var store in candyList)
                    {
                        if (item.candyName == store.candyName)
                        {
                            store.candyStock += item.candyStock;
                        }
                    }

                    shoppingCart.Remove(item);
                }
            }

            //User's new cart contents will be shown to them.
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

            foreach (var cart in shoppingCart)
            {
                totalCandy += cart.candyStock;
                this.checkoutTotal += cart.candyPrice * cart.candyStock;
            }

            if (totalCandy > 0)
            {
                if (totalCandy == 1)
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
    }
}
