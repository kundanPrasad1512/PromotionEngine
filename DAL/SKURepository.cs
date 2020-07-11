using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class SKURepository: ISKURepository
    {
        List<SKU> itemList = new List<SKU>();
        List<Promotion> promotionList;
        public void SeedSKU()
        {
            itemList.Add(new SKU { ID = 'A', Quantity = 1, Price = 50 });
            itemList.Add(new SKU { ID = 'B', Quantity = 1, Price = 30 });
            itemList.Add(new SKU { ID = 'C', Quantity = 1, Price = 20 });
            itemList.Add(new SKU { ID = 'D', Quantity = 1, Price = 15 });
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
                    SKUList = new List<SKU>() { new SKU { ID = 'A', Quantity = 3 } },
                    DiscountPrice = 130,
                    IsActive=true
                },
                new Promotion
                {
                    ID = 1,
                    PromotionType = "Multi",
                    DiscountPercentage = 0,
                    SKUList = new List<SKU>() { new SKU { ID = 'B', Quantity = 2 } },
                    DiscountPrice = 45,
                    IsActive=true
                },
                new Promotion
                {
                    ID = 1,
                    PromotionType = "Combo",
                    DiscountPercentage = 0,
                    SKUList = new List<SKU>() { new SKU { ID = 'C', Quantity = 1 },new SKU { ID = 'D', Quantity = 1 } },
                    DiscountPrice = 30,
                    IsActive=true
                }
            };
        }

        public List<Promotion> GetAllActivePromotions()
        {
            return promotionList.Where(p=>p.IsActive).ToList();
        }
    }
}
