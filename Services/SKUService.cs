using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class SKUService : ISKUService
    {
        ISKURepository _skuRepository;
        public SKUService()
        {
            _skuRepository = new SKURepository();
        }
        public void SeedItems()
        {
            _skuRepository.SeedSKU();
        }

        public void SeedPromotions()
        {
            _skuRepository.SeedPromotions();
        }

        public List<Promotion> GetAllActivePromotions()
        {
           return _skuRepository.GetAllActivePromotions();
        }

    }
}
