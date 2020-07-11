using Entities;
using Services;
using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    class Program
    {
        List<SKU> itemList = new List<SKU>();
        private static ISKUService _itemService;
        static void Main(string[] args)
        {
            _itemService = new SKUService();
            Console.WriteLine("Add Items");
            string action = Console.ReadLine();
            switch (action)
            {
                case "1":
                    SeedItems();
                    break;
                case "2":
                    SeedItemsPromotions();
                    break;
                case "3":
                    SelectItems();
                    break;
                case "4":
                    ShowTotalPrice();
                    break;
            }
        }

        protected static void SeedItems()
        {
            Console.WriteLine("Add Items");
            int action = Console.Read();
        }
        protected static void SeedItemsPromotions()
        {
            Console.WriteLine("Add Items");
            string action = Console.ReadLine();
            _itemService.SeedPromotions();
        }
        protected static void SelectItems()
        {

        }
        protected static void ShowTotalPrice()
        {

        }

        public static void GetItemsList()
        {
            List<char> itemsList = new List<char>();
            List<SKU> calculatedItemPrice = new List<SKU>();
            Console.WriteLine("Enter your items among A, B, C, D");
            //Test cases
            itemsList.Add('A');
            itemsList.Add('B');
            itemsList.Add('C');
            //calculatedItemPrice = CalculateTotalAmount(itemsList);
        }

    }

}
