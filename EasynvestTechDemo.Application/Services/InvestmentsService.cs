using AutoMapper;
using EasynvestTechDemo.Application.Factories;
using EasynvestTechDemo.Application.Interfaces;
using EasynvestTechDemo.Application.Interfaces.Infrastrucutre;
using EasynvestTechDemo.Application.ViewModels;
using EasynvestTechDemo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasynvestTechDemo.Application.Services
{
    public class InvestmentsService : IInvestmentsService
    {
        private readonly IInvestmentFactory _investmentFactory;

        private readonly IMapper _mapper;
        private readonly IEnumerable<IExternalInvestmentService> _externalInvestmentServices;

        public InvestmentsService(IInvestmentFactory investmentFactory, IMapper mapper, IEnumerable<IExternalInvestmentService> externalInvestmentServices)
        {
            _investmentFactory = investmentFactory;

            _mapper = mapper;
            _externalInvestmentServices = externalInvestmentServices;
        }

        public async Task<InvestmentsDetailedViewModel> Get()
        {
            CustomerInvestments investments = new CustomerInvestments();

            foreach (var service in _externalInvestmentServices)
                foreach (var investmentDTO in await service.GetInvestments())
                    investments.AddInvestiment(_investmentFactory.Create(investmentDTO));

            return _mapper.Map<InvestmentsDetailedViewModel>(investments);
        }

    }
}
