using AutoMapper;
using EasynvestTechDemo.Application.DTOs;
using EasynvestTechDemo.Application.Factories;
using EasynvestTechDemo.Application.Interfaces.Infrastrucutre;
using EasynvestTechDemo.Application.Services;
using EasynvestTechDemo.Application.ViewModels;
using EasynvestTechDemo.Domain.Enums;
using EasynvestTechDemo.Domain.Models;
using EasynvestTechDemo.Infrastructure.Services.Fundos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasynvestTechDemo.Tests.Domain
{
    [TestClass]
    public class InvestmentFactoryTests
    {

        IInvestmentFactory _investmentFactory;

        [TestInitialize]
        public void Initialize()
        {
            _investmentFactory = new InvestmentFactory();
        }

        [TestMethod]
        public void DeveCriarUmFundosInvestment()
        {
            var sampleInvesmentDTO = new InvestmentDTO() { Name = "Teste", Amount = 1000, InvestedAmount = 100, PurchaseDate = "2020-11-15T00:00:00", ExpireDate = "2022-11-15T00:00:00", InvestmentType = EInvestmentType.Fundos };

            var instance = _investmentFactory.Create(sampleInvesmentDTO);

            Assert.IsTrue(instance.GetType() == typeof(FundosInvestment));
        }

        [TestMethod]
        public void DeveCriarUmTesouroDiretoInvestment()
        {
            var sampleInvesmentDTO = new InvestmentDTO() { Name = "Teste", Amount = 1000, InvestedAmount = 100, PurchaseDate = "2020-11-15T00:00:00", ExpireDate = "2022-11-15T00:00:00", InvestmentType = EInvestmentType.TesouroDireto };

            var instance = _investmentFactory.Create(sampleInvesmentDTO);

            Assert.IsTrue(instance.GetType() == typeof(TesouroDiretoInvestment));
        }

        [TestMethod]
        public void DeveCriarUmRendaFixaInvestment()
        {
            var sampleInvesmentDTO = new InvestmentDTO() { Name = "Teste", Amount = 1000, InvestedAmount = 100, PurchaseDate = "2020-11-15T00:00:00", ExpireDate = "2022-11-15T00:00:00", InvestmentType = EInvestmentType.RendaFixa };

            var instance = _investmentFactory.Create(sampleInvesmentDTO);

            Assert.IsTrue(instance.GetType() == typeof(RendaFixaInvestment));
        }
    }
}
