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
        public SKUService(ISKURepository skuRepository)
        {
            _skuRepository = skuRepository;
        }

        public void SeedSKU()
        {
            _skuRepository.SeedSKU();
        }
        public SKU GetSKUByID(char SkuID)
        {
            return _skuRepository.GetSKUByID(SkuID);
        }
    }
}
