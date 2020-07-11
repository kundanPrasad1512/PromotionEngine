using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class SeedData
    {
        List<Item> itemList = new List<Item>();
        public void SeedItems()
        {
            itemList.Add(new Item { ID = 'A', Quantity = 1, Price = 50 });
            itemList.Add(new Item { ID = 'B', Quantity = 1, Price = 30 });
            itemList.Add(new Item { ID = 'C', Quantity = 1, Price = 20 });
            itemList.Add(new Item { ID = 'D', Quantity = 1, Price = 15 });
        }

        public void SeedPromotions()
        {
            itemList.Add(new Item { ID = 'A', Quantity = 3, Price = 130 });
            itemList.Add(new Item { ID = 'B', Quantity = 2, Price = 45 });
            itemList.Add(new Item { ID = 'C', Quantity = 1, Price = 20 });
            itemList.Add(new Item { ID = 'D', Quantity = 1, Price = 15 });
        }
    }
}
