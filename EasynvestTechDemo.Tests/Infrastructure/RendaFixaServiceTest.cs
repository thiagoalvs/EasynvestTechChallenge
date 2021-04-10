using AutoMapper;
using EasynvestTechDemo.Application.DTOs;
using EasynvestTechDemo.Application.Exceptions;
using EasynvestTechDemo.Infrastructure.Services.RendaFixa;
using EasynvestTechDemo.Shared.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasynvestTechDemo.Tests.Infrastructure
{
    [TestClass]
    public class RendaFixaServiceTest
    {
        private const string _serviceKey = "RendaFixa";

        IOptions<ExternalServicesConfiguration> _config;
        IMapper _mapper;

        Mock<IHttpClientFactory> _httpClientFactory;
        Mock<IMemoryCache> _cache;

        [TestInitialize]
        public void Initialize()
        {
            _config = Options.Create(new ExternalServicesConfiguration()
            {
                Services = new Dictionary<string, ExternalServiceConfiguration>()
            });
            _config.Value.Services.Add(_serviceKey, new ExternalServiceConfiguration() { Uri = "https://sampleuri" });

            _httpClientFactory = new Mock<IHttpClientFactory>();

            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RendaFixaServiceResponseItem, InvestmentDTO>();
            }));

            _cache = new Mock<IMemoryCache>();
            _cache.Setup(method => method.CreateEntry(It.IsAny<string>())).Returns(new Mock<ICacheEntry>().Object);
        }

        [TestMethod]
        public async Task DeveRetornarInvestimentosSemCache()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new RendaFixaServiceResponse() { Investments = new List<RendaFixaServiceResponseItem>() { new RendaFixaServiceResponseItem() } })),
                });



            var client = new HttpClient(mockHttpMessageHandler.Object);
            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            RendaFixaService service = new RendaFixaService(_config, _mapper, _cache.Object, _httpClientFactory.Object);

            var result = await service.GetInvestments();

            Assert.IsTrue(result.Count == 1);

        }

        [TestMethod]
        public async Task DeveRetornarInvestimentosComCache()
        {
            object expectedValue = new List<InvestmentDTO>() { new InvestmentDTO() };
            _cache.Setup(method => method.TryGetValue(It.IsAny<object>(), out expectedValue)).Returns(true);

            RendaFixaService service = new RendaFixaService(_config, _mapper, _cache.Object, _httpClientFactory.Object);

            var result = await service.GetInvestments();

            Assert.IsTrue(result.Count == 1);

        }

        [TestMethod]
        public async Task DeveRetornarListaVaziaAoTomarNotFound()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                });



            var client = new HttpClient(mockHttpMessageHandler.Object);
            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            RendaFixaService service = new RendaFixaService(_config, _mapper, _cache.Object, _httpClientFactory.Object);

            var result = await service.GetInvestments();

            Assert.IsTrue(result.Count == 0);

        }

        [TestMethod]
        [ExpectedException(typeof(ServiceUnavailableException))]
        public async Task DeveRetornarExceptionAoNaoConseguirChamarOServicoExterno()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                });



            var client = new HttpClient(mockHttpMessageHandler.Object);
            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            RendaFixaService service = new RendaFixaService(_config, _mapper, _cache.Object, _httpClientFactory.Object);

            var result = await service.GetInvestments();

        }

    }
}
