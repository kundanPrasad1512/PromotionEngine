using Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Engine
{
    public class PromotionRuleEngine : IPromotionRuleEngine
    {
        IPromotionService _promotionService;
        ISKUService _skuService;
        List<char> processedSKUList;
        public PromotionRuleEngine(IPromotionService promotionService, ISKUService skuService)
        {
            _promotionService = promotionService;
            _skuService = skuService;
        }
        public int CalculateTotalPrice(List<char> skuIdList)
        {
            try
            {
                List<Promotion> promotions = _promotionService.GetAllActivePromotions();
                var skuIdGroups = skuIdList.GroupBy(i => i.ToString());
                int total = 0;
                processedSKUList = new List<char>();

                foreach (var skuIds in skuIdGroups)
                {
                    List<Promotion> applicablePromoList = promotions.Where(p=> p.SKUList.Any(s=>s.ID.ToString() == skuIds.Key)).ToList();
                    
                    int selectedSKUCount = skuIds.Count();
                    bool isPriceAdded = processedSKUList.Any(s => s == skuIds.Key.ToCharArray()[0]);
                    if (applicablePromoList.Count() > 0 && !isPriceAdded)
                    {
                        foreach (Promotion promotion in applicablePromoList)
                        {
                            if (promotion.SKUList.Any(s => s.ID.ToString() == skuIds.Key))
                            {
                                int discountPrice = promotion.DiscountPrice;
                                if (promotion.PromotionType == "Multi")
                                {
                                    total += CalculatePriceMultiItemPromo(promotion, selectedSKUCount);
                                    processedSKUList.Add(promotion.SKUList[0].ID);
                                    break;
                                }
                                else if (promotion.PromotionType == "Combo")
                                {
                                    total += CalculatePriceComboItemPromo(promotion,selectedSKUCount, skuIdGroups);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!isPriceAdded)
                        {
                            SKU skuItem = _skuService.GetSKUByID(skuIds.Key.ToCharArray()[0]);
                            total += skuItem.Price;
                            processedSKUList.Add(skuIds.Key.ToCharArray()[0]);
                        }
                    }
                    
                }

                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CalculatePriceMultiItemPromo(Promotion promotion, int selectedSKUCount)
        {
            try
            {
                int total = 0;
                int promoQunatity = promotion.SKUList[0].Quantity;
                if (selectedSKUCount >= promoQunatity)
                {
                    int promoApplicableCount = selectedSKUCount / promoQunatity;
                    int withoutPromoCount = selectedSKUCount % promoQunatity;
                    if (promoApplicableCount > 0)
                    {
                        total += (promoApplicableCount * promotion.DiscountPrice) + (withoutPromoCount * promotion.SKUList[0].Price);
                    }
                }
                else
                {
                    total += (selectedSKUCount * promotion.SKUList[0].Price);
                }
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CalculatePriceComboItemPromo(Promotion promotion, int selectedSKUCount,IEnumerable<IGrouping<string, char>> skuIdGroups)
        {
            try
            {
                int total = 0;
                Dictionary<char, int> skuCount = new Dictionary<char, int>();
                foreach (SKU sku in promotion.SKUList)
                {
                    int count = skuIdGroups.Where(s => s.Key.ToString() == sku.ID.ToString()).Count();
                    skuCount.Add(sku.ID, count);
                }

                int itemCount1 = skuCount.ElementAt(0).Value;
                int itemCount2 = skuCount.ElementAt(1).Value;

                if (itemCount1 != 0 && itemCount2 != 0)
                {
                    if (itemCount1 < itemCount2)
                    {
                        SKU skuItem = _skuService.GetSKUByID(skuCount.ElementAt(1).Key);
                        total += (itemCount1 * promotion.DiscountPrice) + (itemCount2 - itemCount1) * skuItem.Price;
                    }
                    else
                    {
                        SKU skuItem = _skuService.GetSKUByID(skuCount.ElementAt(0).Key);
                        total += (itemCount2 * promotion.DiscountPrice) + (itemCount1 - itemCount2) * skuItem.Price;
                    }
                    processedSKUList.Add(skuCount.ElementAt(0).Key);
                    processedSKUList.Add(skuCount.ElementAt(1).Key);
                }
                else
                {
                    if (itemCount1 > 0)
                    {
                        SKU skuItem = _skuService.GetSKUByID(skuCount.ElementAt(0).Key);
                        total += itemCount1 * skuItem.Price;
                    }
                    else
                    {
                        SKU skuItem = _skuService.GetSKUByID(skuCount.ElementAt(1).Key);
                        total += itemCount2 * skuItem.Price;
                    }
                }
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
