using System;

namespace FinalProject
{
    internal class Candy
    {
        //Class Candy initiates each type of individual candy for the candy store with it's own item number,
        //name/description, price, and the amount in stock.
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

        //Each candy will have it's own char letter code, both in the store and in the user's cart.
        public static char itemNum = 'A';
    }
}
