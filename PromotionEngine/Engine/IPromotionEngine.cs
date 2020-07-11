using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Engine
{
    public interface IPromotionEngine
    {
        int Calculation(List<Item> itemList);
    }
}
