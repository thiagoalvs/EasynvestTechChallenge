using EasynvestTechDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Domain.Models
{
    public class FundosInvestment : Investment
    {
        public FundosInvestment(string name, double invested, double amount, DateTime purchaseDate, DateTime expirationDate) : base(name, invested, amount, purchaseDate, expirationDate)
        {

        }

        public override double IncomeTax => (Amount - InvestedAmount) * 0.15;

        public override EInvestmentType InvestmentType => EInvestmentType.Fundos;
    }
}
