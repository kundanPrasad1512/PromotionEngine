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
        public PromotionRuleEngine()
        {

        }
        public int Calculation(List<char> skuIdList)
        {
            try
            {
                IPromotionService promotionService = new PromotionService();
                ISKUService skuService = new SKUService();
                List<SKUPriceBreakups> finalPriceBreakups = new List<SKUPriceBreakups>();
                List<Promotion> promotions = promotionService.GetAllActivePromotions();
                var skuIdGroups = skuIdList.GroupBy(i => i.ToString()).ToArray();
                int total = 0;

                foreach (var skuIds in skuIdGroups)
                {
                    List<Promotion> applicablePromoList = promotions.Where(p=> p.SKUList.Any(s=>s.ID.ToString() == skuIds.Key)).ToList();
                    SKUPriceBreakups finalPriceBreakup = new SKUPriceBreakups();
                    var selectedSKUCount = skuIds.Count();
                    bool isPriceAdded = finalPriceBreakups.Any(p => p.SKUID == skuIds.Key.ToCharArray()[0]);
                    if (applicablePromoList.Count() > 0 && !isPriceAdded)
                    {
                        foreach (Promotion promotion in applicablePromoList)
                        {
                            if (promotion.SKUList.Any(s => s.ID.ToString() == skuIds.Key))
                            {
                                int discountPrice = promotion.DiscountPrice;
                                
                                if (promotion.PromotionType == "Multi")
                                {
                                    int promoQunatity = promotion.SKUList[0].Quantity;
                                    if (selectedSKUCount >= promoQunatity)
                                    {
                                        int promoApplicableCount = selectedSKUCount / promoQunatity;
                                        int withoutPromoCount = selectedSKUCount % promoQunatity;
                                        if (promoApplicableCount > 0)
                                        {
                                            total += (promoApplicableCount * discountPrice) + (withoutPromoCount * promotion.SKUList[0].Price);

                                            finalPriceBreakup.SKUID = promotion.SKUList[0].ID;
                                            finalPriceBreakup.QuantityWithDiscount = promoApplicableCount;
                                            finalPriceBreakup.DiscountPrice = discountPrice;
                                            finalPriceBreakup.QuantityWithoutDiscount = withoutPromoCount;
                                            finalPriceBreakup.PriceWithoutDiscount = promotion.SKUList[0].Price;
                                            break;
                                        }
                                        else
                                        {
                                            finalPriceBreakup.SKUID = promotion.SKUList[0].ID;
                                            finalPriceBreakup.QuantityWithoutDiscount = withoutPromoCount;
                                            finalPriceBreakup.PriceWithoutDiscount = promotion.SKUList[0].Price;
                                            break;
                                        }
                                    }
                                }
                                else if (promotion.PromotionType == "Combo")
                                {
                                    Dictionary<char, int> skuCount = new Dictionary<char, int>();
                                    foreach (SKU sku in promotion.SKUList)
                                    {
                                        int count = skuIdGroups.Where(s=>s.Key== sku.ID.ToString()).Count();
                                        if (count>0)
                                        {
                                            skuCount.Add(sku.ID, count);
                                        }
                                        if (skuCount.Count() == promotion.SKUList.Count())
                                        {

                                        }
                                    }
                                    break;
                                }

                                finalPriceBreakup.Total = total;
                                finalPriceBreakups.Add(finalPriceBreakup);
                            }
                        }
                    }
                    else
                    {
                        if (!isPriceAdded)
                        {
                            SKU skuItem = skuService.GetSKUByID(skuIds.Key.ToCharArray()[0]);
                            total += skuItem.Price;
                            finalPriceBreakup.SKUID =skuIds.Key.ToCharArray()[0];
                            finalPriceBreakup.QuantityWithoutDiscount = selectedSKUCount;
                            finalPriceBreakup.PriceWithoutDiscount = skuItem.Price;
                            finalPriceBreakups.Add(finalPriceBreakup);
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
    }
}
