using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Domain.Models
{
    public class CustomerInvestments
    {
        public CustomerInvestments()
        {
            CustumerId = new Guid();
            Investments = new List<Investment>();
        }

        public Guid CustumerId { get; }

        public double AmountTotal { get; set; }

        public List<Investment> Investments { get; private set; }

        public void AddInvestiment(Investment investment)
        {
            Investments.Add(investment);
            AmountTotal += investment.Amount;
        }
    }
}
