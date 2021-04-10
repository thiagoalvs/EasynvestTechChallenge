using EasynvestTechDemo.Application.DTOs;
using EasynvestTechDemo.Domain.Models;
using EasynvestTechDemo.Domain.Enums;
using EasynvestTechDemo.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Application.Factories
{
    public class InvestmentFactory : IInvestmentFactory
    {

        public Investment Create(InvestmentDTO dto)
        {
            switch (dto.InvestmentType)
            {
                case EInvestmentType.Fundos:
                    return new FundosInvestment(dto.Name, dto.InvestedAmount, dto.Amount, dto.PurchaseDate.ToDateTime(), dto.ExpireDate.ToDateTime());
                case EInvestmentType.RendaFixa:
                    return new RendaFixaInvestment(dto.Name, dto.InvestedAmount, dto.Amount, dto.PurchaseDate.ToDateTime(), dto.ExpireDate.ToDateTime());
                case EInvestmentType.TesouroDireto:
                    return new TesouroDiretoInvestment(dto.Name, dto.InvestedAmount, dto.Amount, dto.PurchaseDate.ToDateTime(), dto.ExpireDate.ToDateTime());
                default:
                    throw new Exception($"Tipo de investimeto {dto.InvestmentType.ToString()} é inválido");
            }
        }

    }
}
