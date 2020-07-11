using DAL;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using PromotionEngine.Engine;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine
{
    class Program
    {
        List<SKU> itemList = new List<SKU>();
        static List<char> selectedSKUList = new List<char>();
        static ISKUService _skuService;
        static IPromotionService _promotionService;
        static IPromotionRuleEngine _promotionEngine;
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddTransient<IPromotionService, PromotionService>()
            .AddTransient<ISKUService, SKUService>()
            .AddTransient<ISKURepository, SKURepository>()
            .AddTransient<IPromotionRepository, PromotionRepository>()
            .AddTransient<IPromotionRuleEngine, PromotionRuleEngine>()
            .BuildServiceProvider();

            _skuService = serviceProvider.GetService<ISKUService>();
            _skuService.SeedSKU();
            _promotionService = serviceProvider.GetService<IPromotionService>();
            _promotionService.SeedPromotions();
            _promotionEngine= serviceProvider.GetService<IPromotionRuleEngine>();
            
            SelectItems();
        }

        protected static void UserAction()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("Do you want to add more items? press 1");
            Console.WriteLine("Do you want to see selected items ? press 2");
            Console.WriteLine("Do you want to see total amount of selected items ? press 3");
            string action = Console.ReadLine();
            switch (action)
            {
                case "1":
                    SelectItems();
                    break;
                case "2":
                    ShowSelectedItems(); 
                    break;
                case "3":
                    ShowTotalPrice();
                    break;
            }
        }
        protected static void ShowSelectedItems()
        {
            try
            {
                var skuIdGroups = selectedSKUList.GroupBy(i => i.ToString()).ToArray();
                foreach (var skuIds in skuIdGroups)
                {
                    Console.WriteLine("{0} * {1}", skuIds.Key, skuIds.Count());
                }
                UserAction();
            }
            catch(Exception ex){
                throw ex;
            }

        }
        protected static void SelectItems()
        {
            try
            {
                Console.WriteLine("Select space separated items among A,B,C,D");
                string item = Console.ReadLine();
                var selectedList = item.Split(" ");
                var isValidEntry = true;
                foreach (var selItem in selectedList)
                {
                    if (!string.IsNullOrEmpty(selItem))
                    {
                        SKU sku = _skuService.GetSKUByID(selItem.ToCharArray()[0]);
                        if (sku != null)
                        {
                            selectedSKUList.Add(selItem.ToUpper().ToCharArray()[0]);
                        }
                        else
                        {
                            isValidEntry = false;
                            Console.WriteLine(selItem + " items are not valid item");
                        }
                    }
                }
                if (!isValidEntry)
                {
                    SelectItems();
                }
                else
                {
                    UserAction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected static void ShowTotalPrice()
        {
            try
            {
                int total = _promotionEngine.Calculation(selectedSKUList);
                Console.WriteLine("Total amount is :" + total);
                UserAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
