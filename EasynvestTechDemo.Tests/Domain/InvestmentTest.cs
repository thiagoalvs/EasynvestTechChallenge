using EasynvestTechDemo.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Tests.Domain
{
    [TestClass]
    public class InvestmentTest
    {

        [TestMethod]
        public void DeveRetirar30PorCentoDoValorInvestidoAoResgatarComMenosDaMetadeDoTempoPrevistoCumprida()
        {
            FundosInvestment fundosInvestment = new FundosInvestment("Teste", 1000, 1000, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(2));

            Assert.IsTrue(fundosInvestment.DrawAmount == 700); // 1000 - (1000 * 0.3) = 700
        }

        [TestMethod]
        public void DeveRetirar15PorCentoDoValorInvestidoAoResgatarComMaisDaMetadeDoTempoPrevistoCumprida()
        {
            FundosInvestment fundosInvestment = new FundosInvestment("Teste", 1000, 1000, DateTime.Now.AddYears(-2), DateTime.Now.AddYears(1));

            Assert.IsTrue(fundosInvestment.DrawAmount == 850); // 1000 - (1000 * 0.15) = 850
        }

        [TestMethod]
        public void DeveRetirar6PorCentoDoValorInvestidoAoResgatarComMenosDe6MesesRestantesDoTempoPrevisto()
        {
            FundosInvestment fundosInvestment = new FundosInvestment("Teste", 1000, 1000, DateTime.Now.AddYears(-2), DateTime.Now.AddMonths(2));

            Assert.IsTrue(fundosInvestment.DrawAmount == 940); // 1000 - (1000 * 0.06) = 940
        }
    }
}
