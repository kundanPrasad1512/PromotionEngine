using DAL;
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
        private ServiceProvider serviceProvider { get; set; }

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();
            services.AddTransient<IPromotionRuleEngine, PromotionRuleEngine>();
            services.AddTransient<IPromotionService, PromotionService>();
            services.AddTransient<ISKUService, SKUService>();
            services.AddTransient<ISKURepository, SKURepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IPromotionRuleEngine, PromotionRuleEngine>();
            serviceProvider = services.BuildServiceProvider();

            _skuService = serviceProvider.GetService<ISKUService>();
            _skuService.SeedSKU();
            _promotionService = serviceProvider.GetService<IPromotionService>();
            _promotionService.SeedPromotions();
            _promotionRuleEngine = serviceProvider.GetService<IPromotionRuleEngine>();
        }

        [Test]
        public void GetTotalPrice_SenarioA()
        {
            List<char> inputs = new List<char>() {'A','B','C' };
            int actualResult = _promotionRuleEngine.Calculation(inputs);
            int expectedResult = 100;
            Assert.AreEqual(actualResult, expectedResult);
        }
        [Test]
        public void GetTotalPrice_SenarioB()
        {
            List<char> inputs = new List<char>() { 'A', 'A', 'B','C','A','B','A','B','A','B','B' };
            int actualResult = _promotionRuleEngine.Calculation(inputs);
            int expectedResult = 370;
            Assert.AreEqual(actualResult, expectedResult);
        }
        [Test]
        public void GetTotalPrice_SenarioC()
        {
            List<char> inputs = new List<char>() { 'A', 'A', 'B', 'C', 'B', 'A', 'B', 'B', 'B','D' };
            int actualResult = _promotionRuleEngine.Calculation(inputs);
            int expectedResult = 280;
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
