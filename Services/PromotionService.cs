using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class PromotionService: IPromotionService
    {
        IPromotionRepository promotionRepository;
        public PromotionService()
        {
            promotionRepository = new PromotionRepository();
        }

        public void SeedPromotions()
        {
            promotionRepository.SeedPromotions();
        }

        public List<Promotion> GetAllActivePromotions()
        {
            return promotionRepository.GetAllActivePromotions();
        }

    }
}
