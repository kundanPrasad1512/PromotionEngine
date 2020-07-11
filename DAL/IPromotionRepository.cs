using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IPromotionRepository
    {
        void SeedPromotions();
        List<Promotion> GetAllActivePromotions();
    }
}
