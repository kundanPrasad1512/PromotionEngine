using Entities;
using PromotionEngine.Engine;
using Services;
using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    class Program
    {
        List<SKU> itemList = new List<SKU>();
        static ISKUService _itemService;
        static void Main(string[] args)
        {
            _itemService = new SKUService();
            Console.WriteLine("Add Items");
            string action = Console.ReadLine();
            IPromotionService promotionService = new PromotionService();
            ISKUService sKUService = new SKUService();
            sKUService.SeedSKU();
            promotionService.SeedPromotions();
            switch (action)
            {
                case "1":
                    SeedSKU();
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

        protected static void SeedSKU()
        {
            Console.WriteLine("Add Items");
            int action = Console.Read();
        }
        protected static void SeedItemsPromotions()
        {
            Console.WriteLine("Add Items");
            string action = Console.ReadLine();
            //promotionService.SeedPromotions();
        }
        protected static void SelectItems()
        {

        }
        protected static void ShowTotalPrice()
        {
            List<char> itemsList = new List<char>();
            itemsList.Add('A');
            itemsList.Add('A');
            itemsList.Add('A');
            itemsList.Add('B');
            itemsList.Add('B');
            itemsList.Add('B');
            itemsList.Add('B');
            itemsList.Add('B');
            itemsList.Add('C');
            itemsList.Add('D');
            IPromotionRuleEngine promotionEngine = new PromotionRuleEngine();
            int total=promotionEngine.Calculation(itemsList);
            Console.WriteLine("Total Amount is :"+ total);
            Console.ReadKey();
        }

    }

}
