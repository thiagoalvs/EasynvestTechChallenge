using EasynvestTechDemo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasynvestTechDemo.Application.Interfaces.Infrastrucutre
{
    public interface IExternalInvestmentService
    {
        Task<List<InvestmentDTO>> GetInvestments();
    }
}
