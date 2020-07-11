using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Promotion
    {
        public Promotion()
        {
            SKUList = new List<SKU>();
        }
        public int ID { get; set; }
        public string PromotionType { get; set; }
        public List<SKU> SKUList { get; set; }
        public int DiscountPrice { get; set; }
        public int DiscountPercentage { get; set; }
        public bool IsActive { get; set; }
    }
}
