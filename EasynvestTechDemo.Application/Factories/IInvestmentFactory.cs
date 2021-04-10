using EasynvestTechDemo.Application.DTOs;
using EasynvestTechDemo.Domain.Models;

namespace EasynvestTechDemo.Application.Factories
{
    public interface IInvestmentFactory
    {
        Investment Create(InvestmentDTO dto);
    }
}