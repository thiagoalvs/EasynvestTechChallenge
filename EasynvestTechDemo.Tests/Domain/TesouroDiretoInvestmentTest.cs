using EasynvestTechDemo.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Tests.Domain
{
    [TestClass]
    public class TesouroDiretoInvestmentTest
    {

        [TestMethod]
        public void DeveRetirar15PorCentoDaRentabilidadeParaCalucularIR()
        {
            TesouroDiretoInvestment tesouroDiretoInvestment = new TesouroDiretoInvestment("Teste", 800, 1000, DateTime.Today, DateTime.Today.AddYears(1));

            Assert.IsTrue(tesouroDiretoInvestment.IncomeTax == 20); // (1000 - 800) * 0.1 = 20
        }

    }
}
