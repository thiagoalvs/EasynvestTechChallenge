using EasynvestTechDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Application.DTOs
{
    public class InvestmentDTO
    {
        public string Name { get; set; }

        public double InvestedAmount { get; set; }

        public double Amount { get; set; }

        public string PurchaseDate { get; set; }

        public string ExpireDate { get; set; }

        public EInvestmentType InvestmentType { get; set; }
    }


}
