using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class PromotionService: IPromotionService
    {
        IPromotionRepository _promotionRepository;
        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public void SeedPromotions()
        {
            _promotionRepository.SeedPromotions();
        }

        public List<Promotion> GetAllActivePromotions()
        {
            return _promotionRepository.GetAllActivePromotions();
        }

    }
}
