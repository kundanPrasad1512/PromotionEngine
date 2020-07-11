using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class PromotionRepository : IPromotionRepository
    {
        static List<Promotion> promotionList;
        ISKURepository skuRepository;
        public PromotionRepository()
        {
            skuRepository = new SKURepository();
        }
        public void SeedPromotions()
        {
            promotionList = new List<Promotion>
            {
                new Promotion
                {
                    ID = 1,
                    PromotionType = "Multi",
                    DiscountPercentage = 0,
                    SKUList = new List<SKU>() { new SKU { ID = 'A', Quantity = 3,Price = skuRepository.GetSKUByID('A').Price} },
                    DiscountPrice = 130,
                    IsActive=true
                },
                new Promotion
                {
                    ID = 1,
                    PromotionType = "Multi",
                    DiscountPercentage = 0,
                    SKUList = new List<SKU>() { new SKU { ID = 'B', Quantity = 2, Price = skuRepository.GetSKUByID('B').Price } },
                    DiscountPrice = 45,
                    IsActive=true
                },
                new Promotion
                {
                    ID = 1,
                    PromotionType = "Combo",
                    DiscountPercentage = 0,
                    SKUList = new List<SKU>() { new SKU { ID = 'C', Quantity = 1, Price = skuRepository.GetSKUByID('C').Price },new SKU { ID = 'D', Quantity = 1, Price = skuRepository.GetSKUByID('D').Price } },
                    DiscountPrice = 30,
                    IsActive=true
                }
            };
        }
        public List<Promotion> GetAllActivePromotions()
        {
            return promotionList.FindAll(p => p.IsActive);
        }
    }
}
