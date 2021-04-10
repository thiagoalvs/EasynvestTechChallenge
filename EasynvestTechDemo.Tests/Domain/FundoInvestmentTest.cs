using EasynvestTechDemo.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Tests.Domain
{
    [TestClass]
    public class FundoInvestmentTest
    {

        [TestMethod]
        public void DeveRetirar15PorCentoDaRentabilidadeParaCalucularIR()
        {
            FundosInvestment fundosInvestment = new FundosInvestment("Teste", 800, 1000, DateTime.Today, DateTime.Today.AddYears(1));

            Assert.IsTrue(fundosInvestment.IncomeTax == 30); // (1000 - 800) * 0.15 = 30
        }

    }
}
