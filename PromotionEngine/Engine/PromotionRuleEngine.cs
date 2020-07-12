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
        public PromotionRuleEngine(IPromotionService promotionService, ISKUService skuService)
        {
            _promotionService = promotionService;
            _skuService = skuService;
        }
        public virtual int Calculation(List<char> skuIdList)
        {
            try
            {
                List<SKUPriceBreakups> finalPriceBreakups = new List<SKUPriceBreakups>();
                List<Promotion> promotions = _promotionService.GetAllActivePromotions();
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
                                            finalPriceBreakups.Add(finalPriceBreakup);
                                            break;
                                        }
                                        else
                                        {
                                            finalPriceBreakup.SKUID = promotion.SKUList[0].ID;
                                            finalPriceBreakup.QuantityWithoutDiscount = withoutPromoCount;
                                            finalPriceBreakup.PriceWithoutDiscount = promotion.SKUList[0].Price;
                                            finalPriceBreakups.Add(finalPriceBreakup);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        total += (selectedSKUCount * promotion.SKUList[0].Price);
                                        finalPriceBreakup.SKUID = promotion.SKUList[0].ID;
                                        finalPriceBreakup.QuantityWithoutDiscount = selectedSKUCount;
                                        finalPriceBreakup.PriceWithoutDiscount = promotion.SKUList[0].Price;
                                        finalPriceBreakups.Add(finalPriceBreakup);
                                    }
                                    
                                }
                                else if (promotion.PromotionType == "Combo")
                                {
                                    Dictionary<char, int> skuCount = new Dictionary<char, int>();
                                    foreach (SKU sku in promotion.SKUList)
                                    {
                                        int count = skuIdGroups.Where(s=>s.Key== sku.ID.ToString()).Count();
                                        skuCount.Add(sku.ID, count);
                                    }

                                    int itemCount1 = skuCount.ElementAt(0).Value;
                                    int itemCount2 = skuCount.ElementAt(1).Value;

                                    if (itemCount1 !=0 && itemCount2 !=0)
                                    {
                                        
                                        if (itemCount1 < itemCount2)
                                        {
                                            SKU skuItem = _skuService.GetSKUByID(skuCount.ElementAt(1).Key);
                                            total += (itemCount1 * discountPrice) + (itemCount2- itemCount1)* skuItem.Price;
                                            finalPriceBreakup.QuantityWithDiscount = itemCount1;
                                            finalPriceBreakup.DiscountPrice = discountPrice;
                                        }
                                        else
                                        {
                                            SKU skuItem = _skuService.GetSKUByID(skuCount.ElementAt(0).Key);
                                            total += (itemCount2 * discountPrice) + (itemCount1 - itemCount2) * skuItem.Price;
                                            finalPriceBreakup.QuantityWithDiscount = itemCount1;
                                            finalPriceBreakup.DiscountPrice = discountPrice;
                                        }
                                        finalPriceBreakup.SKUID = skuCount.ElementAt(0).Key;
                                        finalPriceBreakups.Add(finalPriceBreakup);
                                        finalPriceBreakup.SKUID = skuCount.ElementAt(1).Key;
                                        finalPriceBreakups.Add(finalPriceBreakup);
                                    }
                                    else
                                    {
                                        if (itemCount1 >0)
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
