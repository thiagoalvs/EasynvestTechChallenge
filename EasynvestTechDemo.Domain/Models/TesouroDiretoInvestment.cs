using EasynvestTechDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Domain.Models
{
    public class TesouroDiretoInvestment : Investment
    {
        public TesouroDiretoInvestment(string name, double invested, double amount, DateTime purchaseDate, DateTime expirationDate) : base(name, invested, amount, purchaseDate, expirationDate)
        {

        }

        public override double IncomeTax => (Amount - InvestedAmount) * 0.1;

        public override EInvestmentType InvestmentType => EInvestmentType.Fundos;
    }
}
