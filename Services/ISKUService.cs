using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ISKUService
    {
        void SeedItems();
        void SeedPromotions();
        List<Promotion> GetAllActivePromotions();

    }
}
