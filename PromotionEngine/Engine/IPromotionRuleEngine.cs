using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Engine
{
    public interface IPromotionRuleEngine
    {
        int Calculation(List<char> itemList);
    }
}
