using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface ISKURepository
    {
        void SeedSKU();
        SKU GetSKUByID(char SkuID);
    }
}
