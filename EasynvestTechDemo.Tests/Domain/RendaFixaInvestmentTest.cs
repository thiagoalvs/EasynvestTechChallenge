using EasynvestTechDemo.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Tests.Domain
{
    [TestClass]
    public class RendaFixaInvestmentTest
    {

        [TestMethod]
        public void DeveRetirar15PorCentoDaRentabilidadeParaCalucularIR()
        {
            RendaFixaInvestment fundosInvestment = new RendaFixaInvestment("Teste", 800, 1000, DateTime.Today, DateTime.Today.AddYears(1));

            Assert.IsTrue(fundosInvestment.IncomeTax == 10); // (1000 - 800) * 0.05 = 10
        }

    }
}
