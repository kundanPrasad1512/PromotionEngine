using Entities;
using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    class Program
    {
        List<Item> itemList = new List<Item>();
        static void Main(string[] args)
        {

        }

        public static void GetItemsList()
        {
            List<char> itemsList = new List<char>();
            List<Item> calculatedItemPrice = new List<Item>();
            Console.WriteLine("Enter your items among A, B, C, D");
            //Test cases
            itemsList.Add('A');
            itemsList.Add('B');
            itemsList.Add('C');
            calculatedItemPrice = CalculateTotalAmount(itemsList);
        }

        public static List<Item> CalculateTotalAmount(List<char> itemsList)
        {
            List<Item> items = new List<Item>();
            foreach (char id in itemsList)
            {
                switch (id)
                {
                    case 'A':
                        break;
                    case 'B':
                        break;
                    case 'C':
                        break;
                    case 'D':
                        break;
                }
            }

            return items;
        }

    }

}
