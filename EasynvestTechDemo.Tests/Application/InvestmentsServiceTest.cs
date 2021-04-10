using AutoMapper;
using EasynvestTechDemo.Application.DTOs;
using EasynvestTechDemo.Application.Factories;
using EasynvestTechDemo.Application.Interfaces.Infrastrucutre;
using EasynvestTechDemo.Application.Services;
using EasynvestTechDemo.Application.ViewModels;
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
    public class InvestmentsServiceTest
    {

        Mock<IInvestmentFactory> _investmentFactory;

        Mock<IMapper> _mapper;
        List<IExternalInvestmentService> _externalInvestmentServices;

        Mock<IExternalInvestmentService> _externalInvestmentSampleService;

        [TestInitialize]
        public void Initialize()
        {
            _investmentFactory = new Mock<IInvestmentFactory>();
            _mapper = new Mock<IMapper>();

            _externalInvestmentSampleService = new Mock<IExternalInvestmentService>();
            _externalInvestmentServices = new List<IExternalInvestmentService>();
        }

        [TestMethod]
        public async Task DeveOrquestrarAsChamadasCorretamente()
        {
            var sampleInvestment = new FundosInvestment("Teste", 1000, 1000, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(2));
            var sampleInvestmentDetailedViewModel = new InvestmentsDetailedViewModelItem { Name = "Teste", Amount = 1000, InvestedAmount = 1000, ExpireDate = DateTime.Now.AddYears(2) };
            var sampeInvestmentDTO = new InvestmentDTO() { Name = "Teste", Amount = 1000, InvestedAmount = 100, PurchaseDate = "2020-11-15T00:00:00", ExpireDate = "2022-11-15T00:00:00" };

            _investmentFactory.Setup(method => method.Create(It.IsAny<InvestmentDTO>())).Returns(sampleInvestment);

            _mapper.Setup(method => method.Map<InvestmentsDetailedViewModel>(It.IsAny<CustomerInvestments>())).Returns(new InvestmentsDetailedViewModel() { AmountTotal = 1000, Investments = new List<InvestmentsDetailedViewModelItem> { sampleInvestmentDetailedViewModel } });

            _externalInvestmentSampleService.Setup(method => method.GetInvestments()).ReturnsAsync(new List<InvestmentDTO>() { sampeInvestmentDTO });
            _externalInvestmentServices.Add(_externalInvestmentSampleService.Object);

            InvestmentsService service = new InvestmentsService(_investmentFactory.Object, _mapper.Object, _externalInvestmentServices);

            var result = await service.Get();

            Assert.IsTrue(result.Investments.Count == 1);
        }

        [TestMethod]
        public async Task DeveOrquestrarAsChamadasCorretamenteQuandoNaoHouveremRetornoDosServicosExternos()
        {
            _mapper.Setup(method => method.Map<InvestmentsDetailedViewModel>(It.IsAny<CustomerInvestments>())).Returns(new InvestmentsDetailedViewModel() { AmountTotal = 0, Investments = new List<InvestmentsDetailedViewModelItem>() });

            _externalInvestmentSampleService.Setup(method => method.GetInvestments()).ReturnsAsync(new List<InvestmentDTO>());
            _externalInvestmentServices.Add(_externalInvestmentSampleService.Object);

            InvestmentsService service = new InvestmentsService(_investmentFactory.Object, _mapper.Object, _externalInvestmentServices);

            var result = await service.Get();

            Assert.IsTrue(result.Investments.Count == 0);
        }

        [TestMethod]
        public async Task DeveOrquestrarAsChamadasCorretamenteQuandoNaoHouveremServicosExternos()
        {
            _mapper.Setup(method => method.Map<InvestmentsDetailedViewModel>(It.IsAny<CustomerInvestments>())).Returns(new InvestmentsDetailedViewModel() { AmountTotal = 0, Investments = new List<InvestmentsDetailedViewModelItem>() });

            InvestmentsService service = new InvestmentsService(_investmentFactory.Object, _mapper.Object, _externalInvestmentServices);

            var result = await service.Get();

            Assert.IsTrue(result.Investments.Count == 0);
        }
    }
}
