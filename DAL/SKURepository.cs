using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class SKURepository: ISKURepository
    {
        static List<SKU> itemList = new List<SKU>();

        public void SeedSKU()
        {
            itemList.Add(new SKU { ID = 'A', Quantity = 1, Price = 50 });
            itemList.Add(new SKU { ID = 'B', Quantity = 1, Price = 30 });
            itemList.Add(new SKU { ID = 'C', Quantity = 1, Price = 20 });
            itemList.Add(new SKU { ID = 'D', Quantity = 1, Price = 15 });
        }
        public SKU GetSKUByID(char SkuID)
        {
            return itemList.Where(s => s.ID == SkuID).FirstOrDefault();
        }

    }
}
