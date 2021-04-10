using EasynvestTechDemo.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Infrastructure.Services.Fundos
{
    public class FundosServiceResponse
    {
        [JsonProperty("fundos")]
        public List<FundoServiceResponseItem> Investments { get; set; }
    }

    public class FundoServiceResponseItem
    {
        [JsonProperty("capitalInvestido")]
        public double InvestedAmount { get; set; }

        [JsonProperty("ValorAtual")]
        public double Amount { get; set; }

        [JsonProperty("dataResgate")]
        public string ExpireDate { get; set; }

        [JsonProperty("dataCompra")]
        public string PurchaseDate { get; set; }

        [JsonProperty("iof")]
        public int Iof { get; set; }

        [JsonProperty("totalTaxas")]
        public string TaxesTotal { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public int Qtd { get; set; }

        [JsonIgnore]
        public EInvestmentType InvestmentType => EInvestmentType.Fundos;
    }
}
