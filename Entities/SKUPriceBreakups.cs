using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class SKUPriceBreakups
    {
        public char SKUID { get; set; }
        public int PriceWithoutDiscount { get; set; }
        public int QuantityWithoutDiscount { get; set; }
        public int QuantityWithDiscount { get; set; }
        public int DiscountPrice { get; set; }
    }
}
