using EasynvestTechDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Domain.Models
{
    public abstract class Investment
    {
        public Investment(string name, double invested, double amount, DateTime purchaseDate, DateTime expirationDate)
        {
            Name = name;
            InvestedAmount = invested;
            Amount = amount;
            PurchaseDate = purchaseDate;
            ExpireDate = expirationDate;
        }

        public string Name { get; }

        public double InvestedAmount { get; }

        public double Amount { get; }

        public DateTime PurchaseDate { get; }

        public DateTime ExpireDate { get; }

        public abstract double IncomeTax { get; }

        public double DrawAmount {
            get
            {
                if (ExpireDate.AddMonths(-3) < DateTime.Now) return InvestedAmount - (InvestedAmount * 0.06);
                if ((DateTime.Now - PurchaseDate).TotalDays > ((ExpireDate - PurchaseDate).TotalDays / 2)) return InvestedAmount - (InvestedAmount * 0.15);
                return InvestedAmount - (InvestedAmount * 0.3);
            }
        }

        public abstract EInvestmentType InvestmentType { get; }

    }
}
