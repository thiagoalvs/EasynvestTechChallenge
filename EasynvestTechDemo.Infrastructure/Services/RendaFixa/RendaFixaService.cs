using AutoMapper;
using EasynvestTechDemo.Application.DTOs;
using EasynvestTechDemo.Application.Exceptions;
using EasynvestTechDemo.Application.Interfaces.Infrastrucutre;
using EasynvestTechDemo.Shared.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasynvestTechDemo.Infrastructure.Services.RendaFixa
{
    public class RendaFixaService : IExternalInvestmentService
    {
        private const string _serviceKey = "RendaFixa";

        private readonly ExternalServiceConfiguration _config;

        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _httpClientFactory;

        public RendaFixaService(IOptions<ExternalServicesConfiguration> config, IMapper mapper, IMemoryCache memoryCache, IHttpClientFactory httpClientFactory)
        {
            _config = config.Value.GetConfiguration(_serviceKey);

            _mapper = mapper;
            _memoryCache = memoryCache;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<InvestmentDTO>> GetInvestments()
        {
            if (!_memoryCache.TryGetValue(_serviceKey, out List<InvestmentDTO> result))
            {
                result = await GetInvestmentsInternal();

                _memoryCache.Set(_serviceKey, result, DateTime.Today.Date.AddDays(1));
            }

            return result;
        }

        private async Task<List<InvestmentDTO>> GetInvestmentsInternal()
        {
            using (var httpClient = _httpClientFactory.CreateClient("Default"))
            {
                var requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_config.Uri)
                };

                var responseMessage = await httpClient.SendAsync(requestMessage);

                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                    return new List<InvestmentDTO>();

                var content = string.Empty;
                if (responseMessage.Content != null)
                    content = await responseMessage.Content.ReadAsStringAsync();

                if (!responseMessage.IsSuccessStatusCode)
                    throw new ServiceUnavailableException($"Falha ao chamar o serviço {_serviceKey}. Result: {(int)responseMessage.StatusCode} - {responseMessage.ReasonPhrase}", content);

                RendaFixaServiceResponse response = JsonConvert.DeserializeObject<RendaFixaServiceResponse>(await responseMessage.Content.ReadAsStringAsync());

                return _mapper.Map<List<InvestmentDTO>>(response.Investments);
            }
        }
    }
}
