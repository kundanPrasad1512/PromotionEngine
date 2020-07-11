using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IPromotionService
    {
        void SeedPromotions();
        List<Promotion> GetAllActivePromotions();
    }
}
