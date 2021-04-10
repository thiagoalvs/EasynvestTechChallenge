using EasynvestTechDemo.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasynvestTechDemo.Application.Interfaces
{
    public interface IInvestmentsService
    {

        Task<InvestmentsDetailedViewModel> Get();

    }
}
