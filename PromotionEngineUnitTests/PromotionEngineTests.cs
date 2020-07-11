using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PromotionEngine.Engine;
using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngineUnitTests
{
    [TestFixture]
    public class PromotionEngineTests
    {
        IPromotionRuleEngine _promotionRuleEngine;
        IPromotionService _promotionService;
        ISKUService _skuService;
        public PromotionEngineTests()
        {
            _promotionRuleEngine = new PromotionRuleEngine(_promotionService, _skuService);
        }

        [Test]
        public void GetTotalPrice_ScenarioA()
        {
            List<char> inputs = new List<char>() {'A','B','C' };
            int actualResult = _promotionRuleEngine.Calculation(inputs);
            int expectedResult = 100;
            Assert.AreEqual(actualResult, expectedResult);
        }
        [Test]
        public void GetTotalPrice_ScenarioB()
        {
            List<char> inputs = new List<char>() { 'A', 'A', 'B','C','A','B','A','B','A','B','B' };
            int actualResult = _promotionRuleEngine.Calculation(inputs);
            int expectedResult = 370;
            Assert.AreEqual(actualResult, expectedResult);
        }
        [Test]
        public void GetTotalPrice_ScenarioC()
        {
            List<char> inputs = new List<char>() { 'A', 'A', 'B', 'C', 'B', 'A', 'B', 'B', 'B','D' };
            int actualResult = _promotionRuleEngine.Calculation(inputs);
            int expectedResult = 280;
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
